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
using Android.Support.Design.Widget;
using System;
using static Android.Resource;
using Android.Text;

namespace GameTop
{
    [Activity(Label = "GameTop", Theme = "@style/CustomTheme", MainLauncher = true)]
    public class MainActivity : FragmentActivity
    {
        public static User CurrentUser { get; set; }

        private DrawerLayout drawerLayout;
        private Button genreButton;
        private Button scoreButton;
        private GridLayout gamesGrid;
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
                gamesGrid = view.FindViewById<GridLayout>(Resource.Id.grid);
                genreButton = view.FindViewById<Button>(Resource.Id.menuGenreButton);
                genreButton.Click += GenreButtonOnClick;
                scoreButton = view.FindViewById<Button>(Resource.Id.menuRatingButton);
                scoreButton.Click += ScoreButtonOnClick;
                gamesGrid.RowCount = 2;
                LoadGames("All", 0, gamesGrid);
                return view;
            });
            
            var adapter1 = new PageFragmentAdapter(SupportFragmentManager);
            adapter.AddFragmentView((i, v, b) =>
            {
                var view = i.Inflate(Resource.Layout.news, v, false);
                var layout = view.FindViewById<LinearLayout>(Resource.Id.lay);
                string[] results;
                using (WebClient wc = new WebClient())
                {
                    string response = wc.DownloadString($"http://{Helper.serverIP}:25525/getnews");
                    var result = JsonConvert.DeserializeObject<string>(response).Replace("]","").Replace("[","");
                    results = result.Split(new string[] { "', '", "\", \"" },System.StringSplitOptions.RemoveEmptyEntries);
                }
                for (int index = 0; index < results.Length / 2; index++)
                {
                    NewsView news = new NewsView(this, null, results[index * 2], results[index * 2 + 1]);
                    layout.AddView(news.Layout);
                }                
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
                    var linLay = view.FindViewById<LinearLayout>(Resource.Id.linLay);
                    var logout = view.FindViewById<Button>(Resource.Id.logout);
                    var nick = view.FindViewById<TextView>(Resource.Id.labelNickName2);
                    nick.Text = CurrentUser.Nickname;
                    var registerDate = view.FindViewById<TextView>(Resource.Id.labelDate2);
                    registerDate.Text = CurrentUser.RegDate;
                    foreach (GameScore gs in CurrentUser.Scores)
                    {
                        linLay.AddView(new UserGame(this, null, gs.Name, gs.Score).Layout);
                    }
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

        private void ScoreButtonOnClick(object sender, EventArgs e)
        {            
            var menu = new PopupMenu(this, scoreButton);

            menu.MenuInflater.Inflate(Resource.Menu.menu, menu.Menu);
            for (int i = 0; i < 5; i++)
            {
                menu.Menu.Add($">={i}");
            }

            menu.MenuItemClick += (s, e) =>
            {
                scoreButton.Text = e.Item.TitleFormatted.ToString();
                gamesGrid.RemoveAllViews();
                if (genreButton.Text == "Все жанры")
                {
                    LoadGames("All", int.Parse(scoreButton.Text.Replace(">=", "")), gamesGrid);
                }
                else
                {
                    LoadGames(genreButton.Text, int.Parse(scoreButton.Text.Replace(">=", "")), gamesGrid);
                }
            };
            menu.Show();
        }

        private void GenreButtonOnClick(object sender, System.EventArgs e)
        {
            var btn = FindViewById<Button>(Resource.Id.menuGenreButton);
            var menu = new PopupMenu(this, btn);

            menu.MenuInflater.Inflate(Resource.Menu.menu, menu.Menu);

            List<string> genres;
            using (WebClient wc = new WebClient())
            {
                string response = wc.DownloadString($"http://{Helper.serverIP}:25525/getgenres");
                genres = JsonConvert.DeserializeObject<List<string>>(response);
            }
            menu.Menu.Add("Все жанры");
            foreach (string s in genres)
            {
                menu.Menu.Add(s);
            }

            menu.MenuItemClick += (s, e) =>
            {
                genreButton.Text = e.Item.TitleFormatted.ToString();
                gamesGrid.RemoveAllViews();
                if (genreButton.Text == "Все жанры")
                {
                    LoadGames("All", int.Parse(scoreButton.Text.Replace(">=","")), gamesGrid);
                }
                else
                {
                    LoadGames(genreButton.Text, int.Parse(scoreButton.Text.Replace(">=", "")), gamesGrid);
                }
            };
            menu.Show();
        }

        private void OnEnterClick(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(LogInActivity));
            StartActivity(intent);
        }

        private void LoadGames(string genre, int score, ViewGroup objToPlace)
        {
            List<int> games;
            using (WebClient wc = new WebClient())
            {
                string response = wc.DownloadString($"http://{Helper.serverIP}:25525/getgameslist?genre={genre}&rating={score}");
                games = JsonConvert.DeserializeObject<List<int>>(response);
            }
            foreach (int s in games)
            {
                ImageButton image = Helper.GetImageButton(s.ToString(), this);
                image.TransitionName = s.ToString();
                image.Click += GoToOtherActivity;
                objToPlace.AddView(image);
            }
        }

        private void GoToOtherActivity(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(GamePageActivity));
            intent.PutExtra("name", int.Parse((sender as ImageButton).TransitionName));
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