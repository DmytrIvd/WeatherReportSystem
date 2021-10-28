using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Middleware;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WeatherReportLibrary;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeatherReportReceiver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherReportController : ControllerBase
    {
        private readonly ILogger _logger;

        private readonly IWeatherReportable weatherReports;

        public WeatherReportController(ILogger<WeatherReportController> logger, IWeatherReportable reportable)
        {
            _logger = logger;
            weatherReports = reportable;
        }

        [HttpPost]
        public bool Post([FromBody] WeatherReport weatherReport)
        {
            lock (weatherReports)
            {
                if (weatherReports.ManualResetEvent.WaitOne())
                {
                    weatherReports.Add(weatherReport);
                    _logger.LogInformation("FromController " + weatherReports.Count.ToString());

                    return true;
                }
            }

            return false;
        }

        [HttpGet]
        [Route("api/[controller]/totalReports")]
        public int Get()
        {
            return weatherReports.TotalReports;
        }
    }
}
