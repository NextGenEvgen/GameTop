using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Widget;
using Android.Views;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Views.Accessibility;

namespace GameTop
{
    [Activity(Label = "GameTop", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : FragmentActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {            
            base.OnCreate(savedInstanceState);
            RequestWindowFeature(WindowFeatures.ActionBar);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            
            SetContentView(Resource.Layout.activity_main);
            

            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            var pager = FindViewById<ViewPager>(Resource.Id.viewPager);
            var adapter = new PageFragmentAdapter(SupportFragmentManager);
            adapter.AddFragmentView((i, v, b) =>
            {
                var view = i.Inflate(Resource.Layout.tab,v, false);
                var txt = view.FindViewById<TextView>(Resource.Id.text);
                var layout = view.FindViewById<GridLayout>(Resource.Id.grid);
                layout.RowCount = 2;
                //TODO: Переделать под парсинг JSON файла
                ImageButton image = Helper.GetImageButton("Dark Souls", this);
                ImageButton image1 = Helper.GetImageButton("Dota 2", this);
                ImageButton image2 = Helper.GetImageButton("Overwatch", this);
                image.Click += GoToOtherActivity;
                image1.Click += GoToOtherActivity;
                image2.Click += GoToOtherActivity;
                layout.AddView(image);
                layout.AddView(image1);
                layout.AddView(image2);
                return view;
            });
            var adapter1 = new PageFragmentAdapter(SupportFragmentManager);
            adapter.AddFragmentView((i, v, b) =>
            {
                var view = i.Inflate(Resource.Layout.tab, v, false);
                var txt = view.FindViewById<TextView>(Resource.Id.text);
                //txt.Text = "hi from second page";
                return view;
            });
            var adapter2 = new PageFragmentAdapter(SupportFragmentManager);
            adapter.AddFragmentView((i, v, b) =>
            {
                var view = i.Inflate(Resource.Layout.tab, v, false);
                var txt = view.FindViewById<TextView>(Resource.Id.text);
                //txt.Text = "hi from third page";
                return view;
            });

            pager.Adapter = adapter;

            pager.SetOnPageChangeListener(new ViewPageListener(ActionBar));
            ActionBar.AddTab(pager.GetTab(ActionBar, "Игры"));
            ActionBar.AddTab(pager.GetTab(ActionBar, "Новости"));
            ActionBar.AddTab(pager.GetTab(ActionBar, "Профиль"));            
            //var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            //SetActionBar(toolbar);
            //ActionBar.Title = "Toolbar";
            //tabLayout = FindViewById<TabLayout>(Resource.Id.tabs);
            //ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
        }

        private void InitTabs()
        {
            //tabLayout.SetTabTextColors(Android.Graphics.Color.Aqua, Android.Graphics.Color.AntiqueWhite);
            //var fragments = new Android.Support.V4.App.Fragment[]
            //{
            //    new BlueFragment(),
            //};
        }

        private void GoToOtherActivity(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(GamePageActivity));            
            intent.PutExtra("name", "Dark Souls");            
            StartActivity(intent);            
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}