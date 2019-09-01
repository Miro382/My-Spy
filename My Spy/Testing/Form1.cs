using Microsoft.Win32;
using System;
using System.Windows.Forms;
using TrotiNet;

namespace Testing
{


    public partial class Form1 : Form
    {

        //HttpListener listener;
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            


            int port = 8333;
            const bool bUseIPv6 = false;

            TcpServer Server = new TcpServer(port, bUseIPv6);

            
            Server.Start(TransparentProxy.CreateProxy);

            

                /*

                string outputFile = "FadeBetweenImages.wmv";
                using (ITimeline timeline = new DefaultTimeline())
                {
                    IGroup group = timeline.AddVideoGroup(32, 160, 100);
                    ITrack videoTrack = group.AddTrack();
                    IClip clip1 = videoTrack.AddImage("image.jpg", 0, 2); // play first image for a little while
                    IClip clip2 = videoTrack.AddImage("image.jpg", 0, 2); // and the next
                    IClip clip3 = videoTrack.AddImage("image.jpg", 0, 2); // and finally the last
                    IClip clip4 = videoTrack.AddImage("image.jpg", 0, 2); // and finally the last


                    IRenderer renderer = new WindowsMediaRenderer(timeline, outputFile, WindowsMediaProfiles.HighQualityVideo);

                   renderer.Render();
                }

                */

                //Server.Start(RedirectingProxy.CreateProxy);
                //Server.Start(RewritingProxy.CreateProxy);

                /*
                Server.InitListenFinished.WaitOne();
                if (Server.InitListenException != null)
                    throw Server.InitListenException;

                while (true)
                    System.Threading.Thread.Sleep(1000);
                    */
            }

       
        private void timer1_Tick(object sender, EventArgs e)
        {
        }


    }
    

    public class TransparentProxy : ProxyLogic
    {
        public TransparentProxy(HttpSocket clientSocket)
            : base(clientSocket) { }

        static new public TransparentProxy CreateProxy(HttpSocket clientSocket)
        {
            return new TransparentProxy(clientSocket);
        }

        protected override void OnReceiveRequest()
        {


            //  Console.WriteLine("-> " + RequestLine + " from HTTP referer " +
            //    RequestHeaders.Referer);
            Uri myUri = new Uri(RequestLine.URI);
            string host = myUri.Host;
            Console.WriteLine("URI: " + host);//RequestHeaders.Host);


        }

        protected override void OnReceiveResponse()
        {
            Console.WriteLine("URI: " + RequestLine.URI);

            /*   
               Console.WriteLine("<- " + ResponseStatusLine +
                   " with HTTP Content-Length: " +
                   (ResponseHeaders.ContentLength ?? 0));

                   */
        }
    }


}
