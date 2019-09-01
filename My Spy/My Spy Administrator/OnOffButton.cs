using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

/*
 * 
 * Miroslav Murin
 * OnOffButton
 * 
 */ 


namespace My_Spy_Administrator
{

    [DefaultEvent("CheckedChanged")]
    public partial class OnOffButton : UserControl
    {

        public event EventHandler CheckedChanged;

        public bool Checked
        {
            get
            {
                return OnOffCheckbox.Checked;
            }
            set
            {
                OnOffCheckbox.Checked = value;
            }
        }

        Color EnColor = Color.Green, DisColor = Color.Red;
        string EnText = "ON",DisText = "OFF";



        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string ONText
        {
            get { return EnText; }
            set { EnText = value; }
        }


        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string OFFText
        {
            get { return DisText; }
            set
            {
                OnOffCheckbox.Text = value;
                DisText = value;
            }
        }


        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public Color ONColor
        {
            get { return EnColor; }
            set { EnColor = value; }
        }
        


        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public Color OFFColor
        {
            get { return DisColor; }
            set
            {
                OnOffCheckbox.BackColor = value;
                DisColor = value;
            }
        }


        private void RaiseCheckedChanged()
        {
            if (CheckedChanged != null)
                CheckedChanged(this, EventArgs.Empty);
        }


        public void SetCheckTo(bool check)
        {
            OnOffCheckbox.Checked = check;
        }

        private void OnOffButton_Load(object sender, EventArgs e)
        {
            ChangeCheck();
        }

        public OnOffButton()
        {
            InitializeComponent();
        }

        private void OnOffCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            ChangeCheck();
            RaiseCheckedChanged();
        }


       private void ChangeCheck()
        {
            if (OnOffCheckbox.Checked)
            {
                OnOffCheckbox.Checked = true;
                OnOffCheckbox.BackColor = EnColor;
                OnOffCheckbox.Text = EnText;
            }
            else
            {
                OnOffCheckbox.Checked = false;
                OnOffCheckbox.BackColor = DisColor;
                OnOffCheckbox.Text = DisText;
            }
        }


    }
}
