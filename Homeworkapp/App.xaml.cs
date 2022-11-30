using Homeworkapp.Pages;

namespace Homeworkapp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        if (Preferences.Get("token", "") == "")
        {
            MainPage = new Login();
        }
        else
        {
            MainPage = new MainMenue();
        }
    }
}
