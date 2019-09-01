using CSharp_Weather;
using EncryptionLibrary;
using Microsoft.Win32;
using Saving;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Media;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;


namespace My_Spy
{
    public partial class Form1 : Form
    {
        static string PathMS = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy";
        Encryption encryption = new Encryption("kmAAnmAbVFSO9pgA2sy5X9lZhn5TulKi74FDZYZw");
        Font font,fontbig,fontsmall,fontmedium;
        PrivateFontCollection prfontc = new PrivateFontCollection();
        RegistryKey registry = Registry.LocalMachine.OpenSubKey("Software\\My_Spy", false);
        RegistryKey registrysettings = Registry.LocalMachine.OpenSubKey("Software\\My_Spy\\Settings", false);
        WeatherMET weather = new WeatherMET();
        Bitmap weathericon;

        public Form1()
        {
            // Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("sk-SK");
            //Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");

            SetLanguage();

            InitializeComponent();
        }




        private void AddFontFromResource(string fontResourceName)
        {
            var fontBytes = GetFontResourceBytes(Assembly.GetExecutingAssembly(), fontResourceName);
            var fontData = Marshal.AllocCoTaskMem(fontBytes.Length);
            Marshal.Copy(fontBytes, 0, fontData, fontBytes.Length);
            prfontc.AddMemoryFont(fontData, fontBytes.Length);
            Marshal.FreeCoTaskMem(fontData);
        }

        private byte[] GetFontResourceBytes(Assembly assembly, string fontResourceName)
        {
            var resourceStream = assembly.GetManifestResourceStream(fontResourceName);
            if (resourceStream == null)
                throw new Exception(string.Format("Unable to find font '{0}' in embedded resources.", fontResourceName));
            var fontBytes = new byte[resourceStream.Length];
            resourceStream.Read(fontBytes, 0, (int)resourceStream.Length);
            resourceStream.Close();
            return fontBytes;
        }





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
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }


        //***** Nacitanie Formu
        private void Form1_Load(object sender, EventArgs e)
        {

            //Nacitam si vlastny font
            try
            {

                prfontc = new PrivateFontCollection();

                prfontc.AddFontFile(AppDomain.CurrentDomain.BaseDirectory + "/Resources/font.ttf");

                fontsmall = new Font(prfontc.Families[0], 14.0F);
                fontmedium = new Font(prfontc.Families[0], 15.0F);
                font = new Font(prfontc.Families[0], 18.0F);
                fontbig = new Font(prfontc.Families[0], 26.0F);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                fontsmall = new Font(label1.Font.FontFamily, 14.0F);
                fontmedium = new Font(label1.Font.FontFamily, 15.0F);
                font = new Font(label1.Font.FontFamily, 18.0F);
                fontbig = new Font(label1.Font.FontFamily, 26.0F);
            }


            try
            {
                if (registry == null)
                {
                    Process.Start(AppDomain.CurrentDomain.BaseDirectory + "/Apps/Welcome.exe");
                    Application.Exit();
                }
                else
                {
                    if (registry.GetValue("Properties", null) == null)
                    {
                        Process.Start(AppDomain.CurrentDomain.BaseDirectory + "/Apps/Welcome.exe");
                        Application.Exit();
                    }
                }
            }catch(Exception ex)
            {
                Debug.WriteLine(""+ex);
                Application.Exit();
            }
            

            pictureBox3.MouseEnter += PictureBox3_MouseEnter;
            pictureBox3.MouseLeave += PictureBox3_MouseLeave;
            pictureBox4.MouseEnter += PictureBox4_MouseEnter;
            pictureBox4.MouseLeave += PictureBox4_MouseLeave;
            pictureBox5.MouseDown += PictureBox5_MouseDown;
            pictureBox5.MouseUp += PictureBox5_MouseUp;

            ImageList tabimage = new ImageList();
            tabimage.Images.Add("img1", Properties.Resources.home);
            tabimage.Images.Add("img2", Properties.Resources.login);
            tabimage.Images.Add("img3", Properties.Resources.task);

            tabControl1.ImageList = tabimage;
            tabControl1.TabPages[0].ImageKey = "img1";
            tabControl1.TabPages[1].ImageKey = "img3";
            tabControl1.TabPages[2].ImageKey = "img2";

            label2.Font = fontbig;
            button1.Font = font;

            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
            backgroundWorkerWeather.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorkerWeather_ProgressChanged);

            try {
                if(File.Exists(PathMS+"/Temp/secure.pas"))
                {
                    File.Delete(PathMS+"/Temp/secure.pas");
                }
            } catch(Exception exc)
            {
                Debug.WriteLine(exc);
            }

