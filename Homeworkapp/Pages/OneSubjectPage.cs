using Newtonsoft.Json;

namespace Homeworkapp;

public class OneSubjectPage : ContentPage
{
    PeriodicTimer Ptimer;
    List<HomeworkItems> items = new List<HomeworkItems>();
    public OneSubjectPage(string s)
    {
        getList();
        update(s);
        timer(s);
    }

    async void timer(string s)
    {
        Ptimer = new PeriodicTimer(TimeSpan.FromSeconds(2));
        while (await Ptimer.WaitForNextTickAsync())
        {
            await update(s);
        }



    }

    async Task update(string s)
    {
        VerticalStackLayout views = new VerticalStackLayout() { Margin = 30 };
        //Back button and Title
        {
            Button button = new Button { Text = "Back", FontSize = 18, HorizontalOptions = LayoutOptions.Center, BackgroundColor = Colors.LightGray, TextColor = Colors.Black };
            button.Clicked += async (sender, args) =>
            {
                await Navigation.PushModalAsync(new AllHomewroksPage());
                Ptimer.Dispose();
            };
            Label label = new Label() { Text = s, FontSize = 40, VerticalOptions = LayoutOptions.Center };
            Label non = new Label() { Text = "" };

            Grid grid = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition{Width = new GridLength(30, GridUnitType.Star) },
                    new ColumnDefinition{Width = new GridLength(10, GridUnitType.Star) },
                    new ColumnDefinition{Width = new GridLength(60, GridUnitType.Star) }
                },
                RowDefinitions =
                {
                    new RowDefinition{Height=new GridLength(50, GridUnitType.Star) },
                    new RowDefinition{Height=new GridLength(50, GridUnitType.Star) }
                }
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
            grid.Add(non, 0, 1);
            views.Add(grid);
        }
        await getList();
        //Homeworks
        {
            

            foreach (HomeworkItems homeworkItems in items)
            {
                if (homeworkItems.Subject == s)
                {
                    Frame frame = new Frame
                    {
                        BorderColor = Colors.Black,
                        Padding = new Thickness(5),
                        BackgroundColor = Colors.LightGray
                    };
                    Button button = new Button { Text = homeworkItems.Name, FontSize = 40, HorizontalOptions = LayoutOptions.Fill, VerticalOptions = LayoutOptions.Fill, BackgroundColor = Colors.LightGray, TextColor = Colors.Black };
                    button.Clicked += async (sender, args) =>
                    {
                        Ptimer.Dispose();
                        await Navigation.PushModalAsync(new HomeworkPage(homeworkItems));
                    };
                    Label non = new Label() { Text = "" };
                    frame.Content = button;
                    views.Add(frame);
                    views.Add(non);
                }
            }
        }


        Content = views;
    }
    async Task getList()
    {
        try
        {
            Networking networking = new Networking();
            //parsing("{\"has\":[[\"Biologie\",\"GG\",\"28-11-2022 18:43:33\",116],[\"Deutsch\",\"GG\",\"28-11-2022 18:43:58\",117]]}");
            parsing(await networking.GetHomeworks());

        }
        catch (System.IO.IOException)
        {
            System.Console.WriteLine("\n\nError\n\n");
        }
    }

    void parsing(string Json)
    {
        var quote = Quote.FromJson(Json);
        List<HomeworkItems> output = new List<HomeworkItems>();
        foreach (List<string> list in quote.has)
        {

            HomeworkItems homeworkItem = new HomeworkItems()
            {
                Name = list[1],
                Subject = list[0],
                date = list[2],
                ID = int.Parse(list[3])
            };
            output.Add(homeworkItem);


            Console.WriteLine();
        }
        items= output;

    }

}
public partial class Quote
{

    [JsonProperty("has")]
    public List<List<string>> has { get; set; }
}

public partial class Quote
{
    public static Quote FromJson(string json) =>
        JsonConvert.DeserializeObject<Quote>(json, Converter.Settings);
}

public class Converter
{
    public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
    {
        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
        DateParseHandling = DateParseHandling.None,
    };
}