using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using WeatherReportLibrary;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Middleware
{
    public sealed class CollectionReportable : IWeatherReportable
    {
        public ManualResetEvent ManualResetEvent { get; }

        private Timer timer { get; }

        public CollectionReportable()
        {
            ManualResetEvent = new ManualResetEvent(true);
            timer = new Timer(new TimerCallback(SaveReportsRoutine), ManualResetEvent, 5000, 5000);
        }

        private void SaveReportsRoutine(object state)
        {
            ManualResetEvent.Reset();

            //event_1.Reset();
            foreach (var item in _reports)
            {
                File.AppendAllText("THETESTS.txt", JsonSerializer.Serialize(item));
            }

            Interlocked.Add(ref _totalReports, _reports.Count);
            _reports.Clear();

            ManualResetEvent.Set();
        }

        private List<WeatherReport> _reports { get; } = new List<WeatherReport>();

        public void Add(WeatherReport weatherReport)
        {
            _reports.Add(weatherReport);
        }

        public void Clear()
        {
            _reports.Clear();
        }

        public int Count => _reports.Count;

        public int _totalReports;

        public int TotalReports => _totalReports;
    }
}
