using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat
{
    public partial class SmilesForm : Form
    {
        public SmilesForm()
        {
            InitializeComponent();
        }


        public static Image GetSmileImage(int code)
        {
            switch(code)
            {
                case 1:
                    return Properties.Resources.smile1;
                case 2:
                    return Properties.Resources.smile2;
                case 3:
                    return Properties.Resources.smile3;
                case 4:
                    return Properties.Resources.smile4;
                case 5:
                    return Properties.Resources.smile5;
                case 6:
                    return Properties.Resources.smile6;
                case 7:
                    return Properties.Resources.smile7;
                case 8:
                    return Properties.Resources.smile8;
                case 9:
                    return Properties.Resources.smile9;
                case 10:
                    return Properties.Resources.smile10;
                case 11:
                    return Properties.Resources.smile11;
                case 12:
                    return Properties.Resources.smile12;
                case 13:
                    return Properties.Resources.Smile13;
                case 14:
                    return Properties.Resources.Smile14;
                case 15:
                    return Properties.Resources.Smile15;
                case 16:
                    return Properties.Resources.Smile16;
                default:
                    return null;
            }

        }



        private void buttonSmile_Click(object sender, EventArgs e)
        {
            Button butt = (Button)sender;
            Form1.SmileCode = "[*"+(string)butt.Tag+"*]";
            Form1.Smile = true;
            Form1.Smileim = GetSmileImage(int.Parse((string)butt.Tag));
            Close();
        }
    }
}
