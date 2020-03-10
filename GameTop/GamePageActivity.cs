﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace GameTop
{
    [Activity(Label = "GamePageActivity")]
    public class GamePageActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.game_activity);
            
            string name = Intent.GetStringExtra("name") ?? "send help";
            var textView = FindViewById<TextView>(Resource.Id.gameName);
            
            textView.Text = name;
        }
    }
}