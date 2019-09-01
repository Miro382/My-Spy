using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using TrotiNet;

namespace My_Spy_Worker
{

    public static class InternetReports
    {
        public static StreamWriter save;
        public static string Last = "",Last2 = "",Last3="";
        public static List<string> WebBlock = new List<string>();
        public static bool BlockSites = false, BlockSocialSites = false;
    }


        public class TransparentProxy : ProxyLogic
        {

        Uri myUri;
        string txt;

        public TransparentProxy(HttpSocket clientSocket)
                : base(clientSocket) { }

            static new public TransparentProxy CreateProxy(HttpSocket clientSocket)
            {
                return new TransparentProxy(clientSocket);
            }


            protected override void OnReceiveRequest()
            {

            if (InternetReports.BlockSites)
            {
                for (int i = 0; i < InternetReports.WebBlock.Count; i++)
                {
                    if (RequestLine.URI.Contains(InternetReports.WebBlock[i]))
                    {
                        SocketBP.Send403();
                        State.NextStep = AbortRequest;
                    }
                }
            }

            if (InternetReports.BlockSocialSites)
            {
                    if (RequestLine.URI.Contains("facebook.com") || RequestLine.URI.Contains("twitter.com") 
                    || RequestLine.URI.Contains("instagram.com") || RequestLine.URI.Contains("myspace.com")
                    || RequestLine.URI.Contains("plus.google.com"))
                    {
                        SocketBP.Send403();
                        State.NextStep = AbortRequest;
                    }
            }


            if (!string.IsNullOrWhiteSpace(RequestHeaders.Referer))
            {
                txt = RequestHeaders.Referer;
            }
            else
            {
                //myUri = new Uri(RequestLine.URI);
                //txt = myUri.Host;
                txt = RequestLine.URI;
                if(txt.Contains("localhost"))
                {
                    txt = "";
                }
            }


                if (!string.IsNullOrWhiteSpace(txt))
                {
                    //Debug.WriteLine("txt: "+txt+"  Last: "+InternetReports.Last);

                    if (!txt.Equals(InternetReports.Last) && !txt.Equals(InternetReports.Last2) && !txt.Equals(InternetReports.Last3) && !txt.Equals("localhost"))
                    {
                        InternetReports.save.WriteLine(DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + " : " + txt);
                        InternetReports.Last3 = InternetReports.Last2;
                        InternetReports.Last2 = InternetReports.Last;
                        InternetReports.Last = txt;
                        InternetReports.save.Flush();
                    }
                }
                    

            }




            protected override void OnReceiveResponse()
            {

            }


        }
}
