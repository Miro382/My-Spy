namespace Warning
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.labelHeader = new System.Windows.Forms.Label();
            this.labelText = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.imageButtonhide = new My_Spy_Administrator.ImageButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelHeader
            // 
            this.labelHeader.BackColor = System.Drawing.Color.Transparent;
            this.labelHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelHeader.ForeColor = System.Drawing.Color.White;
            this.labelHeader.Location = new System.Drawing.Point(39, 0);
            this.labelHeader.Name = "labelHeader";
            this.labelHeader.Size = new System.Drawing.Size(173, 32);
            this.labelHeader.TabIndex = 0;
            this.labelHeader.Text = "Header";
            this.labelHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelText
            // 
            this.labelText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelText.ForeColor = System.Drawing.Color.White;
            this.labelText.Location = new System.Drawing.Point(2, 37);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(246, 110);
            this.labelText.TabIndex = 1;
            this.labelText.Text = "Text";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.panel1.Controls.Add(this.imageButtonhide);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.labelHeader);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(250, 32);
            this.panel1.TabIndex = 3;
            // 
            // imageButtonhide
            // 
            this.imageButtonhide.BackgroundImage = global::Warning.Properties.Resources.hide;
            this.imageButtonhide.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.imageButtonhide.ClickImage = global::Warning.Properties.Resources.hideclick;
            this.imageButtonhide.HoverImage = global::Warning.Properties.Resources.hidehover;
            this.imageButtonhide.Location = new System.Drawing.Point(218, 1);
            this.imageButtonhide.Name = "imageButtonhide";
            this.imageButtonhide.NormalImage = global::Warning.Properties.Resources.hide;
            this.imageButtonhide.OnClickShowImage = true;
            this.imageButtonhide.Size = new System.Drawing.Size(30, 30);
            this.imageButtonhide.TabIndex = 5;
            this.imageButtonhide.Click += new System.EventHandler(this.imageButtonhide_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(1, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(30, 30);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.ClientSize = new System.Drawing.Size(250, 150);
            this.Controls.Add(this.labelText);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(250, 150);
            this.MinimumSize = new System.Drawing.Size(250, 32);
            this.Name = "Form1";
            this.ShowInTaskbar = false;
            this.Text = "My Spy Warning";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelHeader;
        private System.Windows.Forms.Label labelText;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private My_Spy_Administrator.ImageButton imageButtonhide;
    }
}

