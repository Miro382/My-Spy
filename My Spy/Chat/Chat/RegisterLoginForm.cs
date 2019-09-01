using EncryptionLibrary;
using Saving;
using Sharp.Xmpp.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat
{
    public partial class RegisterLoginForm : Form
    {

        const string Server = "0nl1ne.cc";
        const string AppPrefix = "MySpy_";

        static string PathMS = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy";

        Encryption256 encrypt = new Encryption256("8B1JqdrHLD6qJIdZk9oWwSuWrpoPcw");
        Encryption encryptpass = new Encryption("Mo4onbrP3ovlTFbPQAq66XTs7Em0rXck");


        public RegisterLoginForm()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegisterForm register = new RegisterForm();
            register.Show();
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(textBoxuser.Text))
            {
                if (!string.IsNullOrWhiteSpace(textBoxpass.Text))
                {
                    try
                    {
                        string pass = encrypt.EncryptText(textBoxpass.Text, "ZuhajoLafyNAYjCAw2VZfaYo");
                        XmppClient client = new XmppClient(Server, AppPrefix+textBoxuser.Text, pass);
                        client.Connect();

                        Debug.WriteLine("Logged in as: "+client.Jid);


                        XmppClient clientad = new XmppClient(Server, "Admin_"+AppPrefix + textBoxuser.Text, pass);
                        clientad.Connect();

                        Debug.WriteLine("Logged in as: "+clientad.Jid);

                        Directory.CreateDirectory(PathMS + "/Settings/");

                        if (File.Exists(PathMS + "/Settings/chat.dat"))
                            File.Delete(PathMS + "/Settings/chat.dat");

                        SaveWriter save = new SaveWriter(PathMS + "/Settings/chat.dat");

                        save.AddItem("Username", AppPrefix + textBoxuser.Text);
                        save.AddItem("Password",encryptpass.EncryptString(pass, "1cPOnaLrV2MusS4Cq3aU6dja"));
                        save.AddItem("Admin",radioButtonAdmin.Checked);
                        save.Save();
                        save.Destroy();

                        pass = "";
                        MessageBox.Show("Logged in", "OK");
                        Application.Exit();

                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("User not exist, or bad password","Error");
                    }
                }
                else
                {
                    MessageBox.Show("Empty password");
                }
            }else
            {
                MessageBox.Show("Empty Username");
            }
        }


    }
}
