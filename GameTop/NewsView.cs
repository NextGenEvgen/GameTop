using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace GameTop
{
    public class NewsView : View
    {
        private LinearLayout layout;
        public new LinearLayout Layout { get => layout; }
        private TextView header;
        public TextView Header { get => header; }
        private TextView content;
        public TextView Content { get => content; }
        public NewsView(Context context, IAttributeSet attrs, string headerText, string contentText) :
            base(context, attrs)
        {
            Initialize();            
            layout = new LinearLayout(context);
            layout.Orientation = Orientation.Vertical;
            layout.SetBackgroundColor(Color.White);
            
            header = new TextView(context);
            header.TextAlignment = TextAlignment.Center;
            header.TextSize = 22f;
            header.Text = headerText;
            LinearLayout.LayoutParams layoutParams = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent);
            layoutParams.SetMargins(10, 0, 0, 10);
            
            header.LayoutParameters = layoutParams;
            content = new TextView(context);
            content.Gravity = GravityFlags.Center;
            content.TextSize = 15f;
            content.Text = contentText;
            content.LayoutParameters = new ViewGroup.LayoutParams(LinearLayout.LayoutParams.FillParent, LinearLayout.LayoutParams.WrapContent);
            layout.LayoutParameters = new ViewGroup.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent);
            layout.AddView(header);
            layout.AddView(content);            

        }

        public NewsView(Context context, IAttributeSet attrs, int defStyle) :
            base(context, attrs, defStyle)
        {
            Initialize();
        }
        

        private void Initialize()
        {
        }
    }
}