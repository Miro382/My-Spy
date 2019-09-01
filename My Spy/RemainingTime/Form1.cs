using Saving;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace RemainingTime
{
    public partial class Form1 : Form
    {

        //------- Funkcie na minimalizovanie aplikacii
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true)]
        static extern IntPtr SendMessage(IntPtr hWnd, Int32 Msg, IntPtr wParam, IntPtr lParam);

        const int WM_COMMAND = 0x111;
        const int MIN_ALL = 419;
        //------


        //----- Funkcie sluziace na presuvanie okna
        private const int WM_NCHITTEST = 0x84;
        private const int HT_CLIENT = 0x1;
        private const int HT_CAPTION = 0x2;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)(HT_CAPTION);
        }
        //-----


        int Time = 0;
        string NotifyText = "";
        string MinutesText = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            notifyIcon1.MouseClick += notifyIcon1_MouseClick;

            NotifyText = notifyIcon1.Text;
            MinutesText = labelremtime.Text;

            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Block/ComputerTime.dat"))
            {
                SaveWriter save = new SaveWriter(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Block/ComputerTime.dat");
                save.Load();

                if (save.GetItemBool("Enable"))
                {
                    Time = save.GetItemInt("WarningTime");
                    labelremtime.Text = "" + Time + " " + MinutesText;
                    notifyIcon1.Text = NotifyText + " " + Time + " " + MinutesText;
                }

                save.Destroy();
            }
            else
            {
                Application.Exit();
            }


            if(File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/shwwrn.dat"))
            {
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/shwwrn.dat");
            }else
            {
               // Application.Exit();
            }

            try
            {
                SoundPlayer player = new SoundPlayer(Properties.Resources.warning);
                player.Play();
            }catch(Exception ex)
            {
            }

            try
            {
                IntPtr lHwnd = FindWindow("Shell_TrayWnd", null);
                SendMessage(lHwnd, WM_COMMAND, (IntPtr)MIN_ALL, IntPtr.Zero);
                WindowState = FormWindowState.Normal;
            }
            catch(Exception ex)
            {

            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Time--;
            labelremtime.Text = "" + Time + " "+MinutesText;
            notifyIcon1.Text = NotifyText+ " " +Time+ " "+MinutesText;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CancelShutdown canc = new CancelShutdown();
            canc.Show();
        }
    }
}
