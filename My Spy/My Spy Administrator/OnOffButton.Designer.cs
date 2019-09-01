namespace My_Spy_Administrator
{
    partial class OnOffButton
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
            this.OnOffCheckbox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // OnOffCheckbox
            // 
            this.OnOffCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OnOffCheckbox.Appearance = System.Windows.Forms.Appearance.Button;
            this.OnOffCheckbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.OnOffCheckbox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.OnOffCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.OnOffCheckbox.Location = new System.Drawing.Point(0, 0);
            this.OnOffCheckbox.Name = "OnOffCheckbox";
            this.OnOffCheckbox.Size = new System.Drawing.Size(70, 25);
            this.OnOffCheckbox.TabIndex = 4;
            this.OnOffCheckbox.Text = "OFF";
            this.OnOffCheckbox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.OnOffCheckbox.UseVisualStyleBackColor = false;
            this.OnOffCheckbox.CheckedChanged += new System.EventHandler(this.OnOffCheckbox_CheckedChanged);
            // 
            // OnOffButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.OnOffCheckbox);
            this.Name = "OnOffButton";
            this.Size = new System.Drawing.Size(70, 25);
            this.Load += new System.EventHandler(this.OnOffButton_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox OnOffCheckbox;
    }
}
