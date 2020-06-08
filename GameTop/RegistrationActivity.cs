using Android.App;
using Android.OS;
using Android.Widget;
using System;
using System.Net;
using System.Text.RegularExpressions;

namespace GameTop
{
    [Activity(Label = "Регистрация", Theme = "@android:style/Theme.Material.Light.DarkActionBar")]
    class RegistrationActivity : Activity
    {
        Regex regex = new Regex(@"^[a-z0-9]{8,}$", RegexOptions.IgnoreCase);
        private EditText login, nickname, password, secondPassword;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.registration_activity);
            var button = FindViewById<Button>(Resource.Id.btn);
            login = FindViewById<EditText>(Resource.Id.login);
            nickname = FindViewById<EditText>(Resource.Id.nickname);
            password = FindViewById<EditText>(Resource.Id.pass);
            secondPassword = FindViewById<EditText>(Resource.Id.secPass);
            button.Click += OnClick;
        }

        private void OnClick(object sender, EventArgs e)
        {
            if (login.Text == "" || nickname.Text == "" || password.Text == "" || secondPassword.Text == "")
            {
                Toast.MakeText(this, "Все поля должны быть заполнены!", ToastLength.Short).Show();
                return;
            }
            if (password.Text != secondPassword.Text)
            {
                Toast.MakeText(this, "Пароли не совпадают!", ToastLength.Short).Show();
                return;
            }
            if (!regex.IsMatch(password.Text))
            {
                Toast.MakeText(this, "Неподходящий пароль! Пароль должен содержать не менее 8 символов латиницы или цифр!", ToastLength.Short).Show();
                return;
            }
            using (WebClient wc = new WebClient())
            {
                wc.DownloadString($"http://{Helper.serverIP}:25525/reg?login={login.Text}&nick={nickname.Text}&pass={Helper.ComputeHash(password.Text)}");
            }
        }
    }
}