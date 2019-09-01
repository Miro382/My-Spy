using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My_Spy_Administrator
{
    class ManageApplication
    {
     
        /// <summary>
        /// This will restart application.
        /// </summary>
        /// <param name="removepath">Path to directory to remove</param>
        public void RestartApplication(string removepath)
        {
            DialogResult dialogResult = MessageBox.Show(ResourcesFiles.ProgramStrings.RestartApplicationDes, ResourcesFiles.ProgramStrings.RestartApplication, MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                //do something else
                Directory.Delete(removepath, true);
                Directory.CreateDirectory(removepath);
                Application.Restart();
            }
            else if (dialogResult == DialogResult.No)
            {

            }

        }


        /// <summary>
        /// Disable or enable task manager
        /// </summary>
        /// <param name="Enable">Enable, Disable</param>
        public void SetTaskManager(bool Enable)
        {
            RegistryKey Regman = Registry.CurrentUser.CreateSubKey(
                @"Software\Microsoft\Windows\CurrentVersion\Policies\System");

            if (Enable)
            {
                Regman.SetValue("DisableTaskMgr", "1");
            }
            else
            {
                if (Regman.GetValue("DisableTaskMgr") != null)
                    Regman.DeleteValue("DisableTaskMgr");
            }

            Regman.Close();
        }


    }
}
