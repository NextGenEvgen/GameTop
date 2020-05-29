using Android.Content;
using Android.Graphics;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace GameTop
{
    public class GameScore
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public GameScore() { }
    }
    public class UserGame : View
    {
        private LinearLayout layout;
        public new LinearLayout Layout { get => layout; }
        private TextView gameName;
        public TextView GameName { get => gameName; }
        private TextView gameScore;
        public TextView GameScore { get => gameScore; }
        
        public UserGame(Context context, IAttributeSet attrs, string gameNameText, int gameRating) :
            base(context, attrs)
        {
            Initialize();
            layout = new LinearLayout(context);
            layout.Orientation = Orientation.Horizontal;
            layout.SetBackgroundColor(Color.White);

            gameName = new TextView(context);
            gameName.TextSize = 22f;
            gameName.Text = gameNameText;

            gameScore = new TextView(context);
            gameScore.TextSize = 22f;
            gameScore.Text = "------->" + gameRating.ToString();
            LinearLayout.LayoutParams layoutParams = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.WrapContent);
            layoutParams.SetMargins(10, 0, 0, 10);

            gameName.LayoutParameters = layoutParams;
            gameScore.LayoutParameters = layoutParams;
            layout.AddView(gameName);
            layout.AddView(gameScore);

        }

        public UserGame(Context context, IAttributeSet attrs, int defStyle) :
            base(context, attrs, defStyle)
        {
            Initialize();
        }


        private void Initialize()
        {
        }
    }
}