namespace Homeworkapp;

public class UpdateApp : ContentPage
{
	public UpdateApp(string outdated)
	{
        VerticalStackLayout views = new VerticalStackLayout() { Margin = 10, };
        bool buck = false;
        string outdatedStatus = "";
        switch (outdated)
        {
            case "version":
                {
                    outdatedStatus = "to make your app compatible with our servers";
                    break;
                }
            case "feature":
                {
                    outdatedStatus = "to use the latest features";
                    break;
                }
            case "buck":
                {
                    outdatedStatus = "to acces the latest buck fixes";
                    buck= true;
                    break;
                }
        }


        Label titel = new Label()
        {
            Text = "Your App is outdated!",
            TextColor = Colors.Red,
            FontSize = 40,
            LineBreakMode = LineBreakMode.WordWrap
        };
        Label instruction = new Label()
        {
            Text = "Please go to our website and download the newest App version " + outdatedStatus,
            FontSize = 18,
            TextType = TextType.Html,
            Margin = 10
        };
        Button website = new Button() { Text = "Go to the webside",FontSize =  18,Margin = 10 };
        website.Clicked += async (sender, args) =>
        {
            await Browser.Default.OpenAsync("http://homeworkmpg.ddns.net:12345/download", BrowserLaunchMode.External);
        };

        
        
       
        views.Add(titel);
        views.Add(instruction);
        views.Add(website);
        if (buck)
        {
            Button returnButton = new Button() { Text = "Return (not recommended)",Margin=10 };
            returnButton.Clicked += async (sender, args) =>
            {
                await Navigation.PushModalAsync(new MainMenue());
            };
            views.Add(returnButton);
        }

        Content = views;
    }
    
}


