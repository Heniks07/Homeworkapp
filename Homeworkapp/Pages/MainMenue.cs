
namespace Homeworkapp;

public class MainMenue : ContentPage
{

    public MainMenue()
    {

        VerticalStackLayout views = new VerticalStackLayout() { Margin = 10, };

        Button settings = new Button() { Text="Settings",HorizontalOptions=LayoutOptions.End};
        settings.Clicked += async (sender, args) =>
        {
            await Navigation.PushModalAsync(new SettingsPage());
        };

        Button add = new Button() { Text = "Add", FontSize = 30, HorizontalOptions = LayoutOptions.Fill, VerticalOptions = LayoutOptions.Fill, BackgroundColor = Colors.LightGray, TextColor = Colors.Black };
        add.Clicked += async (sender, args) =>
        {
            await Navigation.PushModalAsync(new AddHomework());
        };
        Button list = new Button() { Text = "List", FontSize = 30, HorizontalOptions = LayoutOptions.Fill, VerticalOptions = LayoutOptions.Fill, BackgroundColor = Colors.LightGray, TextColor = Colors.Black };
        list.Clicked += async (sender, args) =>
        {
            await Navigation.PushModalAsync(new AllHomewroksPage());
        };
        Button timetable = new Button() { Text = "Timetable", FontSize = 30, HorizontalOptions = LayoutOptions.Fill, VerticalOptions = LayoutOptions.Fill, BackgroundColor = Colors.LightGray, TextColor = Colors.Black };
        timetable.Clicked += async (sender, args) =>
        {
            await Navigation.PushModalAsync(new Timetable());
        };


        Frame addF = new Frame
        {
            Content = add,
            BorderColor = Colors.Black,
            Padding = new Thickness(5),
            BackgroundColor = Colors.LightGray,
            Margin = 10,
            HorizontalOptions = LayoutOptions.Center,
            WidthRequest = 200,
            HeightRequest = 80
        };
        Frame listF = new Frame
        {
            Content = list,
            BorderColor = Colors.LightGray,
            Padding = new Thickness(5),
            BackgroundColor = Colors.LightGray,
            Margin = 10,
            HorizontalOptions = LayoutOptions.Center,
            WidthRequest = 200,
            HeightRequest = 80
        };
        Frame timetableF = new Frame
        {
            Content = timetable,
            BorderColor = Colors.LightGray,
            Padding = new Thickness(5),
            BackgroundColor = Colors.LightGray,
            Margin = 10,
            HorizontalOptions = LayoutOptions.Center,
            WidthRequest = 200,
            HeightRequest = 80
        };

        views.Add(settings);
        views.Add(addF);
        views.Add(listF);
        views.Add(timetableF);

        Content = views;

    }


}
