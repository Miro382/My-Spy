using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * Miroslav Murin
 * My Spy Administrator
 * Help Class
 * -----
 * Pomocna staticka classa pre funkcie
 * -----
 */

namespace My_Spy_Administrator
{
    public static class HelpClass
    {
        public static int PCHour = 0, PCMin = 0;
        


        public static float BytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }


        public static bool IsValidEmail(string email)
        {
            try
            {
                MailAddress address = new MailAddress(email);
                Debug.Write(address);
                return address.Address == email;
            }
            catch
            {
                return false;
            }
        }



        public static void RestartService(string ServiceName,int TimeoutSeconds)
        {
            try
            {
                ServiceController service = new ServiceController(ServiceName);
                service.Refresh();

                if (service.Status == ServiceControllerStatus.Running)
                {
                    TimeSpan timeout = TimeSpan.FromSeconds(TimeoutSeconds);

                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);


                    service.Start();
                    service.WaitForStatus(ServiceControllerStatus.Running, timeout);
                }else
                {
                    TimeSpan timeout = TimeSpan.FromSeconds(TimeoutSeconds);

                    service.Start();
                    service.WaitForStatus(ServiceControllerStatus.Running, timeout);
                }
            }
            catch
            {
                
            }
        }



        public static string GETHtml(string Url)
        {
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(Url);
            myRequest.Method = "GET";
            WebResponse myResponse = myRequest.GetResponse();
            StreamReader sr = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            string result = sr.ReadToEnd();
            sr.Close();
            myResponse.Close();

            return result;
        }


        public static void SendNotification(string Text, string Email)
        {
            string url = "http://myspy.diodegames.eu/sendnotification.php?text=" + Text + "&email=" + Email;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        }


    }//koniec Help Class
}
