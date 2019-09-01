namespace Chat
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
            this.panelcontrol = new System.Windows.Forms.Panel();
            this.imageButtonSmile = new My_Spy_Administrator.ImageButton();
            this.textBoxMessage = new System.Windows.Forms.TextBox();
            this.imageButtonSend = new My_Spy_Administrator.ImageButton();
            this.flowLayoutPanelMessages = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelONOFF = new System.Windows.Forms.Label();
            this.pictureBoxONOFF = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.panelcontrol.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxONOFF)).BeginInit();
            this.SuspendLayout();
            // 
            // panelcontrol
            // 
            this.panelcontrol.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelcontrol.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(81)))), ((int)(((byte)(181)))));
            this.panelcontrol.Controls.Add(this.imageButtonSmile);
            this.panelcontrol.Controls.Add(this.textBoxMessage);
            this.panelcontrol.Controls.Add(this.imageButtonSend);
            this.panelcontrol.Location = new System.Drawing.Point(0, 431);
            this.panelcontrol.Name = "panelcontrol";
            this.panelcontrol.Size = new System.Drawing.Size(705, 51);
            this.panelcontrol.TabIndex = 0;
            // 
            // imageButtonSmile
            // 
            this.imageButtonSmile.BackgroundImage = global::Chat.Properties.Resources.smiles;
            this.imageButtonSmile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.imageButtonSmile.ClickImage = global::Chat.Properties.Resources.smilesclick;
            this.imageButtonSmile.HoverImage = global::Chat.Properties.Resources.smileshover;
            this.imageButtonSmile.Location = new System.Drawing.Point(55, 3);
            this.imageButtonSmile.Name = "imageButtonSmile";
            this.imageButtonSmile.NormalImage = global::Chat.Properties.Resources.smiles;
            this.imageButtonSmile.OnClickShowImage = true;
            this.imageButtonSmile.Size = new System.Drawing.Size(46, 46);
            this.imageButtonSmile.TabIndex = 1;
            this.imageButtonSmile.Click += new System.EventHandler(this.imageButton1_Click);
            // 
            // textBoxMessage
            // 
            this.textBoxMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxMessage.Location = new System.Drawing.Point(107, 16);
            this.textBoxMessage.Name = "textBoxMessage";
            this.textBoxMessage.Size = new System.Drawing.Size(585, 22);
            this.textBoxMessage.TabIndex = 0;
            // 
            // imageButtonSend
            // 
            this.imageButtonSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.imageButtonSend.BackgroundImage = global::Chat.Properties.Resources.send;
            this.imageButtonSend.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.imageButtonSend.ClickImage = global::Chat.Properties.Resources.sendclick;
            this.imageButtonSend.HoverImage = global::Chat.Properties.Resources.sendhover;
            this.imageButtonSend.Location = new System.Drawing.Point(3, 2);
            this.imageButtonSend.Name = "imageButtonSend";
            this.imageButtonSend.NormalImage = global::Chat.Properties.Resources.send;
            this.imageButtonSend.OnClickShowImage = true;
            this.imageButtonSend.Size = new System.Drawing.Size(46, 46);
            this.imageButtonSend.TabIndex = 0;
            this.imageButtonSend.Click += new System.EventHandler(this.imageButtonSend_Click);
            // 
            // flowLayoutPanelMessages
            // 
            this.flowLayoutPanelMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanelMessages.AutoScroll = true;
            this.flowLayoutPanelMessages.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelMessages.Location = new System.Drawing.Point(12, 41);
            this.flowLayoutPanelMessages.Name = "flowLayoutPanelMessages";
            this.flowLayoutPanelMessages.Size = new System.Drawing.Size(680, 384);
            this.flowLayoutPanelMessages.TabIndex = 1;
            this.flowLayoutPanelMessages.WrapContents = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(156)))), ((int)(((byte)(18)))));
            this.panel1.Controls.Add(this.labelONOFF);
            this.panel1.Controls.Add(this.pictureBoxONOFF);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(720, 35);
            this.panel1.TabIndex = 2;
            // 
            // labelONOFF
            // 
            this.labelONOFF.AutoSize = true;
            this.labelONOFF.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelONOFF.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.labelONOFF.Location = new System.Drawing.Point(42, 8);
            this.labelONOFF.Name = "labelONOFF";
            this.labelONOFF.Size = new System.Drawing.Size(44, 20);
            this.labelONOFF.TabIndex = 2;
            this.labelONOFF.Text = "OFF";
            // 
            // pictureBoxONOFF
            // 
            this.pictureBoxONOFF.Image = global::Chat.Properties.Resources.off;
            this.pictureBoxONOFF.Location = new System.Drawing.Point(12, 5);
            this.pictureBoxONOFF.Name = "pictureBoxONOFF";
            this.pictureBoxONOFF.Size = new System.Drawing.Size(24, 24);
            this.pictureBoxONOFF.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxONOFF.TabIndex = 1;
            this.pictureBoxONOFF.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.label1.Location = new System.Drawing.Point(507, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Administrator account";
            this.label1.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 5000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.ClientSize = new System.Drawing.Size(704, 481);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.flowLayoutPanelMessages);
            this.Controls.Add(this.panelcontrol);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "My Spy Chat";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelcontrol.ResumeLayout(false);
            this.panelcontrol.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxONOFF)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelcontrol;
        private My_Spy_Administrator.ImageButton imageButtonSend;
        private System.Windows.Forms.TextBox textBoxMessage;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelMessages;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private My_Spy_Administrator.ImageButton imageButtonSmile;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBoxONOFF;
        private System.Windows.Forms.Label labelONOFF;
        private System.Windows.Forms.Timer timer2;
    }
}

