using Android.App;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Views;
using System;
using System.Collections.Generic;

namespace GameTop
{
    public class PageFragmentAdapter : FragmentPagerAdapter
    {
        private List<Android.Support.V4.App.Fragment> fragments = new List<Android.Support.V4.App.Fragment>();
        public PageFragmentAdapter(Android.Support.V4.App.FragmentManager fm) : base(fm) { }

        public void AddFragmentView(Func<LayoutInflater, ViewGroup, Bundle, View> view)
        {
            fragments.Add(new PageFragment(view));
        }

        public void AddFragment(PageFragment page)
        {
            fragments.Add(page);
        }

        public override int Count => fragments.Count;

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            return fragments[position];
        }
    }

    public class ViewPageListener : ViewPager.SimpleOnPageChangeListener
    {
        private ActionBar bar;

        public ViewPageListener(ActionBar bar)
        {
            this.bar = bar;
        }

        public override void OnPageSelected(int position)
        {
            bar.SetSelectedNavigationItem(position);
        }
    }

    public static class ViewPagerExtensions
    {
        public static ActionBar.Tab GetTab(this ViewPager viewPager, ActionBar bar, string name)
        {
            var tab = bar.NewTab();
            tab.SetText(name);
            tab.TabSelected += (o, e) =>
            {
                viewPager.SetCurrentItem(bar.SelectedNavigationIndex, false);
            };
            return tab;
        }
    }
}