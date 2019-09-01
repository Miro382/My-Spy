using EncryptionLibrary;
using Sharp.Xmpp.Client;
using Sharp.Xmpp.Extensions;
using Sharp.Xmpp.Extensions.Dataforms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }
        // RegistrationCallback reg = new RegistrationCallback(registrationCallback);
        const string Server = "0nl1ne.cc";
        static string AppPrefix = "MySpy_";
        static string Username = "", Password = "";
        //bool 

        Encryption256 encrypt = new Encryption256("8B1JqdrHLD6qJIdZk9oWwSuWrpoPcw"); //password - ZuhajoLafyNAYjCAw2VZfaYo


        private void RegisterForm_Load(object sender, EventArgs e)
        {
        }

        //register
        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBoxpass.Text))
            {
                if (textBoxpass.Text.Equals(textBoxpassrepeat.Text))
                {
                    if(!string.IsNullOrWhiteSpace(textBoxuser.Text))
                    {

                        try
                        {
                            pictureBoxLoading.Visible = true;
                            panel1.Enabled = false;
                            //zaregistruje sa klasicky pouzivatel
                            Username = AppPrefix + textBoxuser.Text;
                            Password = encrypt.EncryptText(textBoxpass.Text, "ZuhajoLafyNAYjCAw2VZfaYo");

                            XmppClient xmpp = new XmppClient(Server);
                            xmpp.Connect();
                            Debug.WriteLine("Connected: "+xmpp.Connected+"  Auth: "+xmpp.Authenticated+"   JID: "+xmpp.Jid);
                            xmpp.Register(RegistrationCallback);
                            xmpp.Close();
                            xmpp.Dispose();


                            new Thread(() =>
                            {
                                Thread.CurrentThread.IsBackground = true;

                                Thread.Sleep(3000);
                                try
                                {
                                    XmppClient xmppa = new XmppClient(Server);
                                    xmppa.Connect();
                                    Username = "Admin_" + AppPrefix + textBoxuser.Text;
                                    Password = encrypt.EncryptText(textBoxpass.Text, "ZuhajoLafyNAYjCAw2VZfaYo");
                                    xmppa.Register(RegistrationCallback);
                                    xmppa.Close();

                                    this.Invoke((MethodInvoker)delegate {
                                        panel1.Enabled = true;
                                        pictureBoxLoading.Visible = false;

                                        MessageBox.Show("Registered! Login to continue");

                                        RegisterLoginForm register = new RegisterLoginForm();
                                        register.Show();
                                        Close();
                                    });

  
                                }catch(Exception ex)
                                {
                                    Debug.WriteLine("" + ex);

                                    this.Invoke((MethodInvoker)delegate {
                                        panel1.Enabled = true;
                                        pictureBoxLoading.Visible = false;

                                        MessageBox.Show("Account exist or something is bad!", "Error to register a new account");
                                    });
                                }
                            }).Start();
                            


                        }
                        catch(Exception ex)
                        {
                            Debug.WriteLine(""+ex);
                            MessageBox.Show("Account exist or something is bad!","Error to register a new account");

                            panel1.Enabled = true;
                            pictureBoxLoading.Visible = false;
                        }

                    }else
                    {
                        MessageBox.Show("Bad username");
                    }
                }
                else
                {
                    MessageBox.Show("Passwords not equal", "Bad password");
                }
            }else
            {
                MessageBox.Show("Bad Password");
            }

        }//button register

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegisterLoginForm register = new RegisterLoginForm();
            register.Show();
            Close();
        }


    static SubmitForm RegistrationCallback(RequestForm form)
    {

            if (!string.IsNullOrEmpty(form.Instructions))
            Debug.WriteLine(form.Instructions);


            SubmitForm submitForm = new SubmitForm();

            Debug.WriteLine(Username);

            TextField textuser = new TextField(form.Fields[0].Name, Username);
            submitForm.Fields.Add(textuser);


            TextField textpass = new TextField(form.Fields[1].Name, Password);
            submitForm.Fields.Add(textpass);


            Username = "";
            Password = "";

            return submitForm;
    }






    }
}
