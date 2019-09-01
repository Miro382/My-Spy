using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * Miroslav Murin
 * Sluzi iba ako dekoracia na oddelenie
 * 
 */ 

namespace My_Spy_Administrator
{
    public partial class DecorativeLine : UserControl
    {

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public Color Color1
        {
            get { return decup.BackColor; }
            set { decup.BackColor = value; }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public Color Color2
        {
            get { return BackColor; }
            set { BackColor = value; }
        }

        public DecorativeLine()
        {
            InitializeComponent();
        }

        private void DecorativeLine_Load(object sender, EventArgs e)
        {
            decup.Size = new Size(decup.Size.Width,Size.Height/2);
        }

        public void SetColor(Color color1,Color color2)
        {
            decup.BackColor = color1;
            BackColor = color2;
        }
    }
}
