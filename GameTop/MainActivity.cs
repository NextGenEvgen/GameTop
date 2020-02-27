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
            var layout = FindViewById<GridLayout>(Resource.Id.grid);
            layout.RowCount = 2;

            ImageButton image = Helper.GetImageButton("Dark Souls", this);
            ImageButton image1 = Helper.GetImageButton("Dota 2", this);
            ImageButton image2 = Helper.GetImageButton("Overwatch", this);
            image.Click += GoToOtherActivity;
            image1.Click += GoToOtherActivity;
            image2.Click += GoToOtherActivity;
            layout.AddView(image);
            layout.AddView(image1);
            layout.AddView(image2);
        }

        private void GoToOtherActivity(object sender, System.EventArgs e)
        {
            StartActivity(typeof(GamePageActivity));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}