using System.Threading;
using WeatherReportLibrary;

namespace Middleware
{
    public interface IWeatherReportable
    {
        int Count { get; }
        ManualResetEvent ManualResetEvent { get; }
        int TotalReports { get; }

        void Add(WeatherReport weatherReport);
        void Clear();
    }
}