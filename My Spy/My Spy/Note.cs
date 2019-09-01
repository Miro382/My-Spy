using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace My_Spy
{
    [DefaultEvent("Click")]
    public partial class Note : UserControl
    {
        Color color1 = Color.FromArgb(250, 227, 0), color2 = Color.FromArgb(250, 238, 122);
        short draw = 0;
        public int HoverAlpha = 100 , ClickAlpha = 170;

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public Color Color1
        {
            get { return color1; }
            set { color1 = value; }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public Color Color2
        {
            get { return color2; }
            set { color2 = value; }
        }


        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public Image Pin
        {
            get { return pictureBox1.Image; }
            set { pictureBox1.Image = value; }
        }


        /*
        public new event EventHandler Click
        {
            add
            {
                base.Click += value;
                foreach (Control control in Controls)
                {
                    control.Click += value;
                    label.Click += value;
                }
            }
            remove
            {
                base.Click -= value;
                foreach (Control control in Controls)
                {
                    control.Click -= value;
                }
            }
        }
        


        public new event EventHandler OnClick
        {
            add
            {

                base.Click += value;
                foreach (Control control in Controls)
                {
                    control.Click += value;
                    label.Click += value;
                }
            }
            remove
            {
                base.Click -= value;
                foreach (Control control in Controls)
                {
                    control.Click -= value;
                }
            }
        }
        */

        private void WireAllControls(Control cont)
        {
            foreach (Control ctl in cont.Controls)
            {
                ctl.Click += ctl_Click;
                if (ctl.HasChildren)
                {
                    WireAllControls(ctl);
                }
            }
        }


        private void ctl_Click(object sender, EventArgs e)
        {
            this.InvokeOnClick(this, EventArgs.Empty);
        }


        private void Note_Load(object sender, EventArgs e)
        {
            WireAllControls(this);
            panel1.MouseEnter += Panel1_MouseEnter;
            panel1.MouseLeave += Panel1_MouseLeave;
            label.MouseEnter += Panel1_MouseEnter;
            label.MouseLeave += Panel1_MouseLeave;
            panel1.MouseDown += Panel1_MouseDown;
            panel1.MouseUp += Panel1_MouseUp;
            label.MouseDown += Panel1_MouseDown;
            label.MouseUp += Panel1_MouseUp;
        }





        public Note()
        {
            InitializeComponent();
        }

        public void SetPinImage(Image image)
        {
            pictureBox1.Image = image;
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle,
                                                           color1,
                                                           color2,
                                                           90F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }

            if (draw==1)
            {
                using (Brush brushn = new SolidBrush(Color.FromArgb(HoverAlpha, 255, 255, 255)))
                {
                    e.Graphics.FillRectangle(brushn, 0, 0, this.Width, this.Height);

                }
            }else if(draw == 2)
            {
                using (Brush brushn = new SolidBrush(Color.FromArgb(ClickAlpha, 255, 255, 255)))
                {
                    e.Graphics.FillRectangle(brushn, 0, 0, this.Width, this.Height);

                }

            }
            else
            {

            }
        }


        public void SetNoteText(string Text)
        { 
            
            if (Text.Length > 90)
            {
                Text = Text.Remove(90);
                Text += "...";
            }
            

            label.Text = Text;
        }




        private void Panel1_MouseUp(object sender, MouseEventArgs e)
        {
            draw = 1;
            panel1.Invalidate();
            panel1.Refresh();
        }

        private void Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            draw = 2;
            panel1.Invalidate();
            panel1.Refresh();
        }

        private void Panel1_MouseLeave(object sender, EventArgs e)
        {
            draw = 0;
            panel1.Invalidate();
            panel1.Refresh();
        }

        private void Panel1_MouseEnter(object sender, EventArgs e)
        {
            draw = 1;
            panel1.Invalidate();
            panel1.Refresh();
        }


    }
}
