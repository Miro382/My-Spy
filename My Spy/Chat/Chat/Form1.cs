using Chat.Xmpp;
using EncryptionLibrary;
using Saving;
using Sharp.Xmpp.Im;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Chat
{
    public partial class Form1 : Form
    {
        bool Administrator = false;

        XmppConnection connection = new XmppConnection();
        XmppMessage message = new XmppMessage("l5wja9bqvTiFsTzHO77eir5js");
        Encryption encryptpass = new Encryption("Mo4onbrP3ovlTFbPQAq66XTs7Em0rXck");

        static string PathMS = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy";

        public static string SmileCode = "";
        public static bool Smile = false;
        public static Image Smileim;

        string ReceiverJID = "";

        public Form1()
        {
            InitializeComponent();


           textBoxMessage.KeyDown += new KeyEventHandler(textBoxMessage_KeyDown);
            textBoxMessage.KeyPress += new KeyPressEventHandler(keypressed);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            if (!File.Exists(PathMS + "/Settings/chat.dat"))
            {

                    MessageBox.Show("Login or register to continue.", "Not logged in");
                    RegisterLoginForm frm = new RegisterLoginForm();
                    frm.Show();
            }
            else
            {
                SaveWriter save = new SaveWriter(PathMS + "/Settings/chat.dat");

                save.Load();
                string usr = save.GetItem("Username");
                string pass = encryptpass.DecryptString(save.GetItem("Password"), "1cPOnaLrV2MusS4Cq3aU6dja");

                Administrator = save.GetItemBool("Admin");


                if (Administrator)
                    label1.Visible = true;



                if (Administrator)
                {
                    ReceiverJID = usr + "@" + XmppConnection.Server;
                    usr = "Admin_" + usr;
                }
                else
                {
                    ReceiverJID = "Admin_" + usr + "@" + XmppConnection.Server;
                }


                //connection.Login(usr, pass);
                //connection.Connect();


                connection.ConnectToXMPPIM(usr, pass);

                /*
                XmppIm xmppim = new XmppIm("", "", "");
                xmppim;*/

                //Debug.WriteLine(connection.GetXmppClient().Authenticated);
                //Debug.WriteLine(connection.IsConnected());

                Debug.WriteLine("Connected as " + connection.GetXmppIm().Jid);
                connection.GetXmppIm().Message += Connection_Message;


                Debug.WriteLine("Size: " + connection.Rostersize());
                connection.AddToRoster(ReceiverJID);

                connection.GetXmppIm().RequestSubscription(ReceiverJID);
                connection.ApproveRequest(ReceiverJID);

                connection.GetXmppIm().Status += Connection_Status;


                Debug.WriteLine("ReceiverJID: " + ReceiverJID);

            }
            /*
            RegisterLoginForm frm = new RegisterLoginForm();
            frm.Show();
            */
        }

        private void Connection_Status(object sender, StatusEventArgs e)
        {
            Debug.WriteLine("JID: " + e.Jid.GetBareJid() + " Status: " + e.Status.Availability);

            if (e.Jid.GetBareJid() == ReceiverJID)
            {
                if (e.Status.Availability == Availability.Online)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        pictureBoxONOFF.Image = Properties.Resources.on;
                        labelONOFF.Text = "ON";

                    });
                }
                else
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        pictureBoxONOFF.Image = Properties.Resources.off;
                        labelONOFF.Text = "OFF";
                    });
                }
            }
        }

        private void Connection_Message(object sender, Sharp.Xmpp.Im.MessageEventArgs e)
        {
            Debug.WriteLine(""+sender+"   JID: "+e.Jid+"   Message: "+e.Message.Body);

            string txt = message.DecryptMessage(e.Message.Body);


            if (txt[0] == '[' && txt[1] == '*')
            {
                try
                {
                    txt = txt.Replace("[*", "");
                    txt = txt.Replace("*]", "");
                    Smileim = SmilesForm.GetSmileImage(int.Parse(txt));

                    PictureBox pic = new PictureBox();
                    pic.Image = Smileim;
                    pic.Size = new Size(128, 128);
                    this.Invoke((MethodInvoker)delegate
                    {
                        flowLayoutPanelMessages.Controls.Add(pic);
                    flowLayoutPanelMessages.VerticalScroll.Value = flowLayoutPanelMessages.VerticalScroll.Maximum;
                    });
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("" + ex);
                }
            }
            else
            {

                Label lab = new Label();
                lab.Text = txt;
                lab.Font = new System.Drawing.Font(lab.Font.FontFamily, 14);
                lab.ForeColor = System.Drawing.Color.White;
                lab.AutoSize = true;
                this.Invoke((MethodInvoker)delegate
                {
                    flowLayoutPanelMessages.Controls.Add(lab);
                    flowLayoutPanelMessages.VerticalScroll.Value = flowLayoutPanelMessages.VerticalScroll.Maximum;
                });
            }
        }

        private void textBoxMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendMessage();
            }
        }

        private void keypressed(Object o, KeyPressEventArgs e)
        {
            if (e.KeyChar ==  (char)Keys.Enter)
            {
                e.Handled = true;
            }
        }

        private void imageButtonSend_Click(object sender, EventArgs e)
        {
            SendMessage();
        }



        void SendMessage()
        {
            if (!string.IsNullOrWhiteSpace(textBoxMessage.Text))
            {

                message.SendMessage(connection.GetXmppIm(), ReceiverJID, textBoxMessage.Text);
                Label lab = new Label();
                lab.Text = textBoxMessage.Text;
                lab.Font = new System.Drawing.Font(lab.Font.FontFamily, 14);
                lab.ForeColor = System.Drawing.Color.Orange;
                lab.AutoSize = true;
                flowLayoutPanelMessages.Controls.Add(lab);
                textBoxMessage.Text = "";
                flowLayoutPanelMessages.VerticalScroll.Value = flowLayoutPanelMessages.VerticalScroll.Maximum;
            }
        }

        private void imageButton1_Click(object sender, EventArgs e)
        {
            SmilesForm smilef = new SmilesForm();
            smilef.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (Smile)
            {
                Smile = false;
                message.SendMessage(connection.GetXmppIm(), ReceiverJID, SmileCode);
                PictureBox pic = new PictureBox();
                pic.Image = Smileim;
                pic.Size = new Size(128, 128);
                flowLayoutPanelMessages.Controls.Add(pic);
                flowLayoutPanelMessages.VerticalScroll.Value = flowLayoutPanelMessages.VerticalScroll.Maximum;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (connection.GetXmppIm()!=null)
            {
                connection.SetONIM(true);
                timer2.Interval = 15000;
            }
        }
    }
}
