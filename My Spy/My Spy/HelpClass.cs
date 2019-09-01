using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace My_Spy
{
    class HelpClass
    {
        private static Random random = new Random();
        public static bool NotesChange = false;

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string BoolToYesNo(bool boolean)
        {
            if(boolean)
            {
                return ResourcesFiles.ProgramStrings.Yes;
            }else
            {
                return ResourcesFiles.ProgramStrings.No;
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


    }
}
