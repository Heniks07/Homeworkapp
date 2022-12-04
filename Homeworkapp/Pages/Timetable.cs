using Newtonsoft.Json;

namespace Homeworkapp;

public class Timetable : ContentPage
{
    PeriodicTimer Ptimer;
    List<List<string>> timetable;
    readonly List<string> workdays = new List<string>() { "Montag","Dienstag", "Mittwoch","Donnerstag","Freitag"};

    async void timer()
    {
        await createContent();
        Ptimer = new PeriodicTimer(TimeSpan.FromSeconds(60));
        while (await Ptimer.WaitForNextTickAsync())
        {
            await createContent();
        }



    }

    public Timetable()
    { 
        timer();
    }

    async Task createContent()
    { 
    
        await getTimetable();
        VerticalStackLayout views= new VerticalStackLayout { Margin=0 };

        //back
        {
            Button back = new Button { Text = "Back", FontSize = 18, HorizontalOptions = LayoutOptions.Center, BackgroundColor = Colors.LightGray, TextColor = Colors.Black };
            back.Clicked += async (sender, args) =>
            {
                Ptimer.Dispose();
                await Navigation.PushModalAsync(new MainMenue());   
            };
            Frame backF = new Frame
            {
                Margin = 5,
                Content = back,
                BorderColor = Colors.Black,
                Padding = new Thickness(5),
                BackgroundColor = Colors.LightGray,
                HorizontalOptions = LayoutOptions.Start
            };
            views.Add(backF);
        }
        //timetable
        {
            Grid gr = new Grid()
            {
                ColumnDefinitions =
            {
                new ColumnDefinition(new GridLength(1,GridUnitType.Star)),
                new ColumnDefinition(new GridLength(1,GridUnitType.Star)),
                new ColumnDefinition(new GridLength(1,GridUnitType.Star)),
                new ColumnDefinition(new GridLength(1,GridUnitType.Star)),
                new ColumnDefinition(new GridLength(1,GridUnitType.Star))
            },
                RowDefinitions =
            {
                new RowDefinition(new GridLength(1,GridUnitType.Star)),
                new RowDefinition(new GridLength(1, GridUnitType.Star)),
                new RowDefinition(new GridLength(1, GridUnitType.Star)),
                new RowDefinition(new GridLength(1, GridUnitType.Star)),
                new RowDefinition(new GridLength(1, GridUnitType.Star)),
                new RowDefinition(new GridLength(1, GridUnitType.Star)),
                new RowDefinition(new GridLength(1, GridUnitType.Star)),
                new RowDefinition(new GridLength(1, GridUnitType.Star)),
                new RowDefinition(new GridLength(1, GridUnitType.Star)),
                new RowDefinition(new GridLength(1, GridUnitType.Star)),
            }
            };
            //day
            for (int day = 0; day < 5; day++)
            {
                Label label = new Label { Text = workdays[day], TextColor = Colors.Black };
                Frame frame = new Frame()
                {
                    BorderColor = Colors.Black,
                    Padding = new Thickness(5),
                    Margin = 0,
                    CornerRadius = 0,
                    Content = label,
                    BackgroundColor = Colors.LightGray,
                    HorizontalOptions = LayoutOptions.Fill,
                    VerticalOptions = LayoutOptions.Fill

                };
                gr.Add(frame, day, 0);
            }

            //lessons
            for (int i = 0; i < timetable.Count; i++)
            {
                List<string> day = timetable[i];
                for (int j = 1; j < day.Count; j++)
                {
                    string lesson = day[j];

                    Label label = new Label { Text = lesson, TextColor = Colors.Black };
                    Frame frame = new Frame()
                    {
                        BorderColor = Colors.Black,
                        Padding = new Thickness(5),
                        Margin = 0,
                        CornerRadius = 0,
                        Content = label,
                        BackgroundColor = Colors.LightGray,
                        HorizontalOptions = LayoutOptions.Fill,
                        VerticalOptions = LayoutOptions.Fill

                    };
                    gr.Add(frame, i, j);
                }
            }

            views.Add(gr);
            Content = views;
        }
    }

    async Task getTimetable()
    {
        try
        {
            Networking networking = new Networking();
            //await parsing("{\"has\":[[\"Biologie\",\"GG\",\"28-11-2022 18:43:33\",116],[\"Deutsch\",\"GG\",\"28-11-2022 18:43:58\",117]]}");
            parsing(await networking.GetTimetable());

        }
        catch (System.IO.IOException)
        {
            await createContent();
            System.Console.WriteLine("\n\nError\n\n");
        }
    }
    void parsing(string Json)
    {
        var quote = Quote.FromJson(Json);

        timetable = quote.timetable;

    }

    public partial class Quote
    {

        [JsonProperty("timetable")]
        public List<List<string>> timetable { get; set; }
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
}