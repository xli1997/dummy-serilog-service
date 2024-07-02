using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configure logging
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();

        // Add hosted service
        builder.Services.AddHostedService<Worker>();

        var app = builder.Build();

        // Add minimal endpoint for testing (optional)
        app.MapGet("/", () => "Worker Service is running.");

        // Run the application as a Windows Service if not in debug mode
#if DEBUG
        app.Run();
#else
        app.RunAsService();
#endif
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
