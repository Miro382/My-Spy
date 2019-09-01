namespace My_Spy
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.panelNote = new System.Windows.Forms.Panel();
            this.panelTime = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panelRemTime = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.labelRemTime = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panelWeather = new System.Windows.Forms.Panel();
            this.pictureBoxweather = new System.Windows.Forms.PictureBox();
            this.labelwclouds = new System.Windows.Forms.Label();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.labelwtemp = new System.Windows.Forms.Label();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.labelwcity = new System.Windows.Forms.Label();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBoxload = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorkerWeather = new System.ComponentModel.BackgroundWorker();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.longButtonchat = new My_Spy.LongButton();
            this.noteMain = new My_Spy.Note();
            this.panelNotes = new My_Spy.BufferedPanel();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panelNote.SuspendLayout();
            this.panelTime.SuspendLayout();
            this.panelRemTime.SuspendLayout();
            this.panelWeather.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxweather)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.BackgroundImage = global::My_Spy.Properties.Resources.background;
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Controls.Add(this.longButtonchat);
            this.tabPage1.Controls.Add(this.panel3);
            this.tabPage1.Controls.Add(this.panelNote);
            this.tabPage1.Controls.Add(this.panelTime);
            this.tabPage1.Controls.Add(this.panelRemTime);
            this.tabPage1.Controls.Add(this.panelWeather);
            this.tabPage1.Name = "tabPage1";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BackgroundImage = global::My_Spy.Properties.Resources.BoxKeylogger;
            this.panel3.Controls.Add(this.label11);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.label11, "label11");
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Name = "label11";
            // 
            // panelNote
            // 
            this.panelNote.Controls.Add(this.noteMain);
            resources.ApplyResources(this.panelNote, "panelNote");
            this.panelNote.Name = "panelNote";
            // 
            // panelTime
            // 
            this.panelTime.BackColor = System.Drawing.Color.Transparent;
            this.panelTime.BackgroundImage = global::My_Spy.Properties.Resources.OnPCTime;
            this.panelTime.Controls.Add(this.label10);
            this.panelTime.Controls.Add(this.label9);
            this.panelTime.Controls.Add(this.label8);
            this.panelTime.Controls.Add(this.label5);
            resources.ApplyResources(this.panelTime, "panelTime");
            this.panelTime.Name = "panelTime";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(35)))), ((int)(((byte)(126)))));
            this.label10.Name = "label10";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(35)))), ((int)(((byte)(126)))));
            this.label9.Name = "label9";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(35)))), ((int)(((byte)(126)))));
            this.label8.Name = "label8";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(35)))), ((int)(((byte)(126)))));
            this.label5.Name = "label5";
            // 
            // panelRemTime
            // 
            this.panelRemTime.BackColor = System.Drawing.Color.Transparent;
            this.panelRemTime.BackgroundImage = global::My_Spy.Properties.Resources.RemainingTime;
            this.panelRemTime.Controls.Add(this.label7);
            this.panelRemTime.Controls.Add(this.labelRemTime);
            this.panelRemTime.Controls.Add(this.label4);
            resources.ApplyResources(this.panelRemTime, "panelRemTime");
            this.panelRemTime.Name = "panelRemTime";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Name = "label7";
            // 
            // labelRemTime
            // 
            this.labelRemTime.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.labelRemTime, "labelRemTime");
            this.labelRemTime.ForeColor = System.Drawing.Color.White;
            this.labelRemTime.Name = "labelRemTime";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Name = "label4";
            // 
            // panelWeather
            // 
            this.panelWeather.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(188)))), ((int)(((byte)(156)))));
            this.panelWeather.Controls.Add(this.pictureBoxweather);
            this.panelWeather.Controls.Add(this.labelwclouds);
            this.panelWeather.Controls.Add(this.pictureBox8);
            this.panelWeather.Controls.Add(this.label6);
            this.panelWeather.Controls.Add(this.labelwtemp);
            this.panelWeather.Controls.Add(this.pictureBox7);
            this.panelWeather.Controls.Add(this.labelwcity);
            this.panelWeather.Controls.Add(this.pictureBox6);
            resources.ApplyResources(this.panelWeather, "panelWeather");
            this.panelWeather.Name = "panelWeather";
            // 
            // pictureBoxweather
            // 
            resources.ApplyResources(this.pictureBoxweather, "pictureBoxweather");
            this.pictureBoxweather.Name = "pictureBoxweather";
            this.pictureBoxweather.TabStop = false;
            // 
            // labelwclouds
            // 
            resources.ApplyResources(this.labelwclouds, "labelwclouds");
            this.labelwclouds.ForeColor = System.Drawing.Color.White;
            this.labelwclouds.Name = "labelwclouds";
            // 
            // pictureBox8
            // 
            this.pictureBox8.Image = global::My_Spy.Properties.Resources.clouds;
            resources.ApplyResources(this.pictureBox8, "pictureBox8");
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.TabStop = false;
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Name = "label6";
            // 
            // labelwtemp
            // 
            resources.ApplyResources(this.labelwtemp, "labelwtemp");
            this.labelwtemp.ForeColor = System.Drawing.Color.White;
            this.labelwtemp.Name = "labelwtemp";
            // 
            // pictureBox7
            // 
            this.pictureBox7.Image = global::My_Spy.Properties.Resources.temperature;
            resources.ApplyResources(this.pictureBox7, "pictureBox7");
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.TabStop = false;
            // 
            // labelwcity
            // 
            resources.ApplyResources(this.labelwcity, "labelwcity");
            this.labelwcity.ForeColor = System.Drawing.Color.White;
            this.labelwcity.Name = "labelwcity";
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = global::My_Spy.Properties.Resources.city;
            resources.ApplyResources(this.pictureBox6, "pictureBox6");
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.TabStop = false;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.White;
            this.tabPage3.BackgroundImage = global::My_Spy.Properties.Resources.backgroundtask;
            this.tabPage3.Controls.Add(this.panelNotes);
            this.tabPage3.Controls.Add(this.button2);
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(156)))), ((int)(((byte)(18)))));
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackgroundImage = global::My_Spy.Properties.Resources.loginbackground;
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panel2.Controls.Add(this.pictureBoxload);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.pictureBox5);
            this.panel2.Controls.Add(this.linkLabel1);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.textBoxPassword);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // pictureBoxload
            // 
            this.pictureBoxload.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxload.Image = global::My_Spy.Properties.Resources.Loading;
            resources.ApplyResources(this.pictureBoxload, "pictureBoxload");
            this.pictureBoxload.Name = "pictureBoxload";
            this.pictureBoxload.TabStop = false;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Name = "label3";
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox5.Image = global::My_Spy.Properties.Resources.eye;
            resources.ApplyResources(this.pictureBox5, "pictureBox5");
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.TabStop = false;
            // 
            // linkLabel1
            // 
            resources.ApplyResources(this.linkLabel1, "linkLabel1");
            this.linkLabel1.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.TabStop = true;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            // 
            // textBoxPassword
            // 
            resources.ApplyResources(this.textBoxPassword, "textBoxPassword");
            this.textBoxPassword.Name = "textBoxPassword";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.panel1.Controls.Add(this.tabControl1);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // backgroundWorkerWeather
            // 
            this.backgroundWorkerWeather.WorkerReportsProgress = true;
            this.backgroundWorkerWeather.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerWeather_DoWork);
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::My_Spy.Properties.Resources.minimize;
            resources.ApplyResources(this.pictureBox4, "pictureBox4");
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Click += new System.EventHandler(this.pictureBox4_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::My_Spy.Properties.Resources.quit;
            resources.ApplyResources(this.pictureBox3, "pictureBox3");
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::My_Spy.Properties.Resources.Header;
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::My_Spy.Properties.Resources.icon;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // longButtonchat
            // 
            this.longButtonchat.BackColor = System.Drawing.Color.White;
            this.longButtonchat.Color1 = System.Drawing.Color.White;
            this.longButtonchat.Color1Hover = System.Drawing.Color.WhiteSmoke;
            this.longButtonchat.Color2 = System.Drawing.Color.Gainsboro;
            this.longButtonchat.Color2Hover = System.Drawing.Color.Silver;
            this.longButtonchat.image = global::My_Spy.Properties.Resources.message;
            resources.ApplyResources(this.longButtonchat, "longButtonchat");
            this.longButtonchat.Name = "longButtonchat";
            this.longButtonchat.TextLabel = "Chat";
            this.longButtonchat.Click += new System.EventHandler(this.longButtonchat_Click);
            // 
            // noteMain
            // 
            this.noteMain.BackColor = System.Drawing.Color.Black;
            this.noteMain.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(227)))), ((int)(((byte)(0)))));
            this.noteMain.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(238)))), ((int)(((byte)(122)))));
            resources.ApplyResources(this.noteMain, "noteMain");
            this.noteMain.Name = "noteMain";
            this.noteMain.Pin = global::My_Spy.Properties.Resources.pin;
            // 
            // panelNotes
            // 
            resources.ApplyResources(this.panelNotes, "panelNotes");
            this.panelNotes.BackColor = System.Drawing.Color.Transparent;
            this.panelNotes.Name = "panelNotes";
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panelNote.ResumeLayout(false);
            this.panelTime.ResumeLayout(false);
            this.panelTime.PerformLayout();
            this.panelRemTime.ResumeLayout(false);
            this.panelRemTime.PerformLayout();
            this.panelWeather.ResumeLayout(false);
            this.panelWeather.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxweather)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBoxload;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button button2;
        private BufferedPanel panelNotes;
        private System.Windows.Forms.Panel panelWeather;
        private System.Windows.Forms.Label labelwcity;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labelwtemp;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.PictureBox pictureBoxweather;
        private System.Windows.Forms.Label labelwclouds;
        private System.ComponentModel.BackgroundWorker backgroundWorkerWeather;
        private System.Windows.Forms.Panel panelRemTime;
        private System.Windows.Forms.Label labelRemTime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panelTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private Note noteMain;
        private System.Windows.Forms.Panel panelNote;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label11;
        private LongButton longButtonchat;
    }
}

