using Saving;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace GetRunningApplications
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //Thread na zrychlenie nacitania niektorych veci
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;


                try
                {
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/");

                    SaveWriter save = new SaveWriter(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/appreport.dat");

                    try
                    {
                        if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/appreport.dat"))
                            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/appreport.dat");
                    }
                    catch (Exception ex)
                    {
                    }

                    int k = 0;
                    Process[] processes = Process.GetProcesses();
                    foreach (Process pr in processes)
                    {

                        try
                        {
                            if (!string.IsNullOrEmpty(pr.MainWindowTitle))
                            {
                                // Debug.WriteLine(pr.MainWindowTitle + "   (" + pr.MainModule.ModuleName + ") " + pr.Id + " Session: " + pr.SessionId);
                                save.AddItem("Title_" + k, pr.MainWindowTitle);
                                save.AddItem("Module_" + k, pr.MainModule.ModuleName);
                                k++;
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }

                    save.AddItem("Count", k);
                    save.Save();

                }
                catch (Exception ex)
                {
                }
                Application.Exit();

            }).Start();
            //koniec threadu

        }

    }
}
