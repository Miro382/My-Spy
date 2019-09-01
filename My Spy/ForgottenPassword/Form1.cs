using EncryptionLibrary;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace ForgottenPassword
{
    public partial class Form1 : Form
    {
        Encryption encryption = new Encryption("kmAAnmAbVFSO9pgA2sy5X9lZhn5TulKi74FDZYZw");
        private string securecode = "",toemail="";

        public Form1()
        {
            SetLanguage();
            InitializeComponent();
        }


        void SetLanguage()
        {
            try
            {
                RegistryKey registry = Registry.LocalMachine.OpenSubKey("Software\\My_Spy", false);
                string Lang = (string)registry.GetValue("Language");
                Debug.WriteLine("Language:" + Lang);
                if (!string.IsNullOrEmpty(Lang))
                {
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(Lang);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }


        //skontrolovat kod
        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Equals(securecode))
            {
                tabControl1.SelectedIndex = 1;
                panel2.Enabled = true;
            }else
            {
                MessageBox.Show(ResourcesFiles.ProgramStrings.Badcode,ResourcesFiles.ProgramStrings.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // ((Control)tabControl1.TabPages[1]).Enabled = false;

            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;

            RegistryKey registry = Registry.LocalMachine.OpenSubKey("Software\\My_Spy");
            toemail = encryption.DecryptString((string)registry.GetValue("Email"), "BNKh3wQVQy");

            label3.Text = ResourcesFiles.ProgramStrings.EmailTo + " " + toemail;
            if (timer1.Enabled == false)
            {
                securecode = HelpClass.RandomString(25);
                string url = "http://myspy.diodegames.eu/forgotpassword.php?pas=" + securecode + "&email=" + toemail;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                timer1.Interval = 300000;
                timer1.Enabled = true;
            }
        }


        //odoslat novy kod
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (timer1.Enabled == false)
            {
                DialogResult dialogResult = MessageBox.Show(ResourcesFiles.ProgramStrings.sendemail, ResourcesFiles.ProgramStrings.sendemailheader, MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    securecode = HelpClass.RandomString(25);
                    string url = "http://myspy.diodegames.eu/forgotpassword.php?pas=" + securecode + "&email="+ toemail;

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    timer1.Interval = 300000;
                    timer1.Enabled = true;

                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show(ResourcesFiles.ProgramStrings.attempt, ResourcesFiles.ProgramStrings.sendemailheader, MessageBoxButtons.OK);
            }
        }


        //zmena hesla
        private void button3_Click(object sender, EventArgs e)
        {
            if(textBox2.Text.Equals(textBox3.Text))
            {
                RegistryKey registry = Registry.LocalMachine.CreateSubKey("Software\\My_Spy");
                registry.SetValue("Properties", encryption.EncryptString(textBox2.Text, "2pqB7l4eRM6nHaZPtetkkHePguacGTvrMhvnE4fy"));
                textBox2.Text = "";
                textBox3.Text = "";
                MessageBox.Show(ResourcesFiles.ProgramStrings.PassChange, ResourcesFiles.ProgramStrings.PassChangeHeader);
                Application.Exit();
            }else
            {
                MessageBox.Show(ResourcesFiles.ProgramStrings.PasswordsNotEqual, ResourcesFiles.ProgramStrings.Error);
            }
        }


    }
}
