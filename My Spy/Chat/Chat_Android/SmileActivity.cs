using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Util;
using Android.Content.Res;
using Android.Media;
using Android.Content;

namespace Chat_Android
{
    [Activity(Label = "Smiles")]
    public class SmileActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SmileLayout);


            float Width = Resources.DisplayMetrics.WidthPixels / Resources.DisplayMetrics.Density;

            GridLayout grid = FindViewById<GridLayout>(Resource.Id.gridLayout1);

            grid.ColumnCount = (int)(Width / 149);

            FindViewById<ImageButton>(Resource.Id.imageButton1).Click += ClickSmile;
            FindViewById<ImageButton>(Resource.Id.imageButton2).Click += ClickSmile;
            FindViewById<ImageButton>(Resource.Id.imageButton3).Click += ClickSmile;
            FindViewById<ImageButton>(Resource.Id.imageButton4).Click += ClickSmile;
            FindViewById<ImageButton>(Resource.Id.imageButton5).Click += ClickSmile;
            FindViewById<ImageButton>(Resource.Id.imageButton6).Click += ClickSmile;
            FindViewById<ImageButton>(Resource.Id.imageButton7).Click += ClickSmile;
            FindViewById<ImageButton>(Resource.Id.imageButton8).Click += ClickSmile;
            FindViewById<ImageButton>(Resource.Id.imageButton9).Click += ClickSmile;
            FindViewById<ImageButton>(Resource.Id.imageButton10).Click += ClickSmile;
            FindViewById<ImageButton>(Resource.Id.imageButton11).Click += ClickSmile;
            FindViewById<ImageButton>(Resource.Id.imageButton12).Click += ClickSmile;
            FindViewById<ImageButton>(Resource.Id.imageButton13).Click += ClickSmile;
            FindViewById<ImageButton>(Resource.Id.imageButton14).Click += ClickSmile;
            FindViewById<ImageButton>(Resource.Id.imageButton15).Click += ClickSmile;
            FindViewById<ImageButton>(Resource.Id.imageButton16).Click += ClickSmile;

        }

        public void ClickSmile(object sender, EventArgs e)
        {

            ImageButton img = (ImageButton)sender;
             MainActivity.SmileCode = "[*" + (string)img.Tag + "*]";
            MainActivity.SmileCodeIM = int.Parse((string)img.Tag);
            MainActivity.Smile = true;
            Finish();
        }




        public static Android.Graphics.Drawables.Drawable GetSmileImage(Context con, int code)
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                switch (code)
                {
                    case 1:
                        return con.GetDrawable(Resource.Drawable.smile1);
                    case 2:
                        return con.GetDrawable(Resource.Drawable.smile2);
                    case 3:
                        return con.GetDrawable(Resource.Drawable.smile3);
                    case 4:
                        return con.GetDrawable(Resource.Drawable.smile4);
                    case 5:
                        return con.GetDrawable(Resource.Drawable.smile5);
                    case 6:
                        return con.GetDrawable(Resource.Drawable.smile6);
                    case 7:
                        return con.GetDrawable(Resource.Drawable.smile7);
                    case 8:
                        return con.GetDrawable(Resource.Drawable.smile8);
                    case 9:
                        return con.GetDrawable(Resource.Drawable.smile9);
                    case 10:
                        return con.GetDrawable(Resource.Drawable.smile10);
                    case 11:
                        return con.GetDrawable(Resource.Drawable.smile11);
                    case 12:
                        return con.GetDrawable(Resource.Drawable.smile12);
                    case 13:
                        return con.GetDrawable(Resource.Drawable.Smile13);
                    case 14:
                        return con.GetDrawable(Resource.Drawable.Smile14);
                    case 15:
                        return con.GetDrawable(Resource.Drawable.Smile15);
                    case 16:
                        return con.GetDrawable(Resource.Drawable.Smile16);
                    default:
                        return null;
                }

            }else
            {
                switch (code)
                {
                    case 1:
                        return con.Resources.GetDrawable(Resource.Drawable.smile1);
                    case 2:
                        return con.Resources.GetDrawable(Resource.Drawable.smile2);
                    case 3:
                        return con.Resources.GetDrawable(Resource.Drawable.smile3);
                    case 4:
                        return con.Resources.GetDrawable(Resource.Drawable.smile4);
                    case 5:
                        return con.Resources.GetDrawable(Resource.Drawable.smile5);
                    case 6:
                        return con.Resources.GetDrawable(Resource.Drawable.smile6);
                    case 7:
                        return con.Resources.GetDrawable(Resource.Drawable.smile7);
                    case 8:
                        return con.Resources.GetDrawable(Resource.Drawable.smile8);
                    case 9:
                        return con.Resources.GetDrawable(Resource.Drawable.smile9);
                    case 10:
                        return con.Resources.GetDrawable(Resource.Drawable.smile10);
                    case 11:
                        return con.Resources.GetDrawable(Resource.Drawable.smile11);
                    case 12:
                        return con.Resources.GetDrawable(Resource.Drawable.smile12);
                    case 13:
                        return con.Resources.GetDrawable(Resource.Drawable.Smile13);
                    case 14:
                        return con.Resources.GetDrawable(Resource.Drawable.Smile14);
                    case 15:
                        return con.Resources.GetDrawable(Resource.Drawable.Smile15);
                    case 16:
                        return con.Resources.GetDrawable(Resource.Drawable.Smile16);
                    default:
                        return null;
                }
            }

        }


    }
}