namespace Homeworkapp;
using Newtonsoft.Json;

public class AllHomewroksPage : ContentPage
{

    PeriodicTimer Ptimer;
    List<HomeworkItems> items = new List<HomeworkItems>();
    public AllHomewroksPage()
    {
        update();
        timer();
    }


    async void timer()
    {
        Ptimer = new PeriodicTimer(TimeSpan.FromSeconds(5));

        while (await Ptimer.WaitForNextTickAsync())
        {
            await getList();
            Console.WriteLine("update");
            update();
        }
    }

    async void update()
    {



        VerticalStackLayout vs = new VerticalStackLayout() { Margin = 30 };

        //Back, TestAdd
        {
            Button button = new Button { Text = "Back", FontSize = 18,HorizontalOptions= LayoutOptions.Center, BackgroundColor = Colors.LightGray, TextColor = Colors.Black };
            button.Clicked += async (sender, args) =>
            {
                await Navigation.PushModalAsync(new MainMenue());
                Ptimer.Dispose();
            };

            Label label = new Label { Text = "Hausaufgaben", HorizontalOptions = LayoutOptions.Center, FontSize = 30 };


            Grid grid = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition{Width = new GridLength(30, GridUnitType.Star) },
                    new ColumnDefinition{Width = new GridLength(10, GridUnitType.Star) },
                    new ColumnDefinition{Width = new GridLength(70, GridUnitType.Star) }
                },
                RowDefinitions =
                {
                    new RowDefinition{Height=new GridLength(1, GridUnitType.Star) }
                }
            };
            Frame frame = new Frame
            {
                BorderColor = Colors.Black,
                Padding = new Thickness(5),
                BackgroundColor = Colors.LightGray,
                Content = button
            };



            grid.Add(frame, 0, 0);
            grid.Add(label, 2, 0);
            vs.Add(grid);

        }


        await getList();

        //Homework
        try
        {
            {

                List<string> subjects = new List<string>();
                ScrollView scrollView = new ScrollView();



                foreach (HomeworkItems homework in items)
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
                        Ptimer.Dispose();
                        await Navigation.PushModalAsync(new OneSubjectPage(subject));
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
        catch (System.NullReferenceException)
        {

        }



    }
    async Task getList()
    {
        try
        {
            Networking networking = new Networking();
            //await parsing("{\"has\":[[\"Biologie\",\"GG\",\"28-11-2022 18:43:33\",116],[\"Deutsch\",\"GG\",\"28-11-2022 18:43:58\",117]]}");
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
            Console.WriteLine(homeworkItem.ID);
            output.Add(homeworkItem);
        }
        items= output;
        

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
}
