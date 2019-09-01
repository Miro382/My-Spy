using Microsoft.Win32;
using Newtonsoft.Json;
using Saving;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Timers;
using Toolkit;

/*
 * Miroslav Murin
 * My Spy Service
 * --------
 * Sluzi na vykonavanie vsetkych sledovacich operacii na pozadi.
 * --------
 */

namespace My_Spy_Service
{
    public partial class Service1 : ServiceBase
    {

        static string PathMS = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/My Spy";

        //Monitorovat applikacie [nastavenia]
        bool MonApp = false;
        //Sledovat vstup z klavesnice [nastavenia] 
        bool Keylogger = false;
        //Statistiky
        bool StatsEn = true;
        //monitorovat internet [nastavenia]
        bool Internet = false;
        //pristup k registru kde su ulozene nastavenia
        RegistryKey registry = Registry.LocalMachine.OpenSubKey("Software\\My_Spy\\Settings", false);
        //List kde sa pridavaju spustene aplikacie
        List<ApplicationInfo> appinfo = new List<ApplicationInfo>();
        //moja classa na ukladanie a nacitavanie suborov
        SaveWriter save = new SaveWriter();
        //odosielanie na Web
        bool WebService = true;
      

        List<NoteRemind> notice = new List<NoteRemind>();
        List<string> blockapplist = new List<string>();

        short day ,timers = 0,timers2 = 0,allmin =0;
        int PCHour = 0, PCMin = 0,allhour = 0,RestartD;

        bool PCTime = false,BlockApps=false;
        bool warning = false;
        int MaxTimePC = 0;
        int warningtime = 0;

        bool WarningisShown = false;


        static HttpClient client = new HttpClient();

        System.Timers.Timer timer1, timer2,timerWeb;

        bool ShutDownVerify = false;

        //Web data
        string WebID = "1";
        string WebAlias = "PC";


        public Service1()
        {
            InitializeComponent();
        }


        Newtonsoft.Json.JsonSerializerSettings jss = new Newtonsoft.Json.JsonSerializerSettings();



        //********* ONSTART ***********
        protected override void OnStart(string[] args)
        {

                DebugService.Write("*******   " + DateTime.Now + "   ********");
                DebugService.Write("Start");

                //nacitaju sa nastavenia
                MonApp = bool.Parse((string)registry.GetValue("MonitorApplications", "False"));
                Keylogger = bool.Parse((string)registry.GetValue("Keylogger", "False"));
                StatsEn = bool.Parse((string)registry.GetValue("Statistics", "True"));
                Internet = bool.Parse((string)registry.GetValue("Internet", "False"));

                DebugService.Write("Monitoring Applications: " + MonApp + "  Keylogger: " + Keylogger + "  Statistics: " + StatsEn);


                day = (short)DateTime.Now.Day;
                RestartD = DateTime.Now.Day;

                //kazdych 5 minut
                timer1 = new System.Timers.Timer();
                timer1.Elapsed += new ElapsedEventHandler(OnTimer1_Tick);
                timer1.Interval = 300000;
                timer1.Enabled = true;

                //kazdu minutu
                timer2 = new System.Timers.Timer();
                timer2.Elapsed += new ElapsedEventHandler(OnTimer2_Tick);
                timer2.Interval = 60000;
                timer2.Enabled = true;


                //timer pre odosielanie dat na Web - 20 minut
                timerWeb = new System.Timers.Timer();
                timerWeb.Elapsed += new ElapsedEventHandler(OnTimerWeb_Tick);
                timerWeb.Interval = 1200000;
                timerWeb.Enabled = true;

            if (File.Exists(PathMS + "/Temp/ExpiredTime.dat"))
                {
                    short TimeDay = short.Parse(File.ReadAllText(PathMS + "/Temp/ExpiredTime.dat"));
                    if (TimeDay == day)
                    {
                        ShutDownVerify = true;
                        ApplicationLoader.PROCESS_INFORMATION procInfo;
                        ApplicationLoader.StartProcessAndBypassUAC(AppDomain.CurrentDomain.BaseDirectory + "/Apps/VerifyUser.exe", out procInfo);
                    }
                    else
                    {
                        ShutDownVerify = false;
                        File.Delete(PathMS + "/Temp/ExpiredTime.dat");
                    }
                }


                if (MonApp)
                {
                    ApplicationLoader.PROCESS_INFORMATION procInfo;
                    ApplicationLoader.StartProcessAndBypassUAC(AppDomain.CurrentDomain.BaseDirectory + "/Apps/GetRunningApplications.exe", out procInfo);

                    LoadAppMonitoring();
                }


                if (Keylogger || Internet)
                {
                    StartWorker();
                }


                if (StatsEn)
                    LoadStatistics();


                ReadNotice();

                Directory.CreateDirectory(PathMS + "/Block/");


                SetRemainingPCTime();

                if (File.Exists(PathMS + "/Block/BlockApps.dat"))
                {
                    SaveWriter set = new SaveWriter(PathMS + "/Block/BlockApps.dat");
                    set.Load();
                    BlockApps = set.GetItemBool("Enabled");
                    for (int i = 0; i < set.GetItemInt("Count"); i++)
                    {
                        blockapplist.Add(set.GetItem("Pr" + i));
                    }
                }

                DebugService.Write("PCTime: " + PCTime);



            client.BaseAddress = new Uri("http://myspy.diodegames.eu");


            Newtonsoft.Json.Serialization.DefaultContractResolver dcr = new Newtonsoft.Json.Serialization.DefaultContractResolver();
            dcr.DefaultMembersSearchFlags |= System.Reflection.BindingFlags.NonPublic;
            jss.ContractResolver = dcr;
        }
        //------------- On Start ---------------


