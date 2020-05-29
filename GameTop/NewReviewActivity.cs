using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace GameTop
{
    [Activity(Label = "Создание обзора")]
    public class NewReviewActivity : Activity
    {
        private User user;
        private int id;
        private EditText review;
        private RatingBar rating;
        private Button button;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.new_review);
            id = Intent.GetIntExtra("id", 0);
            user = MainActivity.CurrentUser;
            review = FindViewById<EditText>(Resource.Id.review);
            rating = FindViewById<RatingBar>(Resource.Id.rating);
            button = FindViewById<Button>(Resource.Id.button);
            rating.NumStars = 5;
            rating.Rating = 0;
            rating.StepSize = 1;
            button.Click += OnClick;
        }

        private void OnClick(object sender, EventArgs e)
        {
            if (user == null)
            {
                Toast.MakeText(this, "Чтобы получить возможность писать обзоры необходимо авторизироваться!", ToastLength.Short).Show();
                return;
            }
            using (WebClient wc = new WebClient())
            {
                wc.DownloadString($"http://{Helper.serverIP}:25525/postreview?id={id}&rating={rating.Rating}&review={review.Text}&user={user.Login}");
            }
            Intent intent = new Intent(this, typeof(GamePageActivity));
            intent.PutExtra("name", id);
            StartActivity(intent);
        }

        protected override void OnStop()
        {
            base.OnStop();
            Finish();
        }
    }
}