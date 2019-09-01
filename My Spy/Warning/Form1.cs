using System;
using System.Drawing;
using System.Windows.Forms;

namespace Warning
{
    public partial class Form1 : Form
    {
        Rectangle rec;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            rec = Screen.FromControl(this).Bounds;

           string[] args = Environment.GetCommandLineArgs();
            Location = new Point(rec.Width - 250, Screen.PrimaryScreen.WorkingArea.Bottom - 150);

            if (args.Length > 1)
            {
                if (args[1].Equals("keylogger"))
                {
                    pictureBox1.Image = Properties.Resources.keyboard;
                    labelHeader.Text = ResourcesFiles.ResourceStrings.KeyHeader;
                    labelText.Text = ResourcesFiles.ResourceStrings.KeyText;
                }
            }
            else
            {
                Application.Exit();
            }

        }

        private void imageButtonhide_Click(object sender, EventArgs e)
        {
            if (Size.Height != 32)
            {
                Size = new Size(250, 32);
                Location = new Point(rec.Width - 250, Screen.PrimaryScreen.WorkingArea.Bottom - 32);
                imageButtonhide.SetImages(Properties.Resources.show, Properties.Resources.showhover, Properties.Resources.showclick);
            }else
            {
                Size = new Size(250, 150);
                Location = new Point(rec.Width - 250, Screen.PrimaryScreen.WorkingArea.Bottom - 150);
                imageButtonhide.SetImages(Properties.Resources.hide, Properties.Resources.hidehover, Properties.Resources.hideclick);
            }
        }

    }
}
