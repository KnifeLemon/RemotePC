using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RemotePC_.RemoteUtils
{
    public static class RemoteMouse
    {
        public static string Position
        {
            //M;상태;버튼;X;Y;휠Delta
            set
            {
                Console.WriteLine(value);
                try
                {
                    string[] mouseState;
                    mouseState = value.Split(';');
                    string state = mouseState[1];
                    string button = mouseState[2];
                    int X = Convert.ToInt32(mouseState[3]);
                    int Y = Convert.ToInt32(mouseState[4]);
                    int delta = Convert.ToInt32(mouseState[5]);

                    if (state == "move")
                    {
                        MouseSimulator.X = X;
                        MouseSimulator.Y = Y;
                    }
                    else if (state == "down")
                    {
                        if (button == "Left")
                            MouseSimulator.MouseDown(MouseButton.Left);
                        else if (button == "Right")
                            MouseSimulator.MouseDown(MouseButton.Right);
                        else if (button == "Middle")
                            MouseSimulator.MouseDown(MouseButton.Middle);
                    }
                    else if (state == "up")
                    {
                        if (button == "Left")
                            MouseSimulator.MouseUp(MouseButton.Left);
                        else if (button == "Right")
                            MouseSimulator.MouseUp(MouseButton.Right);
                        else if (button == "Middle")
                            MouseSimulator.MouseUp(MouseButton.Middle);
                    }
                    else
                    {
                        MouseSimulator.MouseWheel(delta);
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.StackTrace); }
            }
        }
    }
}
