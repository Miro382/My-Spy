using EncryptionLibrary;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My_Spy_Administrator
{
    public partial class WebLogin : Form
    {
        WebEncryption webenc = new WebEncryption();
        RegistryKey registry = Registry.LocalMachine.CreateSubKey("Software\\My_Spy\\Settings");

        public WebLogin()
        {
            InitializeComponent();
        }

        private void buttonWebLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Task t = new Task(CheckLogin);
                t.Start();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("" + ex);
            }
        }



        async void CheckLogin()
        {

            try
            {
                HttpClient client = new HttpClient();
                Dictionary<string, string> values = new Dictionary<string, string>();
                values.Add("Token", "blZEHudX");
                values.Add("Email", textBoxEmail.Text);
                values.Add("Pass", webenc.EncryptMessage(Encoding.ASCII.GetBytes(textBoxPassword.Text), "gdxW1KSK45lICXQvxeko9HnEajCBtY0X"));

                FormUrlEncodedContent content = new FormUrlEncodedContent(values);

                HttpResponseMessage result = await client.PostAsync("http://myspy.diodegames.eu/Connection/CLogin.php", content);
                string resultContent = await result.Content.ReadAsStringAsync();
                resultContent = resultContent.Replace("<tr>","");
                Debug.WriteLine("Web result Login: " + resultContent);
                if(!resultContent.Equals("Error"))
                {
                    registry.SetValue("Web",true);
                    registry.SetValue("WebID",resultContent);
                    MessageBox.Show(ResourcesFiles.ProgramStrings.TextWebSuccess, ResourcesFiles.ProgramStrings.HeaderWebSuccess, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Invoke((MethodInvoker)delegate
                    {
                        Close();
                    });

                }
                else
                {
                  MessageBox.Show(ResourcesFiles.ProgramStrings.TextWebError, ResourcesFiles.ProgramStrings.HeaderWebError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("" + ex);
            }
        }



    }
}
