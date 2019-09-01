using EncryptionLibrary;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace RemainingTime
{
    public partial class CancelShutdown : Form
    {
        Encryption encryption = new Encryption("kmAAnmAbVFSO9pgA2sy5X9lZhn5TulKi74FDZYZw");
        RegistryKey registry = Registry.LocalMachine.OpenSubKey("Software\\My_Spy", false);

        public CancelShutdown()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                string password = encryption.DecryptString((string)registry.GetValue("Properties"), "2pqB7l4eRM6nHaZPtetkkHePguacGTvrMhvnE4fy");
                if (password.Equals(textBox1.Text))
                {
                    File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/NoShutDownFirst.dat", "True");

                    Application.Exit();
                }
                else
                {
                    labelbad.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private void CancelShutdown_Load(object sender, EventArgs e)
        {
            textBox1.KeyPress += TextBox1_KeyPress;
        }


        private void TextBox1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1_Click(textBox1, EventArgs.Empty);
            }
        }

    }
}
