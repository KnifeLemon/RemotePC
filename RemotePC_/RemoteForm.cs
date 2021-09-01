using RemotePC_.SocketUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RemotePCUtils;
using System.IO;
using RemotePC_.FunctionUtils;
using System.Threading;
using System.Globalization;
using RemotePC_.RemoteUtils;

namespace RemotePC_
{
    public partial class RemoteForm : Form
    {
        private readonly KeyboardHook keyboardHook = new KeyboardHook();
        private readonly MouseHook mouseHook = new MouseHook();

        public Client client;
        public new string Name;
        public IPEndPoint RemoteEP;
        public long ID;

        public bool m_right = false;
        public bool m_left = false;
         int prevX = 0;
         int prevY = 0;
        public bool start = false;
        bool enter = false;
        bool ready = false;

        public RemoteForm(Client _client, string _Name, IPEndPoint _RemoteEP, long _ID)
        {
            InitializeComponent();

            client = _client;
            Name = _Name;
            RemoteEP = _RemoteEP;
            ID = _ID;

            this.CenterToScreen();
            this.Text = "RemotePC - " + _Name;
        }

        private void RemoteForm_Load(object sender, EventArgs e)
        {
            mouseHook.MouseMove += mouseHook_MouseMove;
            mouseHook.MouseDown += mouseHook_MouseDown;
            mouseHook.MouseUp += mouseHook_MouseUp;
            mouseHook.MouseWheel += mouseHook_MouseWheel;

            keyboardHook.KeyDown += keyboardHook_KeyDown;
            keyboardHook.KeyUp += keyboardHook_KeyUp;
            keyboardHook.KeyPress += keyboardHook_KeyPress;

            mouseHook.Start();
            keyboardHook.Start();
        }

        private void RemoteForm_Shown(object sender, EventArgs e)
        {
            start = true;
            ready = true;
        }

        List<byte[]> receiveImages = new List<byte[]>();
        byte[] newArray;
        public void ReceiveMessage(RemotePCUtils.Message M)
        {
            Console.WriteLine("원격 메세지 받음 : " + M.Content);
            if (M.Content.Contains("Image"))
            {
                string[] sData = M.Content.Split(';');
                int now = Convert.ToInt32(sData[1].Split('/')[0]);
                int total = Convert.ToInt32(sData[1].Split('/')[1]);
                int length = Convert.ToInt32(sData[1].Split('/')[2]);

                if (M.BImage != null)
                {
                    //0개일 때 최초설정
                    if (receiveImages.Count == 0)
                    {
                        for (int i = 0; i < total; i++)
                        {
                            receiveImages.Add(new byte[0]);
                        }
                    }

                    receiveImages[now - 1] = M.BImage;
                    check(total, length);
                }
            }
        }

        void check(int total, int length)
        {
            newArray = Combine(receiveImages.ToArray());

            Console.WriteLine("Check Length : {0} - {1}", newArray.Length, length);
            if (newArray.Length >= length)
            {
                ready = true;

                byte[] imgbyte = newArray;

                Console.WriteLine("BZIP2 : {0}", imgbyte.Length);

                byte[] byteArray = Utils.DecompressBZip2(imgbyte);
                Console.WriteLine("이미지 : {0}", byteArray.Length);
                Image img = FunctionUtils.Utils.ByteArrayToImage(byteArray);
                pbGround.Image = img;

                receiveImages.Clear();
                newArray = null;
            }
        }

        private byte[] Combine(params byte[][] arrays)
        {
            byte[] rv = new byte[arrays.Sum(a => a.Length)];
            int offset = 0;
            foreach (byte[] array in arrays)
            {
                System.Buffer.BlockCopy(array, 0, rv, offset, array.Length);
                offset += array.Length;
            }
            return rv;
        }

        private void SendMessage(string message)
        {
            RemotePCUtils.Message M = new RemotePCUtils.Message(client.LocalClientInfo.Name, Name, message);
            client.SendMessageUDP(M, RemoteEP);
        }

