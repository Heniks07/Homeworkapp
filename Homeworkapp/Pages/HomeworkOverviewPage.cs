namespace Homeworkapp;

public class HomeworkOverviewPage : ContentPage
{
    public HomeworkOverviewPage()
    {
        /*
		Content = new VerticalStackLayout
		{
			Children = {
				new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "Welcome to .NET MAUI!"
				}
			}
		};
		*/
        GetHomeworks getHomeworks = new GetHomeworks();
        List<string> subjects = new List<string>();
        ScrollView scrollView = new ScrollView();

        VerticalStackLayout vs = new VerticalStackLayout() { Margin = 30 };

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
                await Navigation.PushModalAsync(new SubjectOverviewPage(subject));
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