            AddNotesToPanel();

            backgroundWorkerWeather.RunWorkerAsync();

            textBoxPassword.KeyDown += new KeyEventHandler(TextBoxPassword_KeyPress);

            new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;

                    try
                    {

                        if (File.Exists(PathMS + "/Temp/FavoriteNote.dat"))
                        {
                            string pathn = File.ReadAllText(PathMS + "/Temp/FavoriteNote.dat");

                            string shorttext = "";

                            using (StreamReader str = new StreamReader(pathn))
                            {
                                for (int i = 0; i < 91; i++)
                                {
                                    int ch = str.Read();
                                    if (str.EndOfStream)
                                        break;

                                    shorttext += (char)ch;
                                }
                            }

                            noteMain.SetNoteText(shorttext);

                            noteMain.Tag = pathn;
                            noteMain.Click += new EventHandler(this.Note_Click);

                            this.Invoke((MethodInvoker)delegate
                            {
                                panelNote.Visible = true;
                            });
                        }


                        if (File.Exists(PathMS + "/Block/BlockApps.dat"))
                        {
                            SaveWriter set = new SaveWriter(PathMS + "/Block/BlockApps.dat");
                            set.Load();

                            if (set.GetItemBool("Enabled"))
                            {
                                set.Destroy();
                                set.PathToFile = PathMS + "/Block/RemTime.dat";
                                if (File.Exists(PathMS + "/Block/RemTime.dat"))
                                {

                                    set.Load();
                                    int timerem = set.GetItemInt("Time");
                                    int hrs = 0, mns = 0;
                                    if (timerem > 0)
                                    {
                                        hrs = (int)Math.Floor((decimal)(timerem / 60));
                                        mns = (timerem - (hrs * 60));
                                    }

                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        panelRemTime.Visible = true;

                                        if (timerem > 0)
                                            labelRemTime.Text = hrs.ToString("00") + ":" + mns.ToString("00");

                                    });

                                }
                            }
                        }



                        if (bool.Parse("" + registrysettings.GetValue("Statistics", false)))
                        {


                            if (File.Exists(PathMS + "/Stats/Today.sts"))
                            {
                                SaveWriter svs = new SaveWriter(PathMS + "/Stats/Today.sts");
                                svs.Load();
                                string time = svs.GetItemInt("Hours").ToString("00") + ":" + svs.GetItemInt("Mins").ToString("00");
                                svs.Destroy();

                                bool month = false;
                                string monthav = "";

                                if (File.Exists(PathMS + "/Stats/Month.sts"))
                                {
                                    month = true;
                                    svs.PathToFile=PathMS + "/Stats/Month.sts";
                                    svs.Load();
                                    int avgh = ((svs.GetItemInt("Hours") * 60) + svs.GetItemInt("Mins")) / svs.GetItemInt("Divide");
                                    int hour = (int)Math.Floor((decimal)(avgh / 60));
                                    int minutes = (avgh - (hour * 60));
                                    monthav = hour.ToString("00") + ":" + minutes.ToString("00");
                                    svs.Destroy();
                                }

                                this.Invoke((MethodInvoker)delegate
                                {
                                    panelTime.Visible = true;
                                    label8.Text = time;
                                    label10.Text = monthav;
                                });

                            }
                        }



                    }catch(Exception ex)
                    {
                        Debug.WriteLine(""+ex);
                    }


                }).Start();
            //koniec threadu


            if (File.Exists(PathMS + "/Settings/chat.dat"))
            {
                longButtonchat.Visible = true;
            }

            bool keylogger = false;
            try
            {
                keylogger = bool.Parse((string)registrysettings.GetValue("Keylogger", "false"));
            }
            catch
            {
                keylogger = false;
            }

            panel3.Visible = keylogger;

            label4.Font = fontmedium;
            labelRemTime.Font = fontsmall;
            label5.Font = font;
            label8.Font = fontsmall;
            label9.Font = font;
            label10.Font = fontsmall;
            label11.Font = fontmedium;

        }//----- Form 1 Load -----


        //******* Get notice ********
        void GetNotice()
        {

                Directory.CreateDirectory(PathMS + "/MonitoringReports/Notes/");
                Directory.CreateDirectory(PathMS + "/Temp/");

                DirectoryInfo dir = new DirectoryInfo(PathMS + "/MonitoringReports/Notes/");

                SaveWriter load = new SaveWriter();

                if (File.Exists(PathMS + "/Temp/Rnotes.dat"))
                    File.Delete(PathMS + "/Temp/Rnotes.dat");

                SaveWriter save = new SaveWriter(PathMS + "/Temp/Rnotes.dat");

                int k = 0;
                foreach (FileInfo file in dir.GetFiles("*.notesp"))
                {
                    load.Destroy();
                    load.PathToFile = file.FullName;
                    load.Load();
                    if (load.GetItemBool("Warn"))
                    {
                        save.AddItem("NotePath" + k, file.FullName);
                        save.AddItem("NoteDate" + k, load.GetItem("WarnTimeDate"));
                        k++;
                    }
                }
                save.AddItem("Count", k);
                save.Save();

        }
        //------- Get notice --------


        void AddNotesToPanel()
        {
            GetNotice();
            Directory.CreateDirectory(PathMS+"/MonitoringReports/Notes/");
            DirectoryInfo dir = new DirectoryInfo(PathMS+"/MonitoringReports/Notes/");
            string shorttext = "";
            int x = 0, y = 0;
            Random ran = new Random();
            foreach (FileInfo file in dir.GetFiles("*.note"))
            {
                shorttext = "";
                Note note = new Note();
                note.SetPinImage(Properties.Resources.pin);
                using (StreamReader str = new StreamReader(file.FullName))
                {
                    for (int i = 0; i < 91; i++)
                    {
                        int ch = str.Read();
                        if (str.EndOfStream)
                            break;

                        shorttext += (char)ch;
                    }
                    note.SetNoteText(shorttext);
                    note.Location = new Point((10 + ran.Next(0, 25) + (x * 190)), (ran.Next(0, 30) + (y * 195)));
                    x++;
                    if (x > 3)
                    {
                        x = 0;
                        y++;
                    }

                    note.Size = new Size(152, 162);
                    note.Tag = file.FullName;
                    note.Click += new EventHandler(this.Note_Click);

                    panelNotes.Controls.Add(note);

                }//using streamreader
            }
        }



        private void Note_Click(object sender, EventArgs e)
        {
            Note not = (Note)sender;
            NoteForm ntform = new NoteForm();
            ntform.Path = (string)not.Tag;
            ntform.Show();
        }

        private void PictureBox4_MouseEnter(object sender, EventArgs e)
        {
            pictureBox4.Image = Properties.Resources.minimizehover;
        }

        private void PictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.Image = Properties.Resources.minimize;
        }

        private void PictureBox3_MouseLeave(object arg1, EventArgs arg2)
        {
            pictureBox3.Image = Properties.Resources.quit;
        }

        private void PictureBox3_MouseEnter(object sender, EventArgs e)
        {
            pictureBox3.Image = Properties.Resources.quithover;
        }

 
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



        private void PictureBox5_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox5.Location = new Point(pictureBox5.Location.X + 4, pictureBox5.Location.Y + 4);
            pictureBox5.Size = new Size(32, 32);
            pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
            textBoxPassword.PasswordChar = '●';
        }

        private void PictureBox5_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox5.Location = new Point(pictureBox5.Location.X-4, pictureBox5.Location.Y-4);
            pictureBox5.Size = new Size(40, 40);
            pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
            textBoxPassword.PasswordChar = '\0';
        }


        //zabudnute heslo
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(AppDomain.CurrentDomain.BaseDirectory + "/Apps/ForgottenPassword.exe");
            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            pictureBoxload.Visible = true;
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            string password = encryption.DecryptString((string)registry.GetValue("Properties"), "2pqB7l4eRM6nHaZPtetkkHePguacGTvrMhvnE4fy");

            if (textBoxPassword.Text.Equals(password))
            {
                SaveWriter save = new SaveWriter(PathMS+"/Temp/secure.pas");
                save.AddItem("Token", encryption.EncryptString("M0F2s1Pwza", "BduIfxDmGPn5Xmk4DYqCHFkqg"));
                int time = (DateTime.Now.Hour * 60) + DateTime.Now.Minute;
                save.AddItem("Time", encryption.EncryptString("" + time, "f0Hy3j4tU5"));
                save.Save();
                backgroundWorker1.ReportProgress(0, "");
            }
            else
            {
                SoundPlayer player = new SoundPlayer(Properties.Resources.error);
                player.Play();
                backgroundWorker1.ReportProgress(1, "");
            }
            password = "";
        }


        void NewWeather(string latitude,string longtitude,string cityName)
        {

            if (File.Exists(PathMS+"/Temp/weather.dat"))
            {
                File.Delete(PathMS+"/Temp/weather.dat");
            }

            if (File.Exists(PathMS+"/Temp/weathericon.dat"))
            {
                File.Delete(PathMS+"/Temp/weathericon.dat");
            }


            if (weather.GetWeatherData(latitude, longtitude))
            {
                weather.weatherinfo.CityName = cityName;
                SaveWriter save = new SaveWriter(PathMS+"/Temp/weather.dat");
                save.AddItem("City", weather.weatherinfo.CityName);
                save.AddItem("Temperature", weather.weatherinfo.Temperature);
                save.AddItem("Clouds", weather.weatherinfo.Cloudiness);
                save.AddItem("Day", DateTime.Now.Day);
                save.AddItem("Hour", DateTime.Now.Hour);
                save.Save();


                if (DateTime.Now.Hour > 21 || DateTime.Now.Hour < 6)
                    weathericon = weather.GetIcon(1);
                else
                    weathericon = weather.GetIcon(0);

                weathericon.Save(PathMS+"/Temp/weathericon.dat", ImageFormat.Png);

                backgroundWorkerWeather.ReportProgress(0, "");
            }
        }



        private void backgroundWorkerWeather_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {

                bool showw = bool.Parse( HelpClass.GETHtml("http://myspy.diodegames.eu/ShowWeather.txt"));

                if (showw)
                {

                    SaveWriter Loc = new SaveWriter(PathMS+"/Temp/location.dat");
                    Loc.Load();
                    string Latitude = "", Longtitude = "", CityName = "";

                    Latitude = Loc.GetItem("Latitude");
                    Longtitude = Loc.GetItem("Longtitude");
                    CityName = Loc.GetItem("City");


                    if (File.Exists(PathMS+"/Temp/weather.dat"))
                    {
                        SaveWriter save = new SaveWriter(PathMS+"/Temp/weather.dat");
                        save.Load();

                        if(save.GetItemInt("Day")!=DateTime.Now.Day || DateTime.Now.Hour>= save.GetItemInt("Hour")+3)
                        {
                            Debug.WriteLine("New Weather: Day: "+ save.GetItemInt("Day")+"   Hour: "+ save.GetItemInt("Hour"));
                            NewWeather(Latitude, Longtitude, CityName);
                        }
                        else
                        {
                            Debug.WriteLine("Weather from file");
                            weather.weatherinfo.Temperature = save.GetItem("Temperature");
                            weather.weatherinfo.Cloudiness = save.GetItem("Clouds");
                            weather.weatherinfo.CityName = save.GetItem("City");
                            weathericon = (Bitmap)Bitmap.FromFile(PathMS+"/Temp/weathericon.dat");

                            SaveWriter wsave = new SaveWriter(PathMS+"/Temp/weathersettings.dat");
                            wsave.Load();
                            bool wunit = wsave.GetItemBool("Unit");

                            if (wunit)
                                weather.weatherinfo.TemperatureUnit = "F";
                            else
                                weather.weatherinfo.TemperatureUnit = "C";

                            backgroundWorkerWeather.ReportProgress(0, "");
                        }
                    }
                    else
                    {
                        Debug.WriteLine("New Weather because file not found");
                        NewWeather(Latitude,Longtitude,CityName);
                    }

                }

            }catch(Exception ex)
            {
                Debug.WriteLine(""+ex);
            }
            
        }



        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if(e.ProgressPercentage==0)
            {
                Process.Start(AppDomain.CurrentDomain.BaseDirectory + "/Apps/My Spy Administrator.exe");
                pictureBoxload.Visible = false;
                textBoxPassword.Text = "";
                Application.Exit();
            }
            else
            {
                label3.Visible = true;
                pictureBoxload.Visible = false;
            }
        }



        private void BackgroundWorkerWeather_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 0)
            {
                labelwcity.Text = weather.weatherinfo.CityName;
                labelwclouds.Text = weather.weatherinfo.Cloudiness+"%";
                if(weather.weatherinfo.TemperatureUnit.Equals("F"))
                labelwtemp.Text = weather.GetTemperatureInUnits(WeatherMET.Fahrenheit);
                else
                labelwtemp.Text = weather.GetTemperatureInUnits(WeatherMET.Celsius);

                pictureBoxweather.Image = weathericon;
                panelWeather.Visible = true;
            }
        }



        private void button2_Click(object sender, EventArgs e)
        {
            AddNote addnote = new AddNote();
            addnote.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(HelpClass.NotesChange)
            {
                HelpClass.NotesChange = false;
                panelNotes.Controls.Clear();
                AddNotesToPanel();
            }
        }


        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        private void longButtonchat_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Open");
            Process.Start(AppDomain.CurrentDomain.BaseDirectory + "/Apps/Chat.exe");
        }

        private void buttonToTasks_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }


        private void TextBoxPassword_KeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(null, EventArgs.Empty);
            }
        }




    }
}
