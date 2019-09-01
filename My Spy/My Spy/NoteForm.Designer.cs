namespace My_Spy
{
    partial class NoteForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NoteForm));
            this.labelText = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxpassword = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.imageButtonfavorite = new My_Spy_Administrator.ImageButton();
            this.imageButtonAlert = new My_Spy_Administrator.ImageButton();
            this.imageButton4 = new My_Spy_Administrator.ImageButton();
            this.imageButton3 = new My_Spy_Administrator.ImageButton();
            this.imageButton2 = new My_Spy_Administrator.ImageButton();
            this.imageButton1 = new My_Spy_Administrator.ImageButton();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelText
            // 
            this.labelText.AutoSize = true;
            this.labelText.BackColor = System.Drawing.Color.Transparent;
            this.labelText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelText.Location = new System.Drawing.Point(1, 1);
            this.labelText.MaximumSize = new System.Drawing.Size(500, 0);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(39, 20);
            this.labelText.TabIndex = 0;
            this.labelText.Text = "Text";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.labelText);
            this.panel1.Location = new System.Drawing.Point(12, 88);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(525, 460);
            this.panel1.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxpassword);
            this.groupBox1.Location = new System.Drawing.Point(12, 550);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 42);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Password";
            this.groupBox1.Visible = false;
            // 
            // textBoxpassword
            // 
            this.textBoxpassword.Location = new System.Drawing.Point(6, 14);
            this.textBoxpassword.Name = "textBoxpassword";
            this.textBoxpassword.PasswordChar = '●';
            this.textBoxpassword.Size = new System.Drawing.Size(214, 20);
            this.textBoxpassword.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::My_Spy.Properties.Resources.pinbig;
            this.pictureBox1.Location = new System.Drawing.Point(243, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 60);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // imageButtonfavorite
            // 
            this.imageButtonfavorite.BackgroundImage = global::My_Spy.Properties.Resources.starnone;
            this.imageButtonfavorite.ClickImage = global::My_Spy.Properties.Resources.starnoneclick;
            this.imageButtonfavorite.HoverImage = global::My_Spy.Properties.Resources.starnonehover;
            this.imageButtonfavorite.Location = new System.Drawing.Point(313, 550);
            this.imageButtonfavorite.Name = "imageButtonfavorite";
            this.imageButtonfavorite.NormalImage = global::My_Spy.Properties.Resources.starnone;
            this.imageButtonfavorite.OnClickShowImage = true;
            this.imageButtonfavorite.Size = new System.Drawing.Size(48, 48);
            this.imageButtonfavorite.TabIndex = 11;
            this.imageButtonfavorite.Click += new System.EventHandler(this.imageButtonfavorite_Click);
            // 
            // imageButtonAlert
            // 
            this.imageButtonAlert.BackgroundImage = global::My_Spy.Properties.Resources.alert;
            this.imageButtonAlert.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.imageButtonAlert.ClickImage = global::My_Spy.Properties.Resources.alertclick;
            this.imageButtonAlert.HoverImage = global::My_Spy.Properties.Resources.alerthover;
            this.imageButtonAlert.Location = new System.Drawing.Point(265, 554);
            this.imageButtonAlert.Name = "imageButtonAlert";
            this.imageButtonAlert.NormalImage = global::My_Spy.Properties.Resources.alert;
            this.imageButtonAlert.OnClickShowImage = true;
            this.imageButtonAlert.Size = new System.Drawing.Size(42, 42);
            this.imageButtonAlert.TabIndex = 10;
            this.imageButtonAlert.Visible = false;
            this.imageButtonAlert.Click += new System.EventHandler(this.imageButtonAlert_Click);
            // 
            // imageButton4
            // 
            this.imageButton4.BackgroundImage = global::My_Spy.Properties.Resources.info;
            this.imageButton4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.imageButton4.ClickImage = global::My_Spy.Properties.Resources.infoclick;
            this.imageButton4.HoverImage = global::My_Spy.Properties.Resources.infohover;
            this.imageButton4.Location = new System.Drawing.Point(376, 554);
            this.imageButton4.Name = "imageButton4";
            this.imageButton4.NormalImage = global::My_Spy.Properties.Resources.info;
            this.imageButton4.OnClickShowImage = true;
            this.imageButton4.Size = new System.Drawing.Size(42, 42);
            this.imageButton4.TabIndex = 8;
            this.imageButton4.Click += new System.EventHandler(this.imageButton4_Click);
            // 
            // imageButton3
            // 
            this.imageButton3.BackgroundImage = global::My_Spy.Properties.Resources.noteedit;
            this.imageButton3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.imageButton3.ClickImage = global::My_Spy.Properties.Resources.noteeditclick;
            this.imageButton3.HoverImage = global::My_Spy.Properties.Resources.noteedithover;
            this.imageButton3.Location = new System.Drawing.Point(435, 554);
            this.imageButton3.Name = "imageButton3";
            this.imageButton3.NormalImage = global::My_Spy.Properties.Resources.noteedit;
            this.imageButton3.OnClickShowImage = true;
            this.imageButton3.Size = new System.Drawing.Size(42, 42);
            this.imageButton3.TabIndex = 7;
            this.imageButton3.Click += new System.EventHandler(this.imageButton3_Click);
            // 
            // imageButton2
            // 
            this.imageButton2.BackgroundImage = global::My_Spy.Properties.Resources.notetrash;
            this.imageButton2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.imageButton2.ClickImage = global::My_Spy.Properties.Resources.notetrashclick;
            this.imageButton2.HoverImage = global::My_Spy.Properties.Resources.notetrashhover;
            this.imageButton2.Location = new System.Drawing.Point(495, 554);
            this.imageButton2.Name = "imageButton2";
            this.imageButton2.NormalImage = global::My_Spy.Properties.Resources.notetrash;
            this.imageButton2.OnClickShowImage = true;
            this.imageButton2.Size = new System.Drawing.Size(42, 42);
            this.imageButton2.TabIndex = 6;
            this.imageButton2.Click += new System.EventHandler(this.imageButton2_Click);
            // 
            // imageButton1
            // 
            this.imageButton1.BackColor = System.Drawing.Color.Transparent;
            this.imageButton1.BackgroundImage = global::My_Spy.Properties.Resources.quit;
            this.imageButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.imageButton1.ClickImage = null;
            this.imageButton1.HoverImage = global::My_Spy.Properties.Resources.quithover;
            this.imageButton1.Location = new System.Drawing.Point(514, 1);
            this.imageButton1.Name = "imageButton1";
            this.imageButton1.NormalImage = global::My_Spy.Properties.Resources.quit;
            this.imageButton1.OnClickShowImage = false;
            this.imageButton1.Size = new System.Drawing.Size(32, 32);
            this.imageButton1.TabIndex = 0;
            this.imageButton1.Click += new System.EventHandler(this.imageButton1_Click);
            // 
            // NoteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(238)))), ((int)(((byte)(122)))));
            this.ClientSize = new System.Drawing.Size(550, 600);
            this.Controls.Add(this.imageButtonfavorite);
            this.Controls.Add(this.imageButtonAlert);
            this.Controls.Add(this.imageButton4);
            this.Controls.Add(this.imageButton3);
            this.Controls.Add(this.imageButton2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.imageButton1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NoteForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Note";
            this.Load += new System.EventHandler(this.NoteForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private My_Spy_Administrator.ImageButton imageButton1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelText;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxpassword;
        private My_Spy_Administrator.ImageButton imageButton2;
        private My_Spy_Administrator.ImageButton imageButton3;
        private My_Spy_Administrator.ImageButton imageButton4;
        private My_Spy_Administrator.ImageButton imageButtonAlert;
        private My_Spy_Administrator.ImageButton imageButtonfavorite;
    }
}