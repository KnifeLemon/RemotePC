using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemotePC_.RemoteUtils
{
    public static class RemoteKeybd
    {
        public static string KeybdButton
        {
            //K;상태;키;KeyCode;KeyChar;Shift상태;Alt상태;Control상태
            set
            {
                Console.WriteLine(value);
                string[] keyState;
                keyState = value.Split(';');
                string state = keyState[1];
                string keycode = keyState[2];
                string keychar = keyState[3];
                string shift = keyState[4];
                string alt = keyState[5];
                string control = keyState[6];

                if (state == "press")
                {
                    Keys key = (Keys)char.ToUpper(char.Parse(keyState[3]));
                    KeyboardSimulator.KeyPress(key);
                }
                else if (state == "down")
                {
                    Keys key = (Keys)Int32.Parse(keyState[2]);
                    KeyboardSimulator.KeyDown(key);
                    if (!string.IsNullOrEmpty(shift))
                        KeyboardSimulator.KeyDown(Keys.Shift);
                    if (!string.IsNullOrEmpty(alt))
                        KeyboardSimulator.KeyDown(Keys.Alt);
                    if (!string.IsNullOrEmpty(control))
                        KeyboardSimulator.KeyDown(Keys.Control);
                }
                else
                {
                    Keys key = (Keys)Int32.Parse(keyState[2]);
                    KeyboardSimulator.KeyUp(key);
                    if (!string.IsNullOrEmpty(shift))
                        KeyboardSimulator.KeyUp(Keys.Shift);
                    if (!string.IsNullOrEmpty(alt))
                        KeyboardSimulator.KeyUp(Keys.Alt);
                    if (!string.IsNullOrEmpty(control))
                        KeyboardSimulator.KeyUp(Keys.Control);
                }
            }
        }
    }
}
