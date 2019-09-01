using EncryptionLibrary;
using Microsoft.Win32;
using Saving;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace My_Spy
{
    public partial class AddNote : Form
    {
        public bool Edit = false;
        public string Path = "",SPath="";
        Encryption encryption = new Encryption("kmAAnmAbVFSO9pgA2sy5X9lZhn5TulKi74FDZYZw");
        DateTime Createdate;
        public AddNote()
        {
            InitializeComponent();
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                dateTimePicker1.Enabled = true;
                dateTimePicker2.Enabled = true;
            }
            else
            {
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/MonitoringReports/Notes/");
                string key = HelpClass.RandomString(5);
                bool close = true;
                bool write = true;

                if (checkBox2.Checked)
                {
                    RegistryKey registry = Registry.LocalMachine.OpenSubKey("Software\\My_Spy", false);
                    if (!textBox1.Text.Equals(encryption.DecryptString((string)registry.GetValue("Properties"), "2pqB7l4eRM6nHaZPtetkkHePguacGTvrMhvnE4fy")))
                    {

                        MessageBox.Show(ResourcesFiles.ProgramStrings.badpassword);
                        close = false;
                        write = false;
                    }
                }

                if (write)
                {
                    SaveWriter save = new SaveWriter(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) +
                        "/My Spy/MonitoringReports/Notes/note_" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + key + ".notesp");
                    if (Edit)
                    {
                        save.PathToFile = SPath;
                        File.Delete(SPath);
                        File.Delete(Path);
                    }

                    DateTime date = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day,
                        dateTimePicker2.Value.Hour, dateTimePicker2.Value.Minute, 0);
                    save.AddItem("Warn", checkBox1.Checked);
                    save.AddItem("WarnTimeDate", date.ToString());
                    save.AddItem("Password", checkBox2.Checked);
                    if(Edit)
                    save.AddItem("DateofCreate", "" + Createdate);
                    else
                    save.AddItem("DateofCreate", "" + DateTime.Now.ToString());

                    save.AddItem("DateofEdit", "" + DateTime.Now.ToString());
                    //save.AddItem("Text",richTextBox1.Text);
                    save.Save();
                    save.Clear();


                        if (!Edit)
                        {
                            using (StreamWriter writer = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) +
                                "/My Spy/MonitoringReports/Notes/note_" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + key + ".note"))
                            {
                                writer.WriteLine(richTextBox1.Text);
                            }
                        }else
                        {
                            using (StreamWriter writer = new StreamWriter(Path))
                            {
                                writer.WriteLine(richTextBox1.Text);
                            }
                        }
                }

                if (close)
                {
                    this.Close();
                    HelpClass.NotesChange = true;
                }

        }

        
        private void AddNote_Load(object sender, EventArgs e)
        {
            if(Edit)
            {
                SaveWriter save = new SaveWriter(SPath);
                save.Load();
                richTextBox1.Text = File.ReadAllText(Path);
                this.Text = ResourcesFiles.ProgramStrings.EditNote;
                button2.Text = ResourcesFiles.ProgramStrings.EditNote;
                this.Icon = Properties.Resources.noteediticon;

                dateTimePicker1.Value = DateTime.Parse(save.GetItem("WarnTimeDate"));
                dateTimePicker2.Value = DateTime.Parse(save.GetItem("WarnTimeDate"));
                Createdate  = DateTime.Parse(save.GetItem("DateofCreate"));

                checkBox1.Checked = save.GetItemBool("Warn");
                checkBox2.Checked = save.GetItemBool("Password");

                save.Clear();

                //dateTimePicker1.Value = DateTime.Parse(save.GetItem("WarnTimeDate"));
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                textBox1.Enabled = true;
            }
            else
            {
                textBox1.Enabled = false;
            }
        }


    }
}
