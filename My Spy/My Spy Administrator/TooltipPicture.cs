using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace My_Spy_Administrator
{
    public partial class TooltipPicture : UserControl
    {
        public string ToolTipText = "";


        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string TooltipText
        {
            get { return ToolTipText; }
            set { ToolTipText = value; }
        }



        public TooltipPicture()
        {
            InitializeComponent();
        }

        private void TooltipPicture_Load(object sender, EventArgs e)
        {
        }


        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            toolTip1.Hide(this);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            toolTip1.Show(ToolTipText, this);
        }

    }
}
