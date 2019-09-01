using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace My_Spy
{
    [DefaultEvent("Click")]
    public partial class LongButton : UserControl
    {

        Color color1 = Color.White, color2 = Color.Gray, color1h = Color.WhiteSmoke, color2h = Color.WhiteSmoke;

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string TextLabel
        {
            get { return label1.Text; }
            set
            {
                label1.Text = value;
            }
        }


        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public Image image
        {
            get { return pictureBox1.Image; }
            set
            {
                pictureBox1.Image = value;
            }
        }



        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public Color Color1
        {
            get { return color1; }
            set
            {
                color1 = value;
                BackColor = value;
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public Color Color1Hover
        {
            get { return color1h; }
            set
            {
                color1h = value;
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public Color Color2
        {
            get { return color2; }
            set
            {
                color2 = value;
                panel1.BackColor = value;
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public Color Color2Hover
        {
            get { return color2h; }
            set
            {
                color2h = value;
            }
        }


        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            MLeave();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            MEnter();
        }


        public LongButton()
        {
            InitializeComponent();

            label1.MouseEnter += OnMouseEnter;
            label1.MouseLeave += OnMouseLeave;

            pictureBox1.MouseEnter += OnMouseEnter;
            pictureBox1.MouseLeave += OnMouseLeave;

            panel1.MouseEnter += OnMouseEnter;
            panel1.MouseLeave += OnMouseLeave;

            pictureBox1.Click += Control_Click;
            label1.Click += Control_Click;
            panel1.Click += Control_Click;
        }

        private void Control_Click(object sender, EventArgs e)
        {
            OnClick(EventArgs.Empty);
        }

        private void MLeave()
        {
            BackColor = color1;
            panel1.BackColor = color2;
        }

        private void LongButton_Load(object sender, EventArgs e)
        {

        }

        private void MEnter()
        {
            BackColor = color1h;
            panel1.BackColor = color2h;
        }


        private void OnMouseLeave(object sender, EventArgs e)
        {
            MLeave();
        }

        private void OnMouseEnter(object sender, EventArgs e)
        {
            MEnter();
        }
    }
}
