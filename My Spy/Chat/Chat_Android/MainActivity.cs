using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using Android.Util;
using Chat;
using Chat.Xmpp;
using Sharp.Xmpp.Im;
using Android.Graphics;
using static Android.Views.ViewGroup;
using Android.Net;
using Android.Content;
using Android.Preferences;
using EncryptionLibrary;

namespace Chat_Android
{
    [Activity(Label = "My Spy Chat", MainLauncher = true, Icon = "@drawable/icon", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
    public class MainActivity : Activity
    {
        EditText edit;
        LinearLayout chatlay;
        ScrollView scrollview;
        ImageView onlineimg;
        TextView onlinetxt;

        bool ScrollDown = false;

        string ReceiverJID = "";

        bool Administrator = false;
        private string UserAc, PasswordAc;

        public static string SmileCode = "";
        public static bool Smile = false;
        public static int SmileCodeIM = 0;

        XmppConnection connection = new XmppConnection();

        XmppMessage message = new XmppMessage("l5wja9bqvTiFsTzHO77eir5js");
        Vibrator vibrator;

        Encryption encryptpass = new Encryption("Mo4onbrP3ovlTFbPQAq66XTs7Em0rXck");

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);


            ConnectivityManager connectivityManager = (ConnectivityManager)GetSystemService(ConnectivityService);

            NetworkInfo activeConnection = connectivityManager.ActiveNetworkInfo;
            bool isOnline = (activeConnection != null) && activeConnection.IsConnected;


            if (!isOnline)
            {
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("No network access!");
                alert.SetMessage("Can't connect to server. No network access.");
                
                alert.SetPositiveButton("OK", (senderAlert, args) => {
                    Finish();
                });

                Dialog dialog = alert.Create();
                dialog.Show();
            }
            else
            {

                SetContentView(Resource.Layout.Main);

                Log.Debug("Start", "Started");


                LoadValues();

                if (string.IsNullOrEmpty(UserAc))
                {
                    StartActivity(typeof(LoginActivity));
                    Finish();
                }
                else
                {


                    ImageButton sendbutton = FindViewById<ImageButton>(Resource.Id.imageButtonSend);
                    ImageButton smilebutton = FindViewById<ImageButton>(Resource.Id.imgButtonSmile);

                    edit = FindViewById<EditText>(Resource.Id.editTextMessage);

                    chatlay = FindViewById<LinearLayout>(Resource.Id.linearLayoutchat);

                    scrollview = FindViewById<ScrollView>(Resource.Id.scrollviewchat);

                    onlineimg = FindViewById<ImageView>(Resource.Id.imageViewStatus);
                    onlinetxt = FindViewById<TextView>(Resource.Id.textViewStatus);


                    sendbutton.Click += delegate
                    {

                        SendMessage(null);
                    };


                    smilebutton.Click += delegate
                    {
                        Log.Debug("Smile Activity", "Smile Activity started");
                        StartActivity(typeof(SmileActivity));
                    };


                    System.Threading.Timer timer = new System.Threading.Timer((e) =>
                    {
                        if (ScrollDown)
                        {

                            RunOnUiThread(() =>
                            {
                                scrollview.FullScroll(FocusSearchDirection.Down);
                                ScrollDown = false;
                            });
                        }


                        if (Smile)
                        {

                            RunOnUiThread(() =>
                            {


                                Smile = false;
                                message.SendMessage(connection.GetXmppIm(), ReceiverJID, SmileCode);
                                ImageView img = new ImageView(this);
                                img.SetImageDrawable(SmileActivity.GetSmileImage(this, SmileCodeIM));
                                img.LayoutParameters = new ViewGroup.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);
                                chatlay.AddView(img);
                                ScrollDown = true;
                            });
                        }


                    }, null, 500, 200);





                    System.Threading.Timer timerstatus = new System.Threading.Timer((e) =>
                    {
                        connection.SetONIM(true);

                    }, null, 5000, 15000);


                    string usr = "MySpy_" + UserAc;

                    if (Administrator)
                    {
                        ReceiverJID = usr + "@" + XmppConnection.Server;
                        usr = "Admin_" + usr;
                    }
                    else
                    {
                        ReceiverJID = "Admin_" + usr + "@" + XmppConnection.Server;
                    }


                    vibrator = (Vibrator)GetSystemService(VibratorService);

                    

                    
                    connection.ConnectToXMPPIM(usr, PasswordAc);


                    Log.Debug("XMPP", "Connected as " + connection.GetXmppIm().Jid);
                    connection.GetXmppIm().Message += Connection_Message;


                    connection.AddToRoster(ReceiverJID);

                    connection.GetXmppIm().RequestSubscription(ReceiverJID);
                    connection.ApproveRequest(ReceiverJID);

                    connection.GetXmppIm().Status += Connection_Status;
                    
                }
            }
        }


