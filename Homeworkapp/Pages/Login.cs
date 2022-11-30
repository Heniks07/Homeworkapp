namespace Homeworkapp.Pages;

public class Login : ContentPage
{
	Entry name;
    Entry password;
	string token;

    public Login()
	{
        VerticalStackLayout vsl = new VerticalStackLayout();
       

			

			name = new Entry() { Placeholder = "Username", HorizontalOptions = LayoutOptions.Center, FontSize = 15, WidthRequest = 300, HeightRequest = 50, MaxLength = 20, TextColor = Colors.Black };
			password = new Entry() { Placeholder = "Password", HorizontalOptions = LayoutOptions.Center, FontSize = 15, WidthRequest = 300, HeightRequest = 50, MaxLength = 20, TextColor = Colors.Black, IsPassword = true };

			Button login = new Button() { Text = "Login", HorizontalOptions = LayoutOptions.Center, FontSize = 25, WidthRequest = 300, HeightRequest = 50, TextColor = Colors.Black };
			login.Clicked += async (sender, args) => await OnLogin();

			Frame nameF = new Frame()
			{
				BorderColor = Colors.LightGray,
				Padding = new Thickness(5),
				Content = name,
				Margin = 3
			};
			Frame passwordF = new Frame()
			{
				BorderColor = Colors.LightGray,
				Padding = new Thickness(5),
				Content = password,
				Margin = 3
			};
			Frame loginF = new Frame()
			{
				BorderColor = Colors.LightGray,
				Padding = new Thickness(5),
				Content = login,
				Margin = 3
			};

			vsl.Add(nameF);
			vsl.Add(passwordF);
			vsl.Add(loginF);


			Content = vsl;
		
	}

	async Task login()
	{
        await Navigation.PushModalAsync(new MainMenue());
    }

	async Task OnLogin()
	{
		Networking networking = new Networking();
		await networking.getToken(name.Text,password.Text);

		token = Preferences.Get("token", "");
		Console.WriteLine(token);
		if(token != "")
		{
            await Navigation.PushModalAsync(new MainMenue());
        }
	}
}