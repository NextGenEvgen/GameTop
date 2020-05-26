using Android.OS;
using Android.Views;
using System;

namespace GameTop
{
    public class PageFragment : Android.Support.V4.App.Fragment
    {
        private Func<LayoutInflater, ViewGroup, Bundle, View> view;
        public PageFragment(Func<LayoutInflater, ViewGroup, Bundle, View> view)
        {
            this.view = view;
        }

        public PageFragment() { }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            return view(inflater, container, savedInstanceState);
        }
    }
}