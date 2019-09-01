namespace My_Spy_Administrator
{
    partial class DecorativeLine
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
            this.decup = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // decup
            // 
            this.decup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.decup.BackColor = System.Drawing.Color.DimGray;
            this.decup.Location = new System.Drawing.Point(0, 0);
            this.decup.Name = "decup";
            this.decup.Size = new System.Drawing.Size(300, 12);
            this.decup.TabIndex = 0;
            // 
            // DecorativeLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.Controls.Add(this.decup);
            this.Name = "DecorativeLine";
            this.Size = new System.Drawing.Size(300, 24);
            this.Load += new System.EventHandler(this.DecorativeLine_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel decup;
    }
}
