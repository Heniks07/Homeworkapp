using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Homeworkapp
{
    public class Networking
    {
        HttpClient httpClient;
        JsonSerializerOptions Jsonoptions;
        List<HomeworkItems> items;

        public Networking()
        {
            httpClient = new HttpClient();
            Jsonoptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        //Post
        public async Task getToken(string nameString, string passwordString)
        {
            Token token = new Token();

            Uri uri = new Uri("http://homeworkmpg.ddns.net:12345/api/gen_token");
            try
            {
                string json = JsonSerializer.Serialize(new LoginItem() { name = nameString, pswd = passwordString });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await httpClient.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    HttpContent httpContent = response.Content;
                    string jsonContent = httpContent.ReadAsStringAsync().Result;
                    token = JsonSerializer.Deserialize<Token>(jsonContent);

                    Preferences.Default.Set("token", token.token);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
        public async Task makeDone(string token, int id)
        {
            //2f514b450d03d6224b0f54b7bcf9945d91f13cdaf1bc6ad41814a6c48b473f79b8555a5c9e6079de1ffb809064a0f5040205a29e9997fd85be5efd3fc0d9564d
            string responseString;
            string json = "{\"token\":\"2f514b450d03d6224b0f54b7bcf9945d91f13cdaf1bc6ad41814a6c48b473f79b8555a5c9e6079de1ffb809064a0f5040205a29e9997fd85be5efd3fc0d9564d\",\"id\":\""+id+"\"}";
            Console.WriteLine(json);

            Console.WriteLine(json);


            Uri uri = new Uri("http://homeworkmpg.ddns.net:12345/api/done");
            try
            {
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await httpClient.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    HttpContent httpContent = response.Content;
                    string jsonContent = httpContent.ReadAsStringAsync().Result;
                    Console.WriteLine(jsonContent);
                    Console.WriteLine("done");
                }
                else
                {
                    Console.WriteLine("error");
                    Console.WriteLine(response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

        //Get
        public async Task<string> getHomeworks(string token)
        {
            var url = "http://homeworkmpg.ddns.net:12345/api/all_ha/2f514b450d03d6224b0f54b7bcf9945d91f13cdaf1bc6ad41814a6c48b473f79b8555a5c9e6079de1ffb809064a0f5040205a29e9997fd85be5efd3fc0d9564d";

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);


            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return result;
            }

            Console.WriteLine(httpResponse.StatusCode);

        }



    }

    class LoginItem
    {
        public string name { get; set; }
        public string pswd { get; set; }
    }
    class Token
    {
        public string token { get; set; }
    }
}
