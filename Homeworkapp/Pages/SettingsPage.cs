namespace Homeworkapp;

public class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		

        VerticalStackLayout views = new VerticalStackLayout();

        Button back = new Button() { Text="back", Margin = 5,HorizontalOptions= LayoutOptions.Start,FontSize=18};
        back.Clicked += async (sender, args) =>
        {
            await Navigation.PushModalAsync(new MainMenue());
        };

		Button logout= new Button() { Text="logout", Margin= 10,HorizontalOptions=LayoutOptions.Start,FontSize=25};
        logout.Clicked += async (sender, args) =>
        {
            Preferences.Default.Set("token", "");
            await Navigation.PushModalAsync(new Login());
        };



        views.Add(back);
        views.Add(logout);
        Content = views;

        

    }
}