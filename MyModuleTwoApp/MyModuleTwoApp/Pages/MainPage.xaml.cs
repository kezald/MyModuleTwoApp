using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyModuleTwoApp
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            

            this.Title = "TabbedPage";
            

            //this.ItemsSource = new NamedColor[]
            //{
            //    new NamedColor ("Red", Color.Red),
            //    new NamedColor ("Yellow", Color.Yellow),
            //    new NamedColor ("Green", Color.Green),
            //    new NamedColor ("Aqua", Color.Aqua),
            //    new NamedColor ("Blue", Color.Blue),
            //    new NamedColor ("Purple", Color.Purple)
            //};

            //this.ItemTemplate = new DataTemplate(() =>
            //{
            //    return new NamedColorPage();
            //});



            //this.Children.Add(new ContentPage
            //{
            //    Title = "Blue",
            //    Content = new BoxView
            //    {
            //        Color = Color.Blue,
            //        HeightRequest = 100f,
            //        VerticalOptions = LayoutOptions.Center
            //    },
            //}
            //);
            //this.Children.Add(new ContentPage
            //{
            //    Title = "Blue and Red",
            //    Content = new StackLayout
            //    {
            //        Children =
            //        {
            //            new BoxView { Color = Color.Blue },
            //            new BoxView { Color = Color.Red }
            //        }
            //    },
            //}
            //);
        }

        //async private void Button_Clicked(object sender, EventArgs e)
        //{
        //    Button bt = (Button)sender;
        //    await DisplayAlert("Clicked!", "The button labeled '" + bt.Text + "' has been clicked", "OK");
        //    //await Navigation.PushModalAsync(new Grid1());
        //}

        //class NamedColor
        //{
        //    public NamedColor(string name, Color color)
        //    {
        //        this.Name = name;
        //        this.Color = color;
        //    }

        //    public string Name { private set; get; }
        //    public Color Color { private set; get; }

        //    public override string ToString()
        //    {
        //        return Name;
        //    }
        //}

        //class NamedColorPage : ContentPage
        //{
            
        //    public NamedColorPage()
        //    {
        //        this.SetBinding(ContentPage.TitleProperty, "Name");

        //        BoxView boxView = new BoxView
        //        {
        //            WidthRequest = 100,
        //            HeightRequest = 100,
        //            HorizontalOptions = LayoutOptions.Center
        //        };

        //        boxView.SetBinding(BoxView.ColorProperty, "Color");
        //        this.Content = boxView;
        //    }
        //}
    }
}
