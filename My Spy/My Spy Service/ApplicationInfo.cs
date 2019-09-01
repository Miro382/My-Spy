using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace My_Spy_Service
{
    public class ApplicationInfo
    {
        public string ApplicationName = "", ExecutableName = "";
        private int Minutes = 0, Hours = 0;

        public ApplicationInfo(string Applicationname, string Executablename)
        {
            ApplicationName = Applicationname;
            ExecutableName = Executablename;
            Minutes = 5;
        }

        public void SetTime(int SetHours, int SetMinutes)
        {
            Hours = SetHours;
            Minutes = SetMinutes;
        }

        public void AddTime(int AddMinutes)
        {
            Minutes += AddMinutes;
            if (Minutes >= 60)
            {
                Minutes -= 60;
                Hours++;
            }
        }

        public void CalcTime()
        {
            if (Minutes >= 60)
            {
                Minutes -= 60;
                Hours++;
            }
        }


        public int GetMinutes()
        {
            return Minutes;
        }

        public int GetHours()
        {
            return Hours;
        }

    }
}
