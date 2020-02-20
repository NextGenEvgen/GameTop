using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Widget;
using System.Net;
namespace GameTop
{
    [Activity(Label = "GameTop", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.activity_main);
            var layout = FindViewById<TableLayout>(Resource.Id.tLay);
            ImageView image = new ImageView(this)
            {
                ScaleX = 0.5f,
                ScaleY = 0.5f
            };
            image.SetImageBitmap(GetImageFromUrl("http://192.168.1.104:8080/Dark%20Souls.png"));
            ImageView imageView = new ImageView(this)
            {
                ScaleX = 0.5f,
                ScaleY = 0.5f
            };
            imageView.SetImageBitmap(GetImageFromUrl("http://192.168.1.104:8080/Dark%20Souls.png"));
            layout.AddView(imageView);
            layout.AddView(image);
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