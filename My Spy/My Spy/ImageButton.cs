using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


/*
 * 
 * Miroslav Murin
 * Image Button
 * ------------
 * Obrazok ktory funguje ako tlacidlo
 * s troma animaciami... normalny obrazok, ked je myska nad obrazkom, 
 * a po kliknuti
 * 
 */

namespace My_Spy_Administrator
{
    [DefaultEvent("Click")]
    public partial class ImageButton : UserControl
    {
        public Image normalImage, hoverImage, clickImage;
        public bool ClickImageShow = false;

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public Image NormalImage
        {
            get { return BackgroundImage; }
            set { BackgroundImage = value;
                normalImage = value;
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public Image HoverImage
        {
            get { return hoverImage; }
            set { hoverImage = value; }
        }


        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public Image ClickImage
        {
            get { return clickImage; }
            set { clickImage = value; }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public bool OnClickShowImage
        {
            get { return ClickImageShow; }
            set { ClickImageShow = value; }
        }

        public ImageButton()
        {
            InitializeComponent();
        }

        public void SetImages(Image Normalimage, Image Hoverimage)
        {
            OnClickShowImage = false;
            NormalImage = Normalimage;
            HoverImage = Hoverimage;
        }

        public void SetImages(Image Normalimage, Image Hoverimage, Image Clickimage)
        {
            OnClickShowImage = true;
            normalImage = Normalimage;
            hoverImage = Hoverimage;
            clickImage = Clickimage;
        }

        private void ImageButton_Load(object sender, EventArgs e)
        {
            normalImage = BackgroundImage;
        }


        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            BackgroundImage = normalImage;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            BackgroundImage = hoverImage;
        }


        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if ((e.Button == MouseButtons.Left) && ClickImageShow)
            {
                BackgroundImage = clickImage;
            }
        }


        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            BackgroundImage = hoverImage;
        }


    }
}
