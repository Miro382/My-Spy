namespace My_Spy_Administrator
{
    partial class Toogle_Box
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Header = new System.Windows.Forms.Panel();
            this.pictureBoxImage = new System.Windows.Forms.PictureBox();
            this.HeaderText = new System.Windows.Forms.Label();
            this.ButtonToogle = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ButtonToogle)).BeginInit();
            this.SuspendLayout();
            // 
            // Header
            // 
            this.Header.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.Header.Controls.Add(this.pictureBoxImage);
            this.Header.Controls.Add(this.HeaderText);
            this.Header.Controls.Add(this.ButtonToogle);
            this.Header.Location = new System.Drawing.Point(0, 0);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(250, 32);
            this.Header.TabIndex = 1;
            // 
            // pictureBoxImage
            // 
            this.pictureBoxImage.Location = new System.Drawing.Point(4, 4);
            this.pictureBoxImage.Name = "pictureBoxImage";
            this.pictureBoxImage.Size = new System.Drawing.Size(24, 24);
            this.pictureBoxImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxImage.TabIndex = 2;
            this.pictureBoxImage.TabStop = false;
            // 
            // HeaderText
            // 
            this.HeaderText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HeaderText.ForeColor = System.Drawing.Color.White;
            this.HeaderText.Location = new System.Drawing.Point(34, 0);
            this.HeaderText.Name = "HeaderText";
            this.HeaderText.Size = new System.Drawing.Size(178, 32);
            this.HeaderText.TabIndex = 1;
            this.HeaderText.Text = "Toogle box";
            this.HeaderText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.HeaderText.Click += new System.EventHandler(this.HeaderText_Click);
            // 
            // ButtonToogle
            // 
            this.ButtonToogle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonToogle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ButtonToogle.Location = new System.Drawing.Point(218, 0);
            this.ButtonToogle.Name = "ButtonToogle";
            this.ButtonToogle.Size = new System.Drawing.Size(32, 32);
            this.ButtonToogle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ButtonToogle.TabIndex = 0;
            this.ButtonToogle.TabStop = false;
            this.ButtonToogle.Click += new System.EventHandler(this.ButtonToogle_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 30;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Toogle_Box
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Header);
            this.Name = "Toogle_Box";
            this.Size = new System.Drawing.Size(250, 200);
            this.Load += new System.EventHandler(this.Toogle_Box_Load);
            this.Header.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ButtonToogle)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox ButtonToogle;
        private System.Windows.Forms.Panel Header;
        private System.Windows.Forms.Label HeaderText;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBoxImage;
    }
}
