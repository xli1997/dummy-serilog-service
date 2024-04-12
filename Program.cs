using Serilog;
using Serilog.Events;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MyWindowsService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, log.txt);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override(Microsoft, LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.File(logFilePath, rollingInterval RollingInterval.Day)
                .CreateLogger();

            try
            {
                Log.Information(Starting service...);

                var worker = new Worker();
                await worker.StartAsync(CancellationToken.None);

                Log.Information(Service started.);

                await Task.Delay(Timeout.Infinite, CancellationToken.None);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, Service terminated unexpectedly.);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
