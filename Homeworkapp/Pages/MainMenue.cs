namespace Homeworkapp;

public class MainMenue : ContentPage
{
	public MainMenue(GetHomeworks get)
	{
		VerticalStackLayout views = new VerticalStackLayout() { Margin = 10, };

		Button add = new Button() { Text = "(Add)", FontSize = 30, HorizontalOptions = LayoutOptions.Fill, VerticalOptions = LayoutOptions.Fill, BackgroundColor = Colors.LightGray, TextColor = Colors.Black };
		add.Clicked += async (sender, args) =>
		{
			await Navigation.PushModalAsync(new AddHomework(get));
		};
		Button list = new Button() { Text = "List", FontSize = 30, HorizontalOptions = LayoutOptions.Fill, VerticalOptions = LayoutOptions.Fill, BackgroundColor = Colors.LightGray, TextColor = Colors.Black };
		list.Clicked += async (sender, args) =>
		{
			await Navigation.PushModalAsync(new HomeworkOverviewPage(get));
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

		views.Add(addF);
		views.Add(listF);

		Content = views;

	}
}