        //******* Get notice ********
        void ReadNotice()
        {
            notice.Clear();
            try
            {
                Directory.CreateDirectory(PathMS + "/Temp/");

                if (File.Exists(PathMS + "/Temp/Rnotes.dat"))
                {
                    SaveWriter save = new SaveWriter(PathMS + "/Temp/Rnotes.dat");
                    save.Load();
                    int cnt = save.GetItemInt("Count");

                    for (int i = 0; i < cnt; i++)
                    {
                        notice.Add(new NoteRemind(save.GetItem("NotePath" + i), DateTime.Parse(save.GetItem("NoteDate" + i))));
                    }
                    //notice.Add
                }
            }catch(Exception ex)
            {
                DebugService.WriteError(""+ex);
            }

        }
        //------- Get notice --------





        void CheckAllNotes()
        {
            try
            {
                Directory.CreateDirectory(PathMS + "/Temp/");

                bool run = false;
                int k = 0;
                int del = -1;
                foreach (NoteRemind not in notice)
                {
                    if (DateTime.Now > not.Datetime)
                    {
                        del = k;
                        string pth = not.Path.Replace(".notesp", ".note");

                            File.WriteAllText(PathMS + "/Temp/Remind.dat", pth);
                        
                        run = true;
                    }
                    k++;
                }

                if(del>-1)
                notice.RemoveAt(del);

                if (run)
                {
                    ApplicationLoader.PROCESS_INFORMATION procInfo;
                    ApplicationLoader.StartProcessAndBypassUAC(AppDomain.CurrentDomain.BaseDirectory + "/My Spy.exe", out procInfo);
                }
            }catch(Exception ex)
            {
                DebugService.WriteError(""+ex);
            }
        }




