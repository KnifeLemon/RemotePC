using RemotePC_.FunctionUtils;
using RemotePC_.RemoteUtils;
using RemotePC_.SocketUtils;
using RemotePCUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemotePC_
{
    public partial class MainForm : Form
    {
        private Random random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF); //랜덤 시드값

        //ini파일 경로
        private string inipath = AppDomain.CurrentDomain.BaseDirectory + "\\Setting.ini";
        //ini클래스 선언
        private IniFile ini = new IniFile();

        Client client = new Client();
        List<RemoteForm> RemoteWindows = new List<RemoteForm>();

        string password = "";

        //원격중인지 체크하는 변수
        bool letsRemote = false;
        //원격자인지 체크하는 변수
        bool isremoter = false;

        //이미지 전송시 제한할 용량
        const int BlockLength = 50000;

        public MainForm()
        {
            InitializeComponent();

            client.OnServerConnect += Client_OnServerConnect;
            client.OnServerDisconnect += Client_OnServerDisconnect;
            client.OnResultsUpdate += Client_OnResultsUpdate;
            client.OnClientAdded += Client_OnClientAdded;
            client.OnClientUpdated += Client_OnClientUpdated;
            client.OnClientRemoved += Client_OnClientRemoved;
            client.OnClientConnection += Client_OnClientConnection;
            client.OnMessageReceived += Client_OnMessageReceived;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;

            //펀치서버에 연결
            client.ConnectOrDisconnect();
            //UPnP 허용
            client.UPnPEnabled = true;

            //INI 파일이 없을경우
            if (!File.Exists(inipath))
            {
                //비밀번호를 생성하고
                password = Utils.GetUserPW();

                //ini에 입력
                ini["UserSetting"]["Password"] = password;
                ini.Save(inipath);
            }
            else
            {
                //ini를 불러옴
                ini.Load(inipath);
                password = ini["UserSetting"]["Password"].ToString();
            }

            txtMyID.Text = Utils.GetUserID();
            txtMyPW.Text = password;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void btnRefreshPassword_Click(object sender, EventArgs e)
        {
            password = Utils.GetUserPW();
            ini["UserSetting"]["Password"] = password;
            ini.Save(inipath);
            txtMyPW.Text = password;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (btnConnect.Text == "Connect")
            {
                gbRemotePC.Enabled = false;
                client.GetTargetInfo(txtRemoteID.Text);
            }
            else
            {
                if (Application.OpenForms.OfType<RemoteForm>().Count() == 1)
                {
                    RemoteWindows.Remove(Application.OpenForms.OfType<RemoteForm>().First());
                    Application.OpenForms.OfType<RemoteForm>().First().Close();
                }
                Thread.Sleep(3000);
                letsRemote = false;
                isremoter = false;
                gbRemotePC.Enabled = true;
                btnConnect.Enabled = true;
                btnConnect.Text = "Connect";
            }
        }

        /// <summary>
        /// 서버에 연결이 될 때
        /// </summary>
        private void Client_OnServerConnect(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 서버와 연결이 끊길 때
        /// </summary>
        private void Client_OnServerDisconnect(object sender, EventArgs e)
        {
           
        }

        /// <summary>
        /// 서버로부터 결과를 받아올 때
        /// </summary>
        private void Client_OnResultsUpdate(object sender, string e)
        {
            //e = 받은 메세지
            Console.WriteLine(e);

            if (e.Contains("컴퓨터에서 연결을 거부") || e.Contains("연결되지 않은 소켓"))
            {
                MessageBox.Show("원격서버와 연결에 실패했습니다. 잠시 후 다시 시도해주세요." + Environment.NewLine +
                                "1. 메인 서버에 문제가 있습니다." + Environment.NewLine +
                                "2. 인터넷 연결이 원활하지 않습니다." + Environment.NewLine +
                                "" + Environment.NewLine +
                                "이러한 문제가 계속 된다면 관리자에게 문의해주세요.", this.Text);
                Application.Exit();
            }

            if (e.Contains(";"))
            {
                string[] sData = e.Split(':')[1].Trim().Split(';');
                string mode = sData[0];
                string cmd = sData[1];

                if (mode == "FINDUSER")
                {
                    if (cmd == "NO")
                    {
                        //유저가 접속중이지 않음.
                        MessageBox.Show("원격 컴퓨터에 연결을 실패했습니다." + Environment.NewLine +
                                        "1. 상대방이 온라인 상태가 아닙니다." + Environment.NewLine +
                                        "2. 올바르지 않은 아이디 또는 비밀번호를 입력했습니다." + Environment.NewLine +
                                        "3. 인터넷연결이 원활하지 않습니다.", this.Text);
                        gbRemotePC.Enabled = true;
                    }
                    else
                    {
                        //원격준비 완료
                        //클라이언트 By 클라이언트 연결시킴
                        letsRemote = true;
                        ClientInfo CI = client.GetClientByID(Convert.ToInt64(cmd));
                        client.ConnectToClient(CI);
                    }
                }
            }
        }

        /// <summary>
        /// 새로운 클라이언트가 추가될 때
        /// </summary>
        private void Client_OnClientAdded(object sender, ClientInfo e)
        {
            
        }
        
        /// <summary>
        /// 연결된 클라이언트들의 정보가 변경될 때
        /// </summary>
        private void Client_OnClientUpdated(object sender, ClientInfo e)
        {
        }

        /// <summary>
        /// 연결된 클라이언트가 연결해제할 때
        /// </summary>
        private void Client_OnClientRemoved(object sender, ClientInfo e)
        {
            
        }

        /// <summary>
        /// 클라이언트가 나에게 연결을 걸 때
        /// </summary>
        private void Client_OnClientConnection(object sender, IPEndPoint e)
        {
            //원격제어자는 원격제어를 위한 비밀번호 체크
            if (letsRemote)
            {
                RemotePCUtils.Message M = new RemotePCUtils.Message(client.LocalClientInfo.Name, Name, "CHECKPASSWORD;" + txtRemotePW.Text);
                client.SendMessageUDP(M, e);
            }
        }

        /// <summary>
        /// 연결된 클라이언트로 부터 응답이 수신될 때
        /// </summary>
        private void Client_OnMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            string msg = e.message.Content;
            
            if (msg.Contains(";"))
            {
                string[] sData = msg.Split(';');
                string mode = sData[0];
                string cmd = sData[1];

                //원격 받는자는 비밀번호를 체크시켜줌
                if (mode == "CHECKPASSWORD")
                {
                    RemotePCUtils.Message M;
                    if (password == cmd)
                    {
                        letsRemote = true;
                        isremoter = false;
                        gbRemotePC.Enabled = false;
                        btnConnect.Enabled = false;
                        M = new RemotePCUtils.Message(client.LocalClientInfo.Name, Name, "REMOTE;OK");
                    }
                    else
                    {
                        M = new RemotePCUtils.Message(client.LocalClientInfo.Name, Name, "REMOTE;NO");
                    }
                    client.SendMessageUDP(M, e.EstablishedEP);
                }
                //원격 거는자는 비밀번호 결과를 받음
                else if (mode == "REMOTE")
                {
                    if (cmd == "OK")
                    {
                        Console.WriteLine("비밀번호 OK");
                        btnConnect.Text = "Disconnect";
                        letsRemote = true;
                        isremoter = true;
                    }
                    else
                    {
                        letsRemote = false;
                        MessageBox.Show("원격 컴퓨터에 연결을 실패했습니다.1" + Environment.NewLine +
                                        "1. 상대방이 온라인 상태가 아닙니다." + Environment.NewLine +
                                        "2. 올바르지 않은 아이디 또는 비밀번호를 입력했습니다." + Environment.NewLine +
                                        "3. 인터넷연결이 원활하지 않습니다.", this.Text);
                        gbRemotePC.Enabled = true;
                    }
                }
                else if (mode == "STOPREMOTE")
                {
                    RemoteForm remote = RemoteWindows.FirstOrDefault(C => C.RemoteEP.Equals((IPEndPoint)sender));
                    RemoteWindows.Remove(remote);
                    letsRemote = false;
                    isremoter = false;
                    gbRemotePC.Enabled = true;
                    btnConnect.Enabled = true;
                    btnConnect.Text = "Connect";
                }
            }


            if (letsRemote)
            {
                //원격 거는 사람일 경우
                if (isremoter)
                {
                    RemoteForm remote = RemoteWindows.FirstOrDefault(C => C.RemoteEP.Equals((IPEndPoint)sender));
                    if (remote == null)
                    {
                        this.Invoke(new MethodInvoker(
                            delegate()
                            {
                                Console.WriteLine("원격 연결을 시작합니다.");
                                ClientInfo CI = e.clientInfo;

                                if (remote == null)
                                {
                                    remote = new RemoteForm(client, e.clientInfo.Name, e.EstablishedEP, e.clientInfo.ID);
                                    RemoteWindows.Add(remote);
                                    remote.Closed += delegate {
                                        Thread.Sleep(3000);
                                        RemoteWindows.Remove(remote);
                                        letsRemote = false;
                                        isremoter = false;
                                        gbRemotePC.Enabled = true;
                                        btnConnect.Enabled = true;
                                        btnConnect.Text = "Connect";
                                    };
                                    remote.Show();
                                }
                                else
                                {
                                    remote.Focus();
                                }
                            }
                        )
                        );
                    }
                    else
                    {
                        remote.Focus();
                    }

                    remote.ReceiveMessage(e.message);
                }
                else
                //원격 받는 사람일 경우
                {
                    if (e.message.Content.Contains("Shot"))
                    {
                        Image img = ScreenCapture.CaptureScreen();
                        Console.WriteLine("캡쳐");
                        img = VaryQualityLevel(img, 50L); //이미지 퀄리티 50퍼 낮춤
                        byte[] imgbyte = Utils.imageToByteArray(img);
                        Console.WriteLine("이미지 : {0}", imgbyte.Length);

                        byte[] byteArray = Utils.CompressBZip2(imgbyte);
                        Console.WriteLine("BZIP2 : {0}", byteArray.Length);
                        int sendcnt = 0;

                        byte[][] sbyteArray = ByteArrayToChunks(byteArray, BlockLength);
                        for (int i = 0; i < sbyteArray.Count(); i++)
                        {
                            if (sendcnt >= 2)
                            {
                                sendcnt = 0;
                                Thread.Sleep(300);
                            }
                            Console.WriteLine("이미지 보냄 : Now:{0} - Total:{1} - TotalLength:{2}", (i + 1).ToString(), sbyteArray.Count(), byteArray.Length);
                            RemotePCUtils.Message BI = new RemotePCUtils.Message(client.LocalClientInfo.Name, Name, "Image;" + (i + 1).ToString() + "/" + sbyteArray.Count().ToString() + "/" + byteArray.Length, sbyteArray[i]);
                            client.SendMessageUDP(BI, e.EstablishedEP);
                            ++sendcnt;
                        }
                    }
                    
                    //키보드 / 마우스 제어
                    if (e.message.Content.Contains("M;"))
                        RemoteMouse.Position = e.message.Content;
                    else if (e.message.Content.Contains("K;"))
                        RemoteKeybd.KeybdButton = e.message.Content;
                }
            }
        }

        private Image VaryQualityLevel(Image img, long percent)
        {
            // Get a bitmap.
            ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);

            // Create an Encoder object based on the GUID
            // for the Quality parameter category.
            System.Drawing.Imaging.Encoder myEncoder =
                System.Drawing.Imaging.Encoder.Quality;

            // Create an EncoderParameters object.
            // An EncoderParameters object has an array of EncoderParameter
            // objects. In this case, there is only one
            // EncoderParameter object in the array.
            EncoderParameters myEncoderParameters = new EncoderParameters(1);

            Stream s;
            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, percent);
            myEncoderParameters.Param[0] = myEncoderParameter;
            s = img.ToStream(jgpEncoder, myEncoderParameters);

            return System.Drawing.Image.FromStream(s);
        }

        

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        public byte[][] ByteArrayToChunks(byte[] byteData, long BufferSize)
        {
            byte[][] chunks = byteData.Select((value, index) => new { PairNum = Math.Floor(index / (double)BufferSize), value }).GroupBy(pair => pair.PairNum).Select(grp => grp.Select(g => g.value).ToArray()).ToArray();
            return chunks;
        }
    }
}
