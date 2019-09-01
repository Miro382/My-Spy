

using CSharp_Weather;
using EncryptionLibrary;
using Microsoft.Win32;
using My_Spy_Administrator;
using Saving;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Welcome
{
    public partial class Form1 : Form
    {


        Encryption encryption = new Encryption("kmAAnmAbVFSO9pgA2sy5X9lZhn5TulKi74FDZYZw");
        RegistryKey registry = Registry.LocalMachine.CreateSubKey("Software\\My_Spy");
        RegistryKey registrysettings = Registry.LocalMachine.CreateSubKey("Software\\My_Spy\\Settings");

        public Form1()
        {
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {

            try
            {

                    if (registry.GetValue("Properties", null) != null)
                    {
                        Process.Start(AppDomain.CurrentDomain.BaseDirectory + "/Apps/Welcome.exe");
                        Application.Exit();
                    }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("" + ex);
                Application.Exit();
            }

            SetLanguageBox();
            CreateDirectories();
            Debug.WriteLine(Registry.LocalMachine.CreateSubKey("Software\\My_Spy\\"));
        }


        void SetLanguageBox()
        {
            string currentUI = Thread.CurrentThread.CurrentUICulture.Name;
            if (currentUI.Equals("sk-SK"))
            {
                comboBox1.SelectedIndex = 1;
            }
            else
            {
                comboBox1.SelectedIndex = 0;
            }
        }


        void CreateDirectories()
        {
            try
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Block/");
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/");
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Settings/");
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/MonitoringReports/");

            }
            catch(Exception ex)
            {
                Debug.WriteLine(""+ex);
            }

        }

        private void buttonsavesettings_Click(object sender, EventArgs e)
        {
            
                if (!string.IsNullOrWhiteSpace(textBox1.Text) && textBox2.Text.Equals(textBox1.Text))
                {
                    if (!string.IsNullOrWhiteSpace(textBoxemail.Text))
                    {
                    registry.SetValue("Properties", encryption.EncryptString(textBox2.Text, "2pqB7l4eRM6nHaZPtetkkHePguacGTvrMhvnE4fy"));
                    registry.SetValue("Email", encryption.EncryptString(textBoxemail.Text, "BNKh3wQVQy"));
                    ChangeLanguage();

                    textBox2.Text = "";
                    textBoxlocation.Text = "";
                    textBox1.Text = "";
                    HelpClass.RestartService("My Spy", 10);
                    MessageBox.Show(ResourcesFiles.ProgramStrings.ConfiguredBody,ResourcesFiles.ProgramStrings.ConfiguredHeader);
                    Application.Exit();
                    }
                    else
                    {
                        MessageBox.Show(ResourcesFiles.ProgramStrings.NoEmail, ResourcesFiles.ProgramStrings.Error);
                    }
                }
                else
                {
                    MessageBox.Show(ResourcesFiles.ProgramStrings.PasswordsNotEqual, ResourcesFiles.ProgramStrings.Error);
                }
        }






        void changelocation()
        {
            try
            {
                bool showw = bool.Parse(HelpClass.GETHtml("http://myspy.diodegames.eu/ShowWeather.txt"));
                if (showw)
                {
                    bool change = false;

                        if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/location.dat"))
                        {
                            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/location.dat");
                        }

                        CityLocator city = new CityLocator();
                        city.GetGeoCoordByCityNameEmail(textBoxlocation.Text, "miro382@centrum.sk");
                        SaveWriter Loc = new SaveWriter(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/location.dat");
                        Loc.AddItem("City", city.PlaceDisplayName);
                        Loc.AddItem("Latitude", city.Latitude);
                        Loc.AddItem("Longtitude", city.Longtitude);
                        Loc.Save();

                        textBoxlocation.Text = city.PlaceDisplayName;
                        change = true;
                    

                    if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/weathersettings.dat"))
                    {
                        File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/weathersettings.dat");
                    }

                    SaveWriter wsave = new SaveWriter(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/weathersettings.dat");
                    wsave.AddItem("Unit", radioButton2.Checked);
                    wsave.Save();

                    if (change)
                    {
                        if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/weather.dat"))
                        {
                            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/weather.dat");
                        }

                        if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/weathericon.dat"))
                        {
                            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/weathericon.dat");
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("" + ex);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBoxlocation.Text))
                changelocation();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ChangeLanguage();
        }

        //zmenit jazyk
        void ChangeLanguage()
        {

            string lang = "en-US";
            if (comboBox1.SelectedIndex == 1)
            {
                lang = "sk-SK";
            }
            else
            {
                lang = "en-US";
            }

            registry.SetValue("Language", lang);
        }




    }
}
