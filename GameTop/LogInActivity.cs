using Android.App;
using Android.Content;
using Android.OS;
using Android.Text;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace GameTop
{
    [Activity(Label = "Вход", Theme = "@android:style/Theme.Material.Light.DarkActionBar")]
    class LogInActivity : Activity
    {
        private Button btn;
        private TextView login, password, reg;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.login_activity);
            btn = FindViewById<Button>(Resource.Id.btn);
            login = FindViewById<TextView>(Resource.Id.login);
            password = FindViewById<TextView>(Resource.Id.pass);
            reg = FindViewById<TextView>(Resource.Id.reg);
            reg.Click += OnRegClick;
            btn.Click += OnClick;
        }

        private void OnRegClick(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(RegistrationActivity));
            StartActivity(intent);
        }

        private void OnClick(object sender, EventArgs e)
        {
            if (password.Text == "" || login.Text == "")
            {
                Toast.MakeText(Application.Context, "Все поля должны быть заполнены", ToastLength.Short).Show();
                return;
            }
            using (WebClient wc = new WebClient())
            {
                var responce = wc.DownloadString($"http://{Helper.serverIP}:25525/auth?login={login.Text}&password={Helper.ComputeHash(password.Text)}");
                if (responce != "allowed")
                {
                    Toast.MakeText(Application.Context, "Ошибка авторизации! Попробуйте еще раз", ToastLength.Short).Show();
                }
                else
                {                    
                    MainActivity.CurrentUser = new User(wc.DownloadString($"http://{Helper.serverIP}:25525/getusernick?login={login.Text}"), login.Text,wc.DownloadString( $"http://{Helper.serverIP}:25525/getuserdate?login={login.Text}"));
                    string resp = wc.DownloadString($"http://{Helper.serverIP}:25525/getuserreviews?login={login.Text}");
                    MainActivity.CurrentUser.Scores = JsonConvert.DeserializeObject<List<GameScore>>(resp);
                    Intent intent = new Intent(this, typeof(MainActivity));
                    StartActivity(intent);
                    
                }
                wc.Dispose();
            }
        }

        protected override void OnStop()
        {
            base.OnStop();
            Finish();
        }

    }
}