        //K;상태;키;KeyCode;KeyChar;Shift상태;Alt상태;Control상태
        private void keyboardHook_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.Focused && start)
            {
                SendMessage(string.Format(
                        "{0};{1};{2};{3};{4};{5};{6}",
                        "K",
                        "press",
                        "1",
                        e.KeyChar.ToString(CultureInfo.InvariantCulture),
                        "False",
                        "False",
                        "Flase"
                        ));
            }
        }

        private void keyboardHook_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.Focused && start)
            {
                SendMessage(string.Format(
                    "{0};{1};{2};{3};{4};{5};{6}",
                    "K",
                    "up",
                    e.KeyCode.ToString(),
                    "1",
                    e.Shift.ToString(),
                    e.Alt.ToString(),
                    e.Control.ToString()
                    ));
            }
        }

        private void keyboardHook_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.Focused && start)
            {
                SendMessage(string.Format(
                    "{0};{1};{2};{3};{4};{5};{6}",
                    "K",
                    "down",
                    e.KeyCode.ToString(),
                    "1",
                    e.Shift.ToString(),
                    e.Alt.ToString(),
                    e.Control.ToString()
                    ));
            }
        }

        //M;상태;버튼;X;Y;휠Delta
        private void mouseHook_MouseWheel(object sender, MouseEventArgs e)
        {
            if (this.Focused && start && enter)
            {
                SendMessage(string.Format(
                        "{0};{1};{2};{3};{4};{5}",
                        "M",
                        "wheel",
                        "",
                        "0",
                        "0",
                        e.Delta.ToString(CultureInfo.InvariantCulture)
                        ));
            }
        }

        private void mouseHook_MouseUp(object sender, MouseEventArgs e)
        {
            Console.WriteLine(e.Button + " / " + e.X + " / " + e.Y);
            if (this.Focused && start && enter)
            {
                Point location = pbGround.PointToScreen(Point.Empty);
                SendMessage(string.Format(
                        "{0};{1};{2};{3};{4};{5}",
                        "M",
                        "up",
                        e.Button.ToString(),
                        (e.X - location.X).ToString(CultureInfo.InvariantCulture),
                        (e.Y - location.Y).ToString(CultureInfo.InvariantCulture),
                        "0"
                        ));
            }
        }

        private void mouseHook_MouseDown(object sender, MouseEventArgs e)
        {
            Console.WriteLine(e.Button + " / " + e.X + " / " + e.Y);
            if (this.Focused && start && enter)
            {
                Point location = pbGround.PointToScreen(Point.Empty);
                SendMessage(string.Format(
                        "{0};{1};{2};{3};{4};{5}",
                        "M",
                        "down",
                        e.Button.ToString(),
                        (e.X - location.X).ToString(CultureInfo.InvariantCulture),
                        (e.Y - location.Y).ToString(CultureInfo.InvariantCulture),
                        "0"
                        ));
            }
        }

        private void mouseHook_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.Focused && start && enter && prevX != e.X && prevY != e.Y)
            {
                prevX = e.X;
                prevY = e.Y;
                Point location = pbGround.PointToScreen(Point.Empty);
                SendMessage(string.Format(
                        "{0};{1};{2};{3};{4};{5}",
                        "M",
                        "move",
                        "",
                        (e.X - location.X).ToString(CultureInfo.InvariantCulture),
                        (e.Y - location.Y).ToString(CultureInfo.InvariantCulture),
                        "0"
                        ));
            }
        }

        private void pbGround_MouseEnter(object sender, EventArgs e)
        {
            enter = true;
        }

        private void pbGround_MouseLeave(object sender, EventArgs e)
        {
            enter = false;
        }

        private void RemoteForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void RemoteForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                keyboardHook.Stop();
                mouseHook.Stop();
            }
            catch { }

            try { SendMessage("STOPREMOTE;"); }
            catch { }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ready)
            {
                ready = false;
                SendMessage("Shot");
            }

        }
    }
}
