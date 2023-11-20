using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;


namespace WarsawAPI
{
    class WawAPI
    {
        readonly string APIKey;

        public WawAPI(string APIKey) { this.APIKey = APIKey; }

        //Schedule functions
        public string[] GetLines(string stopID, string stopNr)
        {
            string url = $"https://api.um.warszawa.pl/api/action/dbtimetable_get?id=88cd555f-6f31-43ca-9de4-66c479ad5942&busstopId={stopID}&busstopNr={stopNr}&apikey={APIKey}";

            var task = Task.Run(async () => await APICall(url));

            ApiResponseIntermidiate intermidiate = task.Result;

            string[] lines = new string[intermidiate.result.Length];

            for(int i = 0; i < intermidiate.result.Length;i++)
            {
                lines[i] = intermidiate.result[i].values[0].value;
            }

            return lines;
        }

        public List<LineSchedule> GetSchedule(string stopID, string stopNr)
        {
            string[] lines = GetLines(stopID, stopNr);

            List<LineSchedule> schedules = new List<LineSchedule>();

            foreach(string line in lines) 
            {
                schedules.Add(GetLineSchedule(stopID, stopNr, line));
            }

            return schedules;
        }

        public LineSchedule GetLineSchedule(string stopID, string stopNr, string lineNr)
        {
            string url = $"https://api.um.warszawa.pl/api/action/dbtimetable_get?id=e923fa0e-d96c-43f9-ae6e-60518c9f3238&busstopId={stopID}&busstopNr={stopNr}&line={lineNr}&apikey={APIKey}";
            
            var task = Task.Run(async () => await APICall(url));

            
            ApiResponseIntermidiate intermidiate = task.Result;


            LineSchedule schedule = new LineSchedule(lineNr);

            foreach (Result r in intermidiate.result)
            {
                LineScheduleTime time = new LineScheduleTime();

                time.direction = r.GetValueByKey("kierunek");
                time.route = r.GetValueByKey("trasa");
                time.time = TimeSpan.Parse(r.GetValueByKey("czas"));

                /*
                time.direction = r.values[3].value;
                time.route = r.values[4].value;
                time.time = r.values[5].value;
                 */

                schedule.lineScheduleTimes.Add(time);
            }





            return schedule;
        }

        public string GetstopIDbyName(string stopName)
        {
            string url = $"https://api.um.warszawa.pl/api/action/dbtimetable_get?id=b27f4c17-5c50-4a5b-89dd-236b282bc499&name={HttpUtility.UrlEncode(stopName)}&apikey={APIKey}";

            var task = Task.Run(async () => await APICall(url));

            ApiResponseIntermidiate intermidiate = task.Result;

            return intermidiate.result[0].GetValueByKey("zespol");
        }

        async Task<ApiResponseIntermidiate> APICall(string url)
        {

            using (HttpClient client = new HttpClient())
            {
                // Send a GET request to the API
                HttpResponseMessage response = await client.GetAsync(url);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                        // Read the JSON response as a string
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        ApiResponseIntermidiate intermidiateResults = JsonConvert.DeserializeObject<ApiResponseIntermidiate>(jsonResponse);

                        return intermidiateResults;

                }
                else
                {
                    Console.WriteLine($"API request failed with status code: {response.StatusCode}");
                    ApiResponseIntermidiate intermidiateResults = new ApiResponseIntermidiate();
                    return intermidiateResults;
                }
            }
         
        }
    }
}
