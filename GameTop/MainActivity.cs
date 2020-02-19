using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;

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
using Java.Net;
using Android.Graphics;
using Java.IO;
using Android.Graphics.Drawables;
using Android.Util;
using System.Net;
using System.IO;
namespace GameTop
{
    [Activity(Label = "GameTop", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        //ImageButton button;

        //int counter = 0;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource

            SetContentView(Resource.Layout.activity_main);

            FindViewById<ImageView>(Resource.Id.imageView1).SetImageBitmap(GetImageFromUrl("http://192.168.1.104:8080/Dark%20Souls.png"));
            //image.SetImageBitmap(GetImageFromUrl("https://i.redd.it/lb9h2bfqnuh41.png"));
            //Android.Net.Uri url = Android.Net.Uri.Parse("https://i.redd.it/lb9h2bfqnuh41.png");
            //image.SetImageURI(url);

            //ImageButton button = FindViewById<ImageButton>(Resource.Id.imageButton1);
            //button.SetImageURI(Android.Net.Uri.Parse("https://zhitanska.com/wp-content/uploads/2019/01/rani-ki-vav-11.jpg"));
        }

        private Bitmap GetImageFromUrl(string url)
        {
            Bitmap image = null;
            using (var webClient = new WebClient())
            {
                var bytes = webClient.DownloadData(url);
                if (bytes != null && bytes.Length > 0)
                {
                    image = BitmapFactory.DecodeByteArray(bytes, 0, bytes.Length);
                }
            }
            return image;
        }

        private void Button_Click(object sender, System.EventArgs e)
        {
            //button.SetImageURI()
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}