        void LoadValues()
        {
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            bool saved = prefs.GetBoolean("Saved", false);
            if (saved)
            {
                Administrator = prefs.GetBoolean("Administrator", false);
                UserAc = prefs.GetString("User", "");
                PasswordAc = encryptpass.DecryptString(prefs.GetString("Password", ""), "p9JHopIpNQNk6B6YJbLeNxOeqnfK");
            }
        }




        private void Connection_Status(object sender, StatusEventArgs e)
        {
            if (e.Jid.GetBareJid() == ReceiverJID)
            {
                if (e.Status.Availability == Availability.Online)
                {

                    RunOnUiThread(() =>
                    {
                        onlinetxt.SetText("Online", TextView.BufferType.Normal);

                        if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                        {
                            onlineimg.SetImageDrawable(this.GetDrawable(Resource.Drawable.on));
                        }else
                        {
                            onlineimg.SetImageDrawable(Resources.GetDrawable(Resource.Drawable.on));
                        }
                    });
                }
                else
                {
                    RunOnUiThread(() =>
                    {
                        onlinetxt.SetText("Offline", TextView.BufferType.Normal);

                        if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                        {
                            onlineimg.SetImageDrawable(this.GetDrawable(Resource.Drawable.off));
                        }
                        else
                        {
                            onlineimg.SetImageDrawable(Resources.GetDrawable(Resource.Drawable.off));
                        }
                    });
                }
            }
        }


        private void Connection_Message(object sender, MessageEventArgs e)
        {

            string txt = message.DecryptMessage(e.Message.Body);

            Log.Debug("MessageArrived", "Message: "+txt);

            if (txt[0] == '[' && txt[1] == '*')
            {
                
                try
                {

                    RunOnUiThread(() =>
                    {
                        txt = txt.Replace("[*", "");
                        txt = txt.Replace("*]", "");

                        ImageView img = new ImageView(this);
                        img.SetImageDrawable(SmileActivity.GetSmileImage(this,int.Parse(txt)));
                        img.LayoutParameters = new ViewGroup.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);
                        chatlay.AddView(img);
                        ScrollDown = true;

                    });

                }
                catch (Java.Lang.Exception ex)
                {
                    Log.Debug("Error","" + ex);
                }
                

                vibrator.Vibrate(200);

            }
            else
            {


                RunOnUiThread(() =>
                {
                    TextView txtv = new TextView(this);
                    txtv.Text = txt;
                    txtv.SetTextColor(Color.White);
                    txtv.TextSize = 16f;

                    chatlay.AddView(txtv);
                    ScrollDown = true;

                });
                vibrator.Vibrate(200);
            }

        }//OnMessage

        public void SendMessage(View v)
        {
            Log.Debug("Click","SendMessage");

            if (!string.IsNullOrWhiteSpace(edit.Text))
            {
                TextView txtv = new TextView(this);
                txtv.Text = edit.Text;
                txtv.TextSize = 16f;
                txtv.SetTextColor(Color.Orange);

                chatlay.AddView(txtv);
                ScrollDown = true;

                message.SendMessage(connection.GetXmppIm(), ReceiverJID, edit.Text);

                edit.Text = "";
            }
        }


    }
}

