namespace Homeworkapp;

public class HomeworkPage : ContentPage
{
    PeriodicTimer Ptimer;
    public HomeworkPage(HomeworkItems homework)
    {
        update(homework);
        timer(homework);
    }

    async Task timer(HomeworkItems homework)
    {
        Ptimer = new PeriodicTimer(TimeSpan.FromSeconds(2));

        while (await Ptimer.WaitForNextTickAsync())
        {
            update(homework);
        }
    }

    void update(HomeworkItems homework)
    {
        



        VerticalStackLayout views = new VerticalStackLayout() { Margin = 30 };
        //back, delete
        {
            //back
            Button back = new Button { Text = "Back", FontSize = 18, HorizontalOptions = LayoutOptions.Center, BackgroundColor = Colors.LightGray, TextColor = Colors.Black };
            back.Clicked += async (sender, args) =>
            {
                Ptimer.Dispose();
                await Navigation.PushModalAsync(new OneSubjectPage(homework.Subject));
            };

            //delete
            ImageButton delete = new ImageButton { Source = "delete.png", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center, MaximumHeightRequest = 50 };
            delete.Clicked += async (sender, args) =>
            {
                await deleteHomework(homework);
                await Navigation.PushModalAsync(new OneSubjectPage(homework.Subject));
            };

           

            Grid grid = new Grid
            {
                ColumnDefinitions =
                {
                new ColumnDefinition{Width = new GridLength(30, GridUnitType.Star) },
                new ColumnDefinition{Width = new GridLength(2, GridUnitType.Star) },
                new ColumnDefinition{Width = new GridLength(50, GridUnitType.Star) },
                new ColumnDefinition{Width = new GridLength(18, GridUnitType.Star) }
                }
            };
            Frame frame = new Frame
            {
                BorderColor = Colors.Black,
                Padding = new Thickness(5),
                BackgroundColor = Colors.LightGray
            };
            Frame deleteFrame = new Frame
            {
                BorderColor = Colors.Black,
                Padding = new Thickness(5),
                BackgroundColor = Colors.DarkRed
            };

            frame.Content = back;
            deleteFrame.Content = delete;
            grid.Add(frame, 0, 0);
            grid.Add(deleteFrame, 3, 0);
            views.Add(grid);

        }

        //information
        {
            Label subjectTitle = new Label() { Text = "\nSubject:", FontSize = 30, HorizontalOptions = LayoutOptions.Start };
            Label subject = new Label() { Text = homework.Subject, FontSize = 25, HorizontalOptions = LayoutOptions.Start };
            Label nameTitle = new Label() { Text = "\nName:", FontSize = 30, HorizontalOptions= LayoutOptions.Start };
            Label name = new Label() { Text = homework.Name, FontSize = 25, VerticalOptions = LayoutOptions.Center };

            views.Add(nameTitle);
            views.Add(name);
            views.Add(subjectTitle);
            views.Add(subject);


            Content = views;
        }
    }

    async Task deleteHomework(HomeworkItems homework)
    {
        Networking networking= new Networking();

        await networking.MakeDone(homework.ID);
    }
}