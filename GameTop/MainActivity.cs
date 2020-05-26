using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GameTop
{
    [Activity(Label = "GameTop", Theme = "@style/CustomTheme", MainLauncher = true)]
    public class MainActivity : FragmentActivity
    {
        public static User CurrentUser { get; set; }

        private DrawerLayout drawerLayout;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RequestWindowFeature(WindowFeatures.ActionBar);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            ActionBar.SetHomeButtonEnabled(true);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            var pager = FindViewById<ViewPager>(Resource.Id.viewPager);
            var adapter = new PageFragmentAdapter(SupportFragmentManager);
            SetUpAdapters();
            ActionBar.AddTab(pager.GetTab(ActionBar, "Игры"));
            ActionBar.AddTab(pager.GetTab(ActionBar, "Новости"));
            ActionBar.AddTab(pager.GetTab(ActionBar, "Профиль"));
        }

        private void SetUpAdapters()
        {
            var pager = FindViewById<ViewPager>(Resource.Id.viewPager);
            var adapter = new PageFragmentAdapter(SupportFragmentManager);
            adapter.AddFragmentView((i, v, b) =>
            {
                var view = i.Inflate(Resource.Layout.tab, v, false);
                var txt = view.FindViewById<TextView>(Resource.Id.text);
                var layout = view.FindViewById<GridLayout>(Resource.Id.grid);
                layout.RowCount = 2;
                List<string> games;
                using (WebClient wc = new WebClient())
                {
                    string response = wc.DownloadString($"http://{Helper.serverIP}:25525/getgameslist");
                    games = JsonConvert.DeserializeObject<List<string>>(response);
                }
                foreach (string s in games)
                {
                    ImageButton image = Helper.GetImageButton(s, this);                    
                    image.TransitionName = s;
                    image.Click += GoToOtherActivity;
                    layout.AddView(image);
                }
                return view;
            });
            var adapter1 = new PageFragmentAdapter(SupportFragmentManager);
            adapter.AddFragmentView((i, v, b) =>
            {
                var view = i.Inflate(Resource.Layout.news, v, false);
                var txt = view.FindViewById<TextView>(Resource.Id.text);
                return view;
            });
            var adapter2 = new PageFragmentAdapter(SupportFragmentManager);
            adapter.AddFragmentView((i, v, b) =>
            {
                if (CurrentUser == null)
                {
                    var view = i.Inflate(Resource.Layout.guest, v, false);
                    view.FindViewById<TextView>(Resource.Id.msg).Text = "Для просмотра своего профиля необходимо совершить авторизацию";
                    var btn = view.FindViewById<Button>(Resource.Id.login);
                    btn.Text = "Войти";
                    btn.Click += OnEnterClick;
                    return view;
                }
                else
                {
                    var view = i.Inflate(Resource.Layout.profile, v, false);
                    var logout = view.FindViewById<Button>(Resource.Id.logout);
                    var nick = view.FindViewById<TextView>(Resource.Id.nickname);
                    nick.Text = CurrentUser.Nickname;
                    logout.Text = "Выйти";
                    logout.Click += (o, e) =>
                    {
                        CurrentUser = null;
                        pager.CurrentItem = 0;
                        SetUpAdapters();
                    };
                    return view;
                }

            });
            pager.Adapter = adapter;
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer);
            pager.SetOnPageChangeListener(new ViewPageListener(ActionBar));
        }

        private void OnEnterClick(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(LogInActivity));
            StartActivity(intent);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            this.MenuInflater.Inflate(Resource.Menu.nav_menu, menu);
            var searchItem = menu.FindItem(Resource.Id.action_search);
            return base.OnCreateOptionsMenu(menu);
        }

        private void GoToOtherActivity(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(GamePageActivity));
            intent.PutExtra("name", (sender as ImageButton).TransitionName);
            StartActivity(intent);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    {
                        drawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
                        return true;
                    }
            }
            return base.OnOptionsItemSelected(item);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}