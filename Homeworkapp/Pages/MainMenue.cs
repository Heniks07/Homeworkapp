namespace Homeworkapp;

public class MainMenue : ContentPage
{
	public MainMenue()
	{
		Grid grid = new Grid()
		{
			RowDefinitions =
			{
				new RowDefinition{Height=new GridLength(25,GridUnitType.Star)},
				new RowDefinition{Height=new GridLength(10,GridUnitType.Star)},
                new RowDefinition{Height=new GridLength(5,GridUnitType.Star)},
                new RowDefinition{Height=new GridLength(10,GridUnitType.Star)},
				new RowDefinition{Height=new GridLength(55,GridUnitType.Star)}
			},
			ColumnDefinitions =
			{
				new ColumnDefinition{Width=new GridLength(10,GridUnitType.Star)},
				new ColumnDefinition{Width=new GridLength(80,GridUnitType.Star)},
				new ColumnDefinition{Width=new GridLength(10,GridUnitType.Star)}
			}

		};

		Button add = new Button() { Text = "(Add)", FontSize = 30, HorizontalOptions = LayoutOptions.Fill, VerticalOptions = LayoutOptions.Fill, BackgroundColor = Colors.LightGray, TextColor = Colors.Black };

		Button list = new Button() { Text = "List", FontSize = 30, HorizontalOptions = LayoutOptions.Fill, VerticalOptions = LayoutOptions.Fill, BackgroundColor = Colors.LightGray, TextColor = Colors.Black };
		list.Clicked += async (sender, args) =>
		{
			await Navigation.PushModalAsync(new HomeworkOverviewPage(new GetHomeworks()));
		};

		Frame addF = new Frame
		{
			Content = add,
			BorderColor = Colors.Black,
			Padding = new Thickness(5),
			BackgroundColor = Colors.LightGray
		};
		Frame listF = new Frame
		{
			Content = list,
			BorderColor = Colors.LightGray,
			Padding = new Thickness(5),
			BackgroundColor = Colors.LightGray
		};

		grid.Add(addF, 1, 1);
		grid.Add(listF, 1, 3);

		Content = grid;

	}
}