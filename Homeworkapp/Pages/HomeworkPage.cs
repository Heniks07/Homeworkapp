namespace Homeworkapp;

public class HomeworkPage : ContentPage
{
    public HomeworkPage(HomeworkItems homework)
    {
        VerticalStackLayout views = new VerticalStackLayout() { Margin = 30 };
        //back and title
        {
            Button button = new Button { Text = "Back", FontSize = 18, HorizontalOptions = LayoutOptions.Center, BackgroundColor = Colors.LightGray, TextColor = Colors.Black };
            button.Clicked += async (sender, args) =>
            {
                await Navigation.PushModalAsync(new SubjectOverviewPage(homework.Subject));
            };
            Label label = new Label() { TextColor = Colors.Black, Text = homework.Name, FontSize = 40, VerticalOptions = LayoutOptions.Center };

            Grid grid = new Grid
            {
                ColumnDefinitions =
            {
                new ColumnDefinition{Width = new GridLength(30, GridUnitType.Star) },
                new ColumnDefinition{Width = new GridLength(10, GridUnitType.Star) },
                new ColumnDefinition{Width = new GridLength(60, GridUnitType.Star) } }
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
            views.Add(grid);
        }

        //information
        {
            Label subjectTitle = new Label() { Text = "\nSubject:", FontSize = 30, HorizontalOptions = LayoutOptions.Center };
            Label subject = new Label() { Text = homework.Subject, FontSize = 25, HorizontalOptions = LayoutOptions.Center };
            Label descriptionTitle = new Label() { Text = "\nDescription:", FontSize = 30, HorizontalOptions = LayoutOptions.Center };
            Label description = new Label() { Text = homework.Description, FontSize = 25, HorizontalOptions = LayoutOptions.Center };

            views.Add(subjectTitle);
            views.Add(subject);
            views.Add(descriptionTitle);
            views.Add(description);
        }
        Content = views;
    }
}