using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json;

namespace GameTop
{
    [Activity(Label = "GamePageActivity", Theme = "@style/CustomTheme")]
    public class GamePageActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.game_activity);

            var txt = FindViewById<TextView>(Resource.Id.txt);
            string name = Intent.GetStringExtra("name");
            txt.Text = name;
            this.Title = name;            

            var rating = FindViewById<RatingBar>(Resource.Id.rating);

            //rating.OnRatingBarChangeListener = new
            
            //rating.NumStars = 10;
            //rating.Rating = 7;
        }

    }
}