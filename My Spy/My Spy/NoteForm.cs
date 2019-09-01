using EncryptionLibrary;
using Microsoft.Win32;
using Saving;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace My_Spy
{
    public partial class NoteForm : Form
    {
        public NoteForm()
        {
            InitializeComponent();
        }

        public string Path = "";
        public bool remindshow = false;
        Color color1 = Color.FromArgb(250, 235, 94), color2 = Color.FromArgb(250, 238, 122);
        bool password = false;
        SaveWriter save;
        string pthspc = "";
        Encryption encryption = new Encryption("kmAAnmAbVFSO9pgA2sy5X9lZhn5TulKi74FDZYZw");
        RegistryKey registry = Registry.LocalMachine.OpenSubKey("Software\\My_Spy", false);
        ToolTip tt = new ToolTip();

        private bool FavoriteClick = false;

        //sluzi na presuvanie okna
        private const int WM_NCHITTEST = 0x84;
        private const int HT_CLIENT = 0x1;
        private const int HT_CAPTION = 0x2;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)(HT_CAPTION);
        }



        //sluzi na rychlejsie prekreslovanie 
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }


        private void imageButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void NoteForm_Load(object sender, EventArgs e)
        {
            if(remindshow)
            {
                TopMost = true;
            }

            this.Paint += NoteForm_Paint;
            labelText.Text = File.ReadAllText(Path);
            pthspc = Path.Replace(".note", ".notesp");

            save = new SaveWriter(pthspc);
            save.Load();
            password = save.GetItemBool("Password");

            tt.InitialDelay = 10;
            if (save.GetItemBool("Warn"))
            {
                imageButtonAlert.Visible = true;
                imageButtonAlert.MouseHover += PictureBoxAlert_MouseHover;
            }


            if (password)
                groupBox1.Visible = true;


            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/FavoriteNote.dat"))
            {
                if(File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/FavoriteNote.dat").Equals(Path))
                {
                    FavoriteClick = true;
                    imageButtonfavorite.normalImage = Properties.Resources.starfull;
                    imageButtonfavorite.NormalImage = Properties.Resources.starfull;
                    imageButtonfavorite.HoverImage = Properties.Resources.starfullhover;
                    imageButtonfavorite.ClickImage = Properties.Resources.starfullclick;

                }
            }


        }

        private void PictureBoxAlert_MouseHover(object sender, EventArgs e)
        {

            tt.SetToolTip(this.imageButtonAlert, ResourcesFiles.ProgramStrings.RemindDate + " - " + save.GetItem("WarnTimeDate"));
        }



        //informations
        private void imageButton4_Click(object sender, EventArgs e)
        {
            string warnstr = "";

            if(save.GetItemBool("Warn"))
            {
                warnstr = Environment.NewLine + ResourcesFiles.ProgramStrings.RemindDate + " - " + save.GetItem("WarnTimeDate");
            }

            MessageBox.Show(ResourcesFiles.ProgramStrings.CreatedDate+" - "+save.GetItem("DateofCreate") + Environment.NewLine
              + ResourcesFiles.ProgramStrings.ModifyDate + " - " + save.GetItem("DateofEdit") + Environment.NewLine
              + ResourcesFiles.ProgramStrings.IsProtected + " - " + HelpClass.BoolToYesNo (save.GetItemBool("Password")) + Environment.NewLine 
              + ResourcesFiles.ProgramStrings.Remind + " - " + HelpClass.BoolToYesNo( save.GetItemBool("Warn")) + warnstr
              , ResourcesFiles.ProgramStrings.Informations);
        }

        private void imageButton2_Click(object sender, EventArgs e)
        {
            if (password)
            {
                if (textBoxpassword.Text.Equals(encryption.DecryptString((string)registry.GetValue("Properties"), "2pqB7l4eRM6nHaZPtetkkHePguacGTvrMhvnE4fy")))
                {
                    ShowDeleteDialog();
                }
                else
                {
                    MessageBox.Show(ResourcesFiles.ProgramStrings.badpassword);
                }
            }else
            {
                ShowDeleteDialog();
            }
        }



        void ShowDeleteDialog()
        {
            DialogResult dialogResult = MessageBox.Show(ResourcesFiles.ProgramStrings.DeleteNote, ResourcesFiles.ProgramStrings.Delete, MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                File.Delete(Path);
                File.Delete(pthspc);
                HelpClass.NotesChange = true;
                Close();
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }


        void EditNote()
        {
            AddNote addnote = new AddNote();
            addnote.Path = Path;
            addnote.SPath = pthspc;
            addnote.Edit = true;
            addnote.Show();
            Close();
        }

        //edit
        private void imageButton3_Click(object sender, EventArgs e)
        {
            if (password)
            {
                if (textBoxpassword.Text.Equals(encryption.DecryptString((string)registry.GetValue("Properties"), "2pqB7l4eRM6nHaZPtetkkHePguacGTvrMhvnE4fy")))
                {
                    EditNote();
                }
                else
                {
                    MessageBox.Show(ResourcesFiles.ProgramStrings.badpassword);
                }
            }
            else
            {
                EditNote();
            }
        }


        private void imageButtonAlert_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(ResourcesFiles.ProgramStrings.Cancelremindtext, ResourcesFiles.ProgramStrings.Cancelremind, MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                SaveWriter save = new SaveWriter(pthspc);
                save.Load();
                save.LoadedValuesToSaveValues();
                File.Delete(pthspc);
                save.RemoveItem("Warn");
                save.AddItem("Warn",false);
                save.Save();
                imageButtonAlert.Visible = false;
                HelpClass.NotesChange = true;
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }


        private void imageButtonfavorite_Click(object sender, EventArgs e)
        {
            if(!FavoriteClick)
            {
                FavoriteClick = true;
                imageButtonfavorite.normalImage = Properties.Resources.starfull;
                imageButtonfavorite.HoverImage = Properties.Resources.starfullhover;
                imageButtonfavorite.ClickImage = Properties.Resources.starfullclick;

                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/FavoriteNote.dat"))
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/FavoriteNote.dat");

                File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/FavoriteNote.dat", Path);
            }
            else
            {
                FavoriteClick = false;
                imageButtonfavorite.normalImage = Properties.Resources.starnone;
                imageButtonfavorite.HoverImage = Properties.Resources.starnonehover;
                imageButtonfavorite.ClickImage = Properties.Resources.starnoneclick;

                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/FavoriteNote.dat"))
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/FavoriteNote.dat");
            }
        }


        private void NoteForm_Paint(object sender, PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle,
                                               color1,
                                               color2,
                                               90F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }


            e.Graphics.DrawRectangle(Pens.Black, 0, 0, 549, 599);
        }
    }
}
