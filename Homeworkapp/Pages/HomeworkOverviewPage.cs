namespace Homeworkapp;

public class HomeworkOverviewPage : ContentPage
{
    PeriodicTimer Ptimer;
    GetHomeworks getHomeworks = new GetHomeworks();
    public HomeworkOverviewPage(GetHomeworks get)
    {
        getHomeworks = get;
        update();
        timer();
    }


    async void timer()
    {
        Ptimer = new PeriodicTimer(TimeSpan.FromSeconds(2));

        while (await Ptimer.WaitForNextTickAsync())
        {
            update();
        }
    }

    void update()
    {
        VerticalStackLayout vs = new VerticalStackLayout() { Margin = 30 };

        //Back, TestAdd
        {
            Button button = new Button { Text = "Back", FontSize = 18, HorizontalOptions = LayoutOptions.Fill, VerticalOptions = LayoutOptions.Fill, BackgroundColor = Colors.LightGray, TextColor = Colors.Black };
            button.Clicked += async (sender, args) =>
            {
                await Navigation.PushModalAsync(new MainMenue(getHomeworks));
                Ptimer.Dispose();
            };

            Label label = new Label { Text = "Hausaufgaben", VerticalOptions = LayoutOptions.Center, FontSize = 30 };


            Label non = new Label() { Text = "" };

            Grid grid = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition{Width = new GridLength(30, GridUnitType.Star) },
                    new ColumnDefinition{Width = new GridLength(10, GridUnitType.Star) },
                    new ColumnDefinition{Width = new GridLength(70, GridUnitType.Star) }
                },
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
                BackgroundColor = Colors.LightGray,
                Content = button
            };



            grid.Add(frame, 0, 0);
            grid.Add(label, 2, 0);
            grid.Add(non, 0, 1);
            vs.Add(grid);

        }



        //Homework
        {
            List<string> subjects = new List<string>();
            ScrollView scrollView = new ScrollView();



            foreach (HomeworkItems homework in getHomeworks.GethomeworkItems)
            {
                if (!subjects.Contains(homework.Subject))
                {
                    subjects.Add(homework.Subject);
                }
            }

            foreach (string subject in subjects)
            {
                Frame frame = new Frame
                {
                    BorderColor = Colors.Black,
                    Padding = new Thickness(5),
                    BackgroundColor = Colors.LightGray
                };
                Button button = new Button { Text = subject, FontSize = 40, HorizontalOptions = LayoutOptions.Fill, VerticalOptions = LayoutOptions.Fill, BackgroundColor = Colors.LightGray, TextColor = Colors.Black };
                button.Clicked += async (sender, args) =>
                {
                    Ptimer.Dispose();
                    await Navigation.PushModalAsync(new SubjectOverviewPage(subject, getHomeworks));
                };
                frame.Content = button;
                Label non = new Label() { Text = "" };
                vs.Add(frame);
                vs.Add(non);
            }

            scrollView.Content = vs;
            Content = scrollView;

        }

    }


}