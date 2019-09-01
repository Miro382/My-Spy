using Sharp.Xmpp;
using Sharp.Xmpp.Client;
using Sharp.Xmpp.Extensions;
using Sharp.Xmpp.Im;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Miroslav Murin


namespace Chat
{
    class XmppConnection
    {
        public const string Server = "0nl1ne.cc";
        private XmppClient client;
        private XmppIm xmppim;


        public void Login(string Username, string Password)
        {
            client = new XmppClient(Server, Username, Password);
        }

        public void Connect()
        {
            client.Connect();
        }


        public bool IsConnected()
        {
            return client.Connected;
        }

        public void SetON(bool ON)
        {
            if (IsConnected())
            {
                if (ON)
                    client.SetStatus(Availability.Online);
                else
                    client.SetStatus(Availability.Offline);
            }
        }

        public XmppClient GetXmppClient()
        {
            return client;
        }


        //XMPP IM


        public void SetONIM(bool ON)
        {
            if (xmppim.Connected)
            {
                if (ON)
                    xmppim.SetStatus(Availability.Online);
                else
                    xmppim.SetStatus(Availability.Offline);
            }
        }

        public void ConnectToXMPPIM(string Username, string Password)
        {
            xmppim = new XmppIm(Server, Username, Password);
            xmppim.Connect();
        }

        public XmppIm GetXmppIm()
        {
            return xmppim;
        }

        public void AddToRoster(Jid jid)
        {
            xmppim.AddToRoster(new RosterItem(jid));
        }

        public void ApproveRequest(Jid jid)
        {
            xmppim.ApproveSubscriptionRequest(jid);
        }

        public int Rostersize()
        {
           return xmppim.GetRoster().Count;
        }

    }
}
