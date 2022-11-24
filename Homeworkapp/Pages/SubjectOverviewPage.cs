namespace Homeworkapp;

public class SubjectOverviewPage : ContentPage
{
    public SubjectOverviewPage(string s)
    {
        VerticalStackLayout views = new VerticalStackLayout() { Margin = 30 };
        //Back button and Title
        {
            Button button = new Button { Text = "Back", FontSize = 18, HorizontalOptions = LayoutOptions.Center, BackgroundColor = Colors.LightGray, TextColor = Colors.Black };
            button.Clicked += async (sender, args) =>
            {
                await Navigation.PushModalAsync(new HomeworkOverviewPage());
            };
            Label label = new Label() { TextColor = Colors.Black, Text = s, FontSize = 40, VerticalOptions = LayoutOptions.Center };
            Label non = new Label() { Text = "" };

            Grid grid = new Grid
            {
                ColumnDefinitions =
            {
                new ColumnDefinition{Width = new GridLength(30, GridUnitType.Star) },
                new ColumnDefinition{Width = new GridLength(10, GridUnitType.Star) },
                new ColumnDefinition{Width = new GridLength(60, GridUnitType.Star) } },
                RowDefinitions =
                {
                    new RowDefinition{Height=new GridLength(50, GridUnitType.Star) },
                    new RowDefinition{Height=new GridLength(50, GridUnitType.Star) }
                }
            };
            Frame frame = new Frame
            {
                BorderColor = Colors.Black,
                Padding = new Thickness(5),
                BackgroundColor = Colors.LightGray
            };

            frame.Content = button;
            grid.Add(frame, 0, 0);
            grid.Add(label, 2, 0);
            grid.Add(non, 0, 1);
            views.Add(grid);
        }
        //Homeworks
        {
            GetHomeworks getHomeworks = new GetHomeworks();

            foreach (HomeworkItems homeworkItems in getHomeworks.GethomeworkItems)
            {
                if (homeworkItems.Subject == s)
                {
                    Frame frame = new Frame
                    {
                        BorderColor = Colors.Black,
                        Padding = new Thickness(5),
                        BackgroundColor = Colors.LightGray
                    };
                    Button button = new Button { Text = homeworkItems.Name, FontSize = 40, HorizontalOptions = LayoutOptions.Fill, VerticalOptions = LayoutOptions.Fill, BackgroundColor = Colors.LightGray, TextColor = Colors.Black };
                    button.Clicked += async (sender, args) =>
                    {
                        await Navigation.PushModalAsync(new HomeworkPage(homeworkItems));
                    };
                    Label non = new Label() { Text = "" };
                    frame.Content = button;
                    views.Add(frame);
                    views.Add(non);
                }
            }
        }


        Content = views;
    }
}