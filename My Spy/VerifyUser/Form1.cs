using EncryptionLibrary;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace VerifyUser
{
    public partial class Form1 : Form
    {
        string RemTime = "";
        int time = 5;

        Encryption encryption = new Encryption("kmAAnmAbVFSO9pgA2sy5X9lZhn5TulKi74FDZYZw");
        RegistryKey registry =  Registry.LocalMachine.OpenSubKey("Software\\My_Spy", false);

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            time--;
            labeltime.Text = RemTime.Replace(":", ": "+time);
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/ExpiredTime.dat"))
                Application.Exit();

            RemTime = labeltime.Text;
            labeltime.Text = RemTime.Replace(":", ": "+time);
        }


        private void TextBox1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1_Click(textBox1, EventArgs.Empty);
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            try
            {
                string password = encryption.DecryptString((string)registry.GetValue("Properties"), "2pqB7l4eRM6nHaZPtetkkHePguacGTvrMhvnE4fy");
                if (password.Equals(textBox1.Text))
                {
                    File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/DisableShutDown.dat", "True");
                    Application.Exit();
                }
                else
                {
                    labelbad.Visible = true;
                }
            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

    }
}
