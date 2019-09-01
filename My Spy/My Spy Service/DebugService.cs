using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace My_Spy_Service
{
    public static class DebugService
    {
        private static bool Debug = true,Error = true;

        public static void Write(string Text)
        {
            if (Debug)
            {
                using (StreamWriter str = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "/servicedebug.txt",true))
                {
                    str.WriteLine(Text);
                    str.Flush();
                    str.Close();
                }
            }
        }


        public static void WriteError(string Text)
        {
            if (Error)
            {
                using (StreamWriter str = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "/serviceerror.txt", true))
                {
                    str.WriteLine(Text);
                    str.Flush();
                    str.Close();
                }
            }
        }


    }
}
