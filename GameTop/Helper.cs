#define HOME
using System.Net;
using Android.Content;
using Android.Graphics;
using Android.Widget;

namespace GameTop
{
    static class Helper
    {
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
            }
            return image;
        }

        public static ImageButton GetImageButton(string imageName, Context context)
        {
            ImageButton imageButton = new ImageButton(context);
#if HOME
            imageButton.SetImageBitmap(GetImageFromUrl($"http://192.168.1.137:25525/{imageName}.png"));
#else
            imageButton.SetImageBitmap(GetImageFromUrl($"http://77.43.249.46:22525/{imageName}.png"));
#endif
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