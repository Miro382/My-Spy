using EncryptionLibrary;
using Sharp.Xmpp.Client;
using Sharp.Xmpp.Im;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Miroslav Murin


namespace Chat.Xmpp
{
    class XmppMessage
    {
        Encryption encryption = new Encryption("lo6Edpmn0eyFeofyhDk3ParMERJbPp4lREcps3Ma");
        private string Password = "123456";
        private bool encrypt = true;

        /// <summary>
        /// Encrypt and decrypt messages with password
        /// </summary>
        /// <param name="EncryptionPassword">Password for encrypt</param>
        public XmppMessage(string EncryptionPassword)
        {
            Password = EncryptionPassword;
            encrypt = true;
        }

        /// <summary>
        /// Classic messages with no encrypt
        /// </summary>
        public XmppMessage()
        {
            Password = "";
            encrypt = false;
        }

        public void SendMessage(XmppClient Client ,string To, string Message)
        {
            if(encrypt)
            Client.SendMessage(To, encryption.EncryptString(Message, Password));
            else
            Client.SendMessage(To, Message);
        }

        public void SendMessage(XmppIm Client, string To, string Message)
        {
            if (encrypt)
                Client.SendMessage(To, encryption.EncryptString(Message, Password));
            else
                Client.SendMessage(To, Message);
        }

        public void SendMessage(XmppConnection Connection, string To, string Message, bool IM)
        {
            if (!IM)
            {
                if (encrypt)
                    Connection.GetXmppClient().SendMessage(To, encryption.EncryptString(Message, Password));
                else
                    Connection.GetXmppClient().SendMessage(To, Message);
            }else
            {
                if (encrypt)
                    Connection.GetXmppIm().SendMessage(To, encryption.EncryptString(Message, Password));
                else
                    Connection.GetXmppIm().SendMessage(To, Message);
            }
        }

        public string DecryptMessage(string Message)
        {
            return encryption.DecryptString(Message, Password);
        }

    }
}
