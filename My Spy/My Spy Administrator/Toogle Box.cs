using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel.Design;
/*
 * Miroslav Murin
 * Toogle box
 */

namespace My_Spy_Administrator
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class Toogle_Box : UserControl
    {
        private Image Up, Down;
        private bool hidden = false,animation=true;
        private Size size;
        private bool AutoScrollDefault = false;
        private Color headershow = Color.FromArgb(52, 73, 94), headerhide = Color.FromArgb(44, 62, 80);
        private int AnimSpeed = 70;

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public Image UpImage
        {
            get { return Up; }
            set { Up = value; }
        }


        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public Image DownImage
        {
            get { return Down; }
            set { Down = value; }
        }



        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public Image PictureBoxImage
        {
            get { return pictureBoxImage.Image; }
            set { pictureBoxImage.Image = value; }
        }



        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string HeaderTitle
        {
            get { return HeaderText.Text; }
            set { HeaderText.Text = value; }
        }


        public Toogle_Box()
        {
            InitializeComponent();
        }

        private void ButtonToogle_Click(object sender, EventArgs e)
        {
            Toogle();
        }


        //skryt/ukazat
        public void Toogle()
        {
            if (!hidden)
            {
                hidden = true;
                ButtonToogle.Image = Down;
                // this.Size = new Size(size.Width, 32);
                timer1.Enabled = true;
                AutoScroll = false;
                Header.BackColor = headerhide;
            }
            else
            {
                hidden = false;
                ButtonToogle.Image = Up;
                //this.Size = size;
                timer1.Enabled = true;
                AutoScroll = AutoScrollDefault;
                Header.BackColor = headershow;
            }
        }

        public void Animation(bool Set, int AnimationSpeed)
        {
            animation = Set;
            AnimSpeed = AnimationSpeed;
        }

        public void SetImages(Image UpImage, Image DownImage,Image Icon)
        {
            Up = UpImage;
            Down = DownImage;
            ButtonToogle.Image = Up;
            pictureBoxImage.Image = Icon;
        }

        public bool IsHidden()
        {
            return hidden;
        }

        public void SetHeaderColor(Color SetColorNormal, Color SetColorHidden)
        {
            Header.BackColor = SetColorNormal;
            headershow = SetColorNormal;
            headerhide = SetColorHidden;
        }

        public void SetHeaderTextColor(Color SetColor)
        {
            HeaderText.ForeColor = SetColor;
        }


        private void Toogle_Box_Load(object sender, EventArgs e)
        {
            size = this.Size;
            Header.Size = new Size(size.Width,32);
            AutoScrollDefault = this.AutoScroll;

            ButtonToogle.Image = Up;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (hidden)
            {
                if (Size.Height >32)
                {
                    int min = AnimSpeed;
                    if((Size.Height - AnimSpeed)<32)
                    {
                        min = Size.Height - 32;
                    }

                    Size = new Size(Size.Width, Size.Height - min);
                }
                else
                {
                    Size = new Size(Size.Width, 32);
                    timer1.Enabled = false;
                }

            }else
            {
                
                if (Size.Height < size.Height)
                {
                    
                    int min = AnimSpeed;
                    
                    if ((Size.Height + AnimSpeed) > size.Height)
                    {
                        min = size.Height-Size.Height;
                    }
                    

                    Size = new Size(Size.Width, Size.Height + min);
                }
                else
                {
                    Size = size;
                    timer1.Enabled = false;
                }
            }

                
        }

        private void HeaderText_Click(object sender, EventArgs e)
        {
            Toogle();
        }

        public void SetHeaderText(string Text)
        {
            HeaderText.Text = Text;
        }

    }
}
