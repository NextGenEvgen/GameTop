using System.Net;
using Android.Content;
using Android.Graphics;
using Android.Widget;

namespace GameTop
{
    static class Helper
    {
        public const string serverIP = "192.168.1.137";
        public static Bitmap GetImageFromUrl(string url)
        {
            Bitmap image = null;
            using (var webClient = new WebClient())
            {
                
                var bytes = webClient.DownloadData(url);
                if (bytes != null && bytes.Length > 0)
                {
                    image = BitmapFactory.DecodeByteArray(bytes, 0, bytes.Length);
                }
                webClient.Dispose();
            }
            return image;
        }

        public static ImageButton GetImageButton(string imageName, Context context)
        {
            ImageButton imageButton = new ImageButton(context);
            imageButton.SetImageBitmap(GetImageFromUrl($"http://{serverIP}:25525/getimage?name={imageName}.png"));
            imageButton.LayoutParameters = GetParams();
            return imageButton;
        }

        public static GridLayout.LayoutParams GetParams()
        {
            GridLayout.LayoutParams param = new GridLayout.LayoutParams();
            param.RowSpec = GridLayout.InvokeSpec(GridLayout.Undefined, 1f);
            param.ColumnSpec = GridLayout.InvokeSpec(GridLayout.Undefined, 1f);
            return param;
        }
    }
}