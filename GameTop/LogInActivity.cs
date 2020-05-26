using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;
using System.Net;

namespace GameTop
{
    [Activity(Label = "Вход", Theme = "@android:style/Theme.Material.Light.DarkActionBar")]
    class LogInActivity : Activity
    {
        private Button btn;
        private TextView login, password;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.login_activity);
            btn = FindViewById<Button>(Resource.Id.btn);
            login = FindViewById<TextView>(Resource.Id.login);
            password = FindViewById<TextView>(Resource.Id.pass);
            btn.Click += OnClick;
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
                var responce = wc.DownloadString($"http://{Helper.serverIP}:25525/auth?login=kek@lul.ru&password=qwerty123");
                if (responce != "allowed")
                {
                    Toast.MakeText(Application.Context, "Ошибка авторизации! Попробуйте еще раз", ToastLength.Short).Show();
                }
                else
                {
                    MainActivity.CurrentUser = new User("kekPro");
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