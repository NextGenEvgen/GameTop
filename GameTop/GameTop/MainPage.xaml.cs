using System;
using System.Net.Http;
using Xamarin.Forms;

namespace GameTop
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Appearing += MainPage_Appearing;
            for (int i = 0; i < 3; i++)
            {
                ColumnDefinition column = new ColumnDefinition() { Width = App.ScreenWidth / 3 };
                Image image = new Image();
                grid.ColumnDefinitions.Add(column);
            }
            for (int i = 0; i < 10; i++)
            {
                RowDefinition row = new RowDefinition() { Height = App.ScreenWidth / 3 };
                grid.RowDefinitions.Add(row);
            }
            grid.Margin = 10;
            //NavigationPage.SetHasBackButton(this, false);

            //((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.Black;
            //((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
        }

        private async void MainPage_Appearing(object sender, EventArgs e)
        {
            //Button button;
            //Grid.SetRow(rectangle, 0);
            //HttpClient client = new HttpClient();
            //HttpRequestMessage request = new HttpRequestMessage();
            //request.RequestUri = new Uri("http://192.168.1.104:8080/Dark%20Souls.png");
            //request.Method = HttpMethod.Get;
            //request.Headers.Add("Image", "image/jpeg");

            //HttpResponseMessage response = await client.SendAsync(request);
            //string result = "";
            //System.IO.Stream stream = null;
            //if (response.StatusCode == System.Net.HttpStatusCode.OK)
            //{
            //    HttpContent content = response.Content;
            //    stream = await response.Content.ReadAsStreamAsync();
            //}
            //var data = result.Split(',');
            ImageButton imageCell = new ImageButton();
            //imageCell.Source = ImageSource.FromStream(() => { return stream; });
            imageCell.Source = ImageSource.FromUri(new Uri("http://192.168.1.104:8080/Dark%20Souls.png"));
            imageCell.Aspect = Aspect.AspectFill;
            //string url = $"http://192.168.1.104:8080/{data[0]}";

            //imageCell.Source = ImageSource.FromUri(new Uri(url));
            //imageCell.HorizontalOptions = LayoutOptions.Center;
            //imageCell.VerticalOptions = LayoutOptions.CenterAndExpand;
            //Label label = new Label();
            //label.Focused += Label_Focused;
            //label.Text = result;
            grid.Children.Add(imageCell);
            Grid.SetRow(imageCell, 0);
            Grid.SetColumn(imageCell, 0);
            //for (int i = 0; i < grid.RowDefinitions.Count; i++)
            //{
            //    for (int j = 0; j < grid.ColumnDefinitions.Count; j++)
            //    {
            //        Image img = new Image();
            //        img.Source = ImageSource.FromUri(new Uri("http://192.168.1.104:8080/peka.png"));
            //        grid.Children.Add(img);
            //        Grid.SetColumn(img, j);
            //        Grid.SetRow(img, i);
            //    }
            //}
            //}
        }

        private void Label_Focused(object sender, FocusEventArgs e)
        {
            Label label = new Label();
            label.Text = "got focus";
            grid.Children.Add(label);
            Grid.SetRow(label, 1);
            Grid.SetColumn(label, 1);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

            //label.Text += abc;

        }
    }
}
