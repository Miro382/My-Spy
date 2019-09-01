using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Preferences;
using EncryptionLibrary;
using Sharp.Xmpp.Client;
using Android.Util;

namespace Chat_Android
{
    [Activity(Label = "Login")]
    public class LoginActivity : Activity
    {
        EditText Password, Username;
        RadioButton Admin;
        Button Login;

        const string Server = "0nl1ne.cc";
        const string AppPrefix = "MySpy_";

        Encryption256 encrypt = new Encryption256("8B1JqdrHLD6qJIdZk9oWwSuWrpoPcw");
        Encryption encryptpass = new Encryption("Mo4onbrP3ovlTFbPQAq66XTs7Em0rXck");


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.LoginLayout);


            Password = FindViewById<EditText>(Resource.Id.editTextpass);
            Username = FindViewById<EditText>(Resource.Id.editTextuser);

            Admin = FindViewById<RadioButton>(Resource.Id.radioButtonadmin);
            Login = FindViewById<Button>(Resource.Id.buttonlogin);


            Login.Click += delegate
            {
                LoginClick();
            };
        }



        void LoginClick()
        {
            if (!string.IsNullOrWhiteSpace(Username.Text))
            {
                if (!string.IsNullOrWhiteSpace(Password.Text))
                {
                    try
                    {
                        string pass = encrypt.EncryptText(Password.Text, "ZuhajoLafyNAYjCAw2VZfaYo");
                        XmppClient client = new XmppClient(Server, AppPrefix + Username.Text, pass);
                        client.Connect();

                        Log.Debug("Debug","Logged in as: " + client.Jid);


                        XmppClient clientad = new XmppClient(Server, "Admin_" + AppPrefix + Username.Text, pass);
                        clientad.Connect();

                        Log.Debug("Debug", "Logged in as: " + clientad.Jid);

                        SaveValues(Admin.Checked, Username.Text, pass);

                        Toast.MakeText(this, "Logged in. OK. Start application to start.", ToastLength.Long).Show();
                        Finish();

                    }
                    catch (Exception ex)
                    {
                        Toast.MakeText(this,"User not exist, or bad password",ToastLength.Long).Show();
                    }
                }
                else
                {
                    Toast.MakeText(this, "Empty password", ToastLength.Long).Show();
                }
            }
            else
            {
                Toast.MakeText(this, "Empty Username", ToastLength.Long).Show();
            }
        }



        void SaveValues(bool Admin,string User,string Pass)
        {
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            ISharedPreferencesEditor editor = prefs.Edit();

            editor.PutBoolean("Saved", true);

            editor.PutBoolean("Administrator", Admin);
            editor.PutString("User", User);
            editor.PutString("Password", encryptpass.EncryptString(Pass, "p9JHopIpNQNk6B6YJbLeNxOeqnfK"));

            editor.Commit();
        }

    }
}