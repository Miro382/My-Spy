using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace My_Spy_Service
{
    class NoteRemind
    {
        public string Path = "";
        public DateTime Datetime;

        public NoteRemind()
        {

        }

        public NoteRemind(string path,DateTime datetime)
        {
            Path = path;
            Datetime =  datetime;
        }

    }
}
