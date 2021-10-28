using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherReportLibrary;

namespace WeatherReportSender
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly string apiAddres = "https://localhost:44384/api/WeatherReport";
        static async Task Main(string[] args)
        {
            for (int i = 0; i < 5000; i++)
            {
                WeatherReport weatherReport = new WeatherReport { Id = i, Name = "WeatherReport" + i, Temparature = 30 };
                var json = JsonConvert.SerializeObject(weatherReport);
                var responseMessage = await client.PostAsync(apiAddres, new StringContent(json, Encoding.UTF8, "application/json"));
                Console.WriteLine(i);
            }
        }
    }
}
