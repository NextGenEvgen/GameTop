using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace GameTop
{
    class Game
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ReleaseDate { get; set; }
        public string DevName { get; set; }
        public int Rating { get; set; }
        public Game() { }
    }

    [Activity(Label = "GamePageActivity")]
    public class GamePageActivity : Activity
    {
        private static List<Review> reviews;
        private Game game;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.game_activity);

            int id = Intent.GetIntExtra("name", 0);
            var gameName = FindViewById<TextView>(Resource.Id.gameName);
            var rating = FindViewById<RatingBar>(Resource.Id.rating);
            var desc = FindViewById<TextView>(Resource.Id.info);
            var release = FindViewById<TextView>(Resource.Id.release);
            var developer = FindViewById<TextView>(Resource.Id.developer);
            var image = FindViewById<ImageView>(Resource.Id.image);
            var genre = FindViewById<TextView>(Resource.Id.genres);
            ListView lis = FindViewById<ListView>(Resource.Id.rev);
            List<string> genres;
            using (WebClient wc = new WebClient())
            {
                string response1 = wc.DownloadString($"http://{Helper.serverIP}:25525/getgame?id={id}");                
                game = JsonConvert.DeserializeObject<Game>(response1);
                string response2 = wc.DownloadString($"http://{Helper.serverIP}:25525/getgenre?id={id}");
                genres = JsonConvert.DeserializeObject<List<string>>(response2);
                string response3 = wc.DownloadString($"http://{Helper.serverIP}:25525/getreviews?id={id}");
                reviews = JsonConvert.DeserializeObject<List<Review>>(response3);
            }

            image.SetImageBitmap(Helper.GetImageFromUrl($"http://{Helper.serverIP}:25525/getimage?name={id}.png"));
            this.Title = game.Name;
            gameName.Text = game.Name;
            rating.Rating = game.Rating;
            desc.Text = game.Description;
            release.Text = $"Дата выхода {game.ReleaseDate}";
            developer.Text = $"Разработчик {game.DevName}";
            foreach (string s in genres)
            {
                genre.Text += s + ", ";
            }
            genre.Text = genre.Text.Remove(genre.Text.Length - 2);            
            ArrayAdapter<Review> adapter = new MyAdapter(this);
            lis.Adapter = adapter;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Intent intent = new Intent(this, typeof(NewReviewActivity));
            intent.PutExtra("id", Intent.GetIntExtra("name", 0));
            StartActivity(intent);
            return base.OnOptionsItemSelected(item);
        }

        class Review
        {
            public string Nickname { get; set; }
            public string Content { get; set; }
            public Review() { }
        }
        class MyAdapter : ArrayAdapter<Review>
        {

            public MyAdapter(Context context) : base(context, Resource.Layout.layout1, reviews) { }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                Review rev = GetItem(position);
                if (convertView == null)
                {
                    convertView = LayoutInflater.From(Context).Inflate(Resource.Layout.layout1, null);
                }
                ((TextView)convertView.FindViewById(Resource.Id.author)).SetText(rev.Nickname, TextView.BufferType.Normal);
                ((TextView)convertView.FindViewById(Resource.Id.comments)).SetText(rev.Content, TextView.BufferType.Normal);
                return convertView;
            }

        }
    }


}