        void SetRemainingPCTime()
        {

            if (!ShutDownVerify)
            {
                bool custommaxtimepc = false;

                if (File.Exists(PathMS + "/Block/RemTime.dat"))
                {
                    save = new SaveWriter(PathMS + "/Block/RemTime.dat");
                    save.Load();
                    if (save.GetItemInt("Day") == DateTime.Now.Day)
                    {
                        MaxTimePC = save.GetItemInt("Time");
                        custommaxtimepc = true;
                    }
                    else
                    {
                        File.Delete(PathMS + "/Block/RemTime.dat");
                    }
                }

                if (File.Exists(PathMS + "/Block/ComputerTime.dat"))
                {
                    SaveWriter save = new SaveWriter(PathMS + "/Block/ComputerTime.dat");
                    save.Load();

                    if (save.GetItemBool("Enable"))
                    {
                        PCTime = true;

                        DateTime time = DateTime.Parse(save.GetItem("Time"));

                        if (!custommaxtimepc)
                            MaxTimePC = (time.Hour * 60) + time.Minute;

                        warning = save.GetItemBool("Warning");
                        warningtime = save.GetItemInt("WarningTime");

                        if (MaxTimePC <= 1)
                        {
                            if (File.Exists(PathMS + "/Temp/ExpiredTime.dat"))
                                File.Delete(PathMS + "/Temp/ExpiredTime.dat");

                            File.WriteAllText(PathMS + "/Temp/ExpiredTime.dat", "" + DateTime.Now.Day);
                            PCTime = false;
                            ShutDownVerify = true;
                            ApplicationLoader.PROCESS_INFORMATION procInfo;
                            ApplicationLoader.StartProcessAndBypassUAC(AppDomain.CurrentDomain.BaseDirectory + "/Apps/VerifyUser.exe", out procInfo);
                        }
                    }

                    save.Destroy();
                }
                else
                {
                    PCTime = false;
                }
            }
        }


        #region Timer1

        //************ TIMER 1 (5 min) ********************
        private void OnTimer1_Tick(object source, ElapsedEventArgs e)
        {

            try
            {



                if(RestartD != DateTime.Now.Day)
                {
                    RestartD = DateTime.Now.Day;

                    if (File.Exists(PathMS + "/Block/RemTime.dat"))
                    {
                        File.Delete(PathMS + "/Block/RemTime.dat");
                    }

                        SetRemainingPCTime();

                    if(DateTime.Now.Day == 1)
                    {
                        if(File.Exists(AppDomain.CurrentDomain.BaseDirectory + "/serviceerror.txt"))
                        {
                            File.Delete(AppDomain.CurrentDomain.BaseDirectory + "/serviceerror.txt");
                        }
                        if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "/servicedebug.txt"))
                        {
                            File.Delete(AppDomain.CurrentDomain.BaseDirectory + "/servicedebug.txt");
                        }
                    }

                }//RestartD



                if (ShutDownVerify)
                {
                    if (File.Exists(PathMS + "/Temp/DisableShutDown.dat"))
                    {
                        if(bool.Parse(File.ReadAllText(PathMS + "/Temp/DisableShutDown.dat"))==true)
                        {
                            ShutDownVerify = false;
                            File.Delete(PathMS + "/Temp/DisableShutDown.dat");
                        }else
                        {
                            File.Delete(PathMS + "/Temp/DisableShutDown.dat");
                            SaveAll();
                            ShutDownComputer();
                        }

                    }
                    else
                    {
                        SaveAll();
                        ShutDownComputer();
                    }
                }


                PCMin += 5;
                if(PCMin>=60)
                {
                    PCMin -= 60;
                    PCHour++;
                }


               if(timers2>6)
               {
                    ReadNotice();
               }

                timers2++;

                CheckAllNotes();


                if (MonApp)
                {

                    WorkWithRunningApplications();

                    timers++;

                    if (timers > 3)
                    {
                        SaveAppMonitoring();
                        timers = 0;
                    }
                }



                if (BlockApps)
                    CheckBlockApplications();


                if (Keylogger || Internet)
                {
                    StartWorker();
                }



                SaveStatistics();

