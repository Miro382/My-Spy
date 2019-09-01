using System;
using System.IO;
using System.Windows.Forms;

namespace My_Spy
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if(File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/Remind.dat"))
            {
                NoteForm ntform = new NoteForm();
                ntform.Path = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/Remind.dat");
                ntform.remindshow = true;
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/Remind.dat");
                Application.Run(ntform);
            }
            else
            Application.Run(new Form1());
        }
    }
}
