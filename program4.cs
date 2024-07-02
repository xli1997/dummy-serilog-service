using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        // Optionally, you can also build and run a WebApplication within the host.
        var builder = WebApplication.CreateBuilder(new WebApplicationOptions
        {
            ContentRootPath = AppContext.BaseDirectory,
            Args = args
        });

        ConfigureServices(builder.Services);
        var app = builder.Build();
        Configure(app);

        if (OperatingSystem.IsWindows() && !Environment.UserInteractive)
        {
            // Running as a Windows Service
            host.RunAsService();
        }
        else
        {
            // Running as a Console App
            host.Run();
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<Worker>();
            })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
                logging.AddEventLog();
            });

    private static void ConfigureServices(IServiceCollection services)
    {
        // Add services to the container.
    }

    private static void Configure(WebApplication app)
    {
        // Configure the HTTP request pipeline.
        app.MapGet("/", () => "Worker Service is running.");
    }
}

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker doing background work at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Worker operation canceled.");
        }
        finally
        {
            _logger.LogInformation("Worker stopping at: {time}", DateTimeOffset.Now);
        }
    }
}