                if (WebService)
                {
                    Task t = new Task(SetOnWeb);
                    t.Start();
                }

            }
            catch(Exception ex)
            {
                DebugService.WriteError(""+ex);
            }


        }
        //--------- ON Timer 1 Tick -----------


        #endregion


        #region Timer2

        //************ TIMER 2 (1 min) ********************
        private void OnTimer2_Tick(object sender, ElapsedEventArgs e)
        {

            allmin++;
            if(allmin>59)
            {
                allmin = 0;
                allhour++;
            }


            try
            {

                if (PCTime)
                {
                    MaxTimePC--;

                    if(MaxTimePC<0)
                    {
                        if (File.Exists(PathMS + "/Temp/NoShutDownFirst.dat"))
                        {
                            File.Delete(PathMS + "/Temp/NoShutDownFirst.dat");
                            PCTime = false;
                        }
                        else
                        {


                            if (File.Exists(PathMS + "/Temp/ExpiredTime.dat"))
                                File.Delete(PathMS + "/Temp/ExpiredTime.dat");

                            File.WriteAllText(PathMS + "/Temp/ExpiredTime.dat", "" + DateTime.Now.Day);

                            ShutDownComputer();

                            if (MaxTimePC < -20)
                                MaxTimePC = -1;
                        }
                    }

                    if (warning)
                    {
                        if (MaxTimePC <= warningtime)
                        {
                            if (!WarningisShown)
                            {
                                File.WriteAllText(PathMS + "/Temp/shwwrn.dat", "True");
                                ApplicationLoader.PROCESS_INFORMATION procInfo;
                                ApplicationLoader.StartProcessAndBypassUAC(AppDomain.CurrentDomain.BaseDirectory + "/Apps/RemainingTime.exe", out procInfo);
                                WarningisShown = true;
                            }
                        }
                    }

                    SaveRemainingTime();
                }//if PCTime


            }catch(Exception ex)
            {
                DebugService.WriteError("" + ex);
            }
        }
        //--------- ON Timer 2 Tick -----------

        #endregion




        void CheckBlockApplications()
        {
            try
            {
                for (int i = 0; i < blockapplist.Count; i++)
                {
                    Process[] processes = Process.GetProcessesByName(blockapplist[0]);
                    if (processes.Length > 0)
                    {
                        foreach(Process prc in processes)
                        prc.Kill();
                    }
                }

            }catch(Exception ex)
            {
                DebugService.WriteError(ex.ToString());
            }
        }


        void ShutDownComputer()
        {
            ApplicationLoader.PROCESS_INFORMATION procInfo;
            ApplicationLoader.StartProcessAndBypassUAC(AppDomain.CurrentDomain.BaseDirectory + "/Apps/ShutDown_App.exe", out procInfo);
        }


        //***** Start Worker *****
        void StartWorker()
        {
            try
            {
                Process[] processes = Process.GetProcessesByName("My_Spy_Worker");
                DebugService.Write("Count: "+processes.Length);
                if (processes.Length < 1)
                {
                    ApplicationLoader.PROCESS_INFORMATION procInfo;
                    ApplicationLoader.StartProcessAndBypassUAC(AppDomain.CurrentDomain.BaseDirectory + "/Apps/My_Spy_Worker.exe", out procInfo);
                }
            }catch(Exception ex)
            {
                DebugService.WriteError(""+ex);
            }
        }
        //----- Start Worker -----



        //********  Work With Running Applications ********
        //Skontroluje spustene aplikacie
        void WorkWithRunningApplications()
        {
            if (day != ((short)DateTime.Now.Day))
            {
                PCHour = 0;
                PCMin = 0;
                appinfo.Clear();
                day = (short)DateTime.Now.Day;
            }

            if (File.Exists(PathMS + "/Temp/appreport.dat"))
            {
                try
                {
                    save.Destroy();
                    save = new SaveWriter(PathMS + "/Temp/appreport.dat");
                    save.Load();
                    int count = save.GetItemInt("Count");
                    for (int i = 0; i < count; i++)
                    {

                        bool found = false;
                        string executable = save.GetItem("Module_" + i);
                        for (int j = 0; j < appinfo.Count; j++)
                        {
                            if (appinfo[j].ExecutableName.Equals(executable))
                            {
                                found = true;
                                appinfo[j].AddTime(5);
                            }
                        }

                        if (!found)
                        {
                            appinfo.Add(new ApplicationInfo(save.GetItem("Title_" + i), executable));
                        }

                    }
                    save.Destroy();
                }
                catch(Exception ex)
                {
                    ApplicationLoader.PROCESS_INFORMATION procInfos;
                    ApplicationLoader.StartProcessAndBypassUAC(AppDomain.CurrentDomain.BaseDirectory + "/Apps/GetRunningApplications.exe", out procInfos);
                    DebugService.WriteError("Bad load data:   "+ex);
                    return;
                }
            }

            ApplicationLoader.PROCESS_INFORMATION procInfo;
            ApplicationLoader.StartProcessAndBypassUAC(AppDomain.CurrentDomain.BaseDirectory + "/Apps/GetRunningApplications.exe", out procInfo);
        }
        //--------- Work With Running Applications ----------



        //********* Save Remaining Time *********
        void SaveRemainingTime()
        {
            try
            {
                Directory.CreateDirectory(PathMS + "/Block/");
                string rpath = PathMS + "/Block/RemTime.dat";

                if (File.Exists(rpath))
                    File.Delete(rpath);

                save.Destroy();
                save = new SaveWriter(rpath);
                save.AddItem("Day",DateTime.Now.Day);
                save.AddItem("Time", MaxTimePC);
                save.Save();

            }
            catch(Exception ex)
            {
                DebugService.WriteError("" + ex);
            }
        }
        //---------- Save Remaining Time ----------


        //********* Save App Monitoring *********
        void SaveAppMonitoring()
        {
            try
            {
                Directory.CreateDirectory(PathMS + "/MonitoringReports/App/");
                save.Destroy();
                save = new SaveWriter(PathMS + "/MonitoringReports/App/" + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + ".apprep");
                save.AddItem("Count", appinfo.Count);
                for (int i = 0; i < appinfo.Count; i++)
                {
                    appinfo[i].CalcTime();
                    save.AddItem("Title_" + i, appinfo[i].ApplicationName);
                    save.AddItem("Module_" + i, appinfo[i].ExecutableName);
                    save.AddItem("TimeMin_" + i, appinfo[i].GetMinutes());
                    save.AddItem("TimeHours_" + i, appinfo[i].GetHours());
                }

                save.AddItem("PCTimeHour",PCHour);
                save.AddItem("PCTimeMinutes", PCMin);

                save.Save();
                save.Destroy();
            }catch(Exception ex)
            {
                DebugService.WriteError(""+ex);
            }
        }
        //---------- Save App Monitoring ----------



        //********* Load App Monitoring *********
        void LoadAppMonitoring()
        {
            try
            {
                Directory.CreateDirectory(PathMS + "/MonitoringReports/App/");
                if (File.Exists(PathMS + "/MonitoringReports/App/" + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + ".apprep"))
                {
                    save = new SaveWriter(PathMS + "/MonitoringReports/App/" + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + ".apprep");
                    save.Load();
                    for (int i = 0; i < save.GetItemInt("Count"); i++)
                    {
                        ApplicationInfo inf = new ApplicationInfo(save.GetItem("Title_" + i), save.GetItem("Module_" + i));
                        inf.SetTime(save.GetItemInt("TimeHours_" + i), save.GetItemInt("TimeMin_" + i));
                        appinfo.Add(inf);
                    }

                    PCHour = save.GetItemInt("PCTimeHour");
                    PCMin = save.GetItemInt("PCTimeMinutes");
                    save.Destroy();
                }
            }
            catch (Exception ex)
            {
                DebugService.WriteError("" + ex);
            }
        }
        //---------- Load App Monitoring ----------




        void LoadStatistics()
        {
            try
            {
                if (File.Exists(PathMS + "/Stats/Today.sts"))
                {
                    SaveWriter save = new SaveWriter(PathMS + "/Stats/Today.sts");
                    save.Load();
                    if (save.GetItemInt("Day") == DateTime.Now.Day)
                    {
                        allhour = save.GetItemInt("Hours");
                        allmin = save.GetItemShort("Mins");
                    }
                }
            }catch(Exception ex)
            {
                DebugService.WriteError(""+ex);
            }
        }



        void SaveStatistics()
        {
            try
            {
                if (StatsEn)
                {
                    Directory.CreateDirectory(PathMS + "/Stats/");

                    if (File.Exists(PathMS + "/Stats/Today.sts"))
                    {
                        SaveWriter savew = new SaveWriter(PathMS + "/Stats/Today.sts");
                        savew.Load();

                        if (savew.GetItemInt("Day") != DateTime.Now.Day)
                        {
                            int hrs = savew.GetItemInt("Hours");

                            short mns = (short)savew.GetItemInt("Mins");

                            bool Graphskip = false;

                            if (DateTime.Now.Day < savew.GetItemInt("Day"))
                            {
                                Graphskip = true;

                                if (File.Exists(PathMS + "/Stats/MonthOld.sts"))
                                    File.Delete(PathMS + "/Stats/MonthOld.sts");
                                if (File.Exists(PathMS + "/Stats/MonthGraphOld.sts"))
                                    File.Delete(PathMS + "/Stats/MonthGraphOld.sts");


                                if (File.Exists(PathMS + "/Stats/Month.sts"))
                                    File.Move(PathMS + "/Stats/Month.sts", PathMS + "/Stats/MonthOld.sts");
                                if (File.Exists(PathMS + "/Stats/MonthGraph.sts"))
                                    File.Move(PathMS + "/Stats/MonthGraph.sts", PathMS + "/Stats/MonthGraphOld.sts");
                            }

                            savew.Destroy();

                            savew.PathToFile = PathMS + "/Stats/Month.sts";
                            if (File.Exists(PathMS + "/Stats/Month.sts"))
                            {
                                savew.Load();
                                int hrsm = hrs + savew.GetItemInt("Hours");
                                short mnsm = (short)(mns + (savew.GetItemInt("Mins")));
                                if (mnsm > 59)
                                {
                                    mnsm -= 60;
                                    hrsm++;
                                }

                                int cnt = savew.GetItemInt("Divide");
                                File.Delete(PathMS + "/Stats/Month.sts");
                                savew.Destroy();
                                savew.AddItem("Month", DateTime.Now.Month);
                                savew.AddItem("Hours", hrsm);
                                savew.AddItem("Mins", mnsm);
                                savew.AddItem("Divide", (cnt + 1));
                                savew.Save();
                            }
                            else
                            {
                                if (!Graphskip)
                                {
                                    savew.AddItem("Month", DateTime.Now.Month);
                                    savew.AddItem("Hours", hrs);
                                    savew.AddItem("Mins", mns);
                                    savew.AddItem("Divide", 1);
                                    savew.Save();
                                }
                            }


                            savew.Destroy();
                            savew.PathToFile = PathMS + "/Stats/MonthGraph.sts";
                            if (File.Exists(PathMS + "/Stats/MonthGraph.sts"))
                            {
                                savew.Load();
                                savew.LoadedValuesToSaveValues();
                                int cnt = savew.GetItemInt("Count");
                                cnt++;
                                savew.AddItem("Value" + cnt, ((hrs * 60) + mns));
                                savew.AddItem("Day" + cnt, (DateTime.Now.Day - 1));
                                savew.RemoveItem("Count");
                                savew.AddItem("Count", cnt);
                                savew.Save();
                            }
                            else
                            {
                                if (!Graphskip)
                                {
                                    savew.AddItem("Value1", ((hrs * 60) + mns));
                                    savew.AddItem("Day1", (DateTime.Now.Day - 1));
                                    savew.AddItem("Count", 1);
                                    savew.Save();
                                }
                            }

                            savew.Destroy();
                            savew.PathToFile = PathMS + "/Stats/AllTime.sts";
                            if (File.Exists(PathMS + "/Stats/AllTime.sts"))
                            {
                                savew.Load();
                                int hrsa = savew.GetItemInt("Hours");
                                short mnsa = (short)savew.GetItemInt("Mins");
                                int cnt = savew.GetItemInt("Divide");


                                hrsa += hrs;
                                mnsa += mns;
                                cnt++;

                                if(mnsa>=60)
                                {
                                    hrsa++;
                                    mnsa -= 60;
                                }

                                if (cnt > 30)
                                {
                                    long tms = (hrsa * 60) + mnsa;
                                    long time = (tms / cnt) * 5;
                                    int hour = (int)Math.Floor((decimal)(time / 60));
                                    short minutes = (short)(time - (hour * 60));
                                    hrsa = hour;
                                    mnsa = minutes;
                                    cnt = 5;
                                }

                                File.Delete(PathMS + "/Stats/AllTime.sts");
                                savew.Destroy();
                                savew.AddItem("Hours", hrsa);
                                savew.AddItem("Mins", mnsa);
                                savew.AddItem("Divide", cnt);
                                savew.Save();
                            }
                            else
                            {
                                savew.AddItem("Hours", hrs);
                                savew.AddItem("Mins", mns);
                                savew.AddItem("Divide", 1);
                                savew.Save();
                            }
                            allhour = 0;
                            allmin = 0;

                        }

                    }//ak existuje today.sts



                    if (File.Exists(PathMS + "/Stats/Today.sts"))
                        File.Delete(PathMS + "/Stats/Today.sts");

                    SaveWriter save = new SaveWriter(PathMS + "/Stats/Today.sts");
                    save.AddItem("Date", "" + DateTime.Now);
                    save.AddItem("Hours", allhour);
                    save.AddItem("Mins", allmin);
                    save.AddItem("Day", DateTime.Now.Day);
                    save.Save();
                    save.Destroy();
                }//StatsEn == true
            }catch(Exception ex)
            {
                DebugService.WriteError(""+ex);
            }
        }//SaveStatistics()



        void SaveAll()
        {
            SaveAppMonitoring();
            SaveStatistics();

            if (PCTime)
                SaveRemainingTime();
        }




        //***** On Stop *********
        protected override void OnStop()
        {
            SaveAll();

            DebugService.Write("Stop");
        }
        // --------- On Stop ---------




        private void OnTimerWeb_Tick(object source, ElapsedEventArgs e)
        {
            try
            {
                PostData();
            }catch(Exception ex)
            {
                DebugService.WriteError(""+ex);
            }
        }


        public async void PostData()
        {
            await PostDataTask();
        }


        async Task<int> PostDataTask()
        {
            try
            {
                string jsonapp = JsonConvert.SerializeObject(appinfo,jss);
                Dictionary<string, string> values = new Dictionary<string, string>();
                values.Add("Token", "blZEHudX");
                values.Add("ID", WebID);
                values.Add("Alias", WebAlias);
                values.Add("Appinfo", jsonapp);
                values.Add("PCHour", ""+PCHour);
                values.Add("PCMin", "" + PCMin);

                FormUrlEncodedContent content = new FormUrlEncodedContent(values);

                HttpResponseMessage result = await client.PostAsync("/Connection/PCReport.php", content);
                string resultContent = await result.Content.ReadAsStringAsync();
                DebugService.Write("Web result: "+resultContent);
                return 1;
            }catch(Exception ex)
            {
                DebugService.WriteError(""+ex);
                return 1;
            }
        }



        async void SetOnWeb()
        {

            try
            {
                Dictionary<string, string> values = new Dictionary<string, string>();
                values.Add("Token", "blZEHudX");
                values.Add("ID", WebID);
                values.Add("Alias", WebAlias);

                FormUrlEncodedContent content = new FormUrlEncodedContent(values);

                HttpResponseMessage result = await client.PostAsync("/Connection/SetDeviceOn.php", content);
                string resultContent = await result.Content.ReadAsStringAsync();
                DebugService.Write("Web result SetON: " + resultContent);
            }
            catch (Exception ex)
            {
                DebugService.WriteError("" + ex);
            }
        }




    }// koniec service base
}
