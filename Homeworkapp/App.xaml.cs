namespace Homeworkapp;

public partial class App : Application
{
    readonly List<int> Appversion = new List<int> { 1, 0, 1 };
    public App()
    {
        InitializeComponent();
        string checkedVersion = checkVersion();
        if (checkedVersion == "new")
        {

            if (Preferences.Get("token", "") == "")
            {
                MainPage = new Login();
            }
            else
            {
                MainPage = new MainMenue();
            }
        }
        else
        {
            MainPage = new UpdateApp(checkedVersion);
        }
    }

    string checkVersion()
    {
        Networking networking = new Networking();
        networking.GetVersion();
        string version = Preferences.Default.Get("version", "");
        string[] single = version.Split(".");
        List<int> newestVersion = new List<int>();
        foreach (string s in single)
        {
            int result = int.Parse(s);
            newestVersion.Add(result);
        }
        if (newestVersion[0] != Appversion[0])
        {
            return "version";
        }
        else if (newestVersion[1] != Appversion[1])
        {
            return "feature";
        }
        else if (newestVersion[2] != Appversion[2])
        {
            return "bug";
        }
        return "new";
    }
}
