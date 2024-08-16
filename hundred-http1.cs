using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // The number of concurrent requests
        int numberOfRequests = 100;

        // The URL of the HTTP service you want to test
        string url = "https://example.com/api/test";

        // Initialize an HttpClient
        using var httpClient = new HttpClient();

        // Variables to track statistics
        int successfulRequests = 0;
        double totalResponseTime = 0;

        // Create an array of tasks to hold the HTTP requests
        Task[] tasks = new Task[numberOfRequests];

        // Send the requests concurrently
        for (int i = 0; i < numberOfRequests; i++)
        {
            tasks[i] = SendRequest(httpClient, url, i, ref successfulRequests, ref totalResponseTime);
        }

        // Wait for all tasks to complete
        await Task.WhenAll(tasks);

        // Calculate average response time and success rate
        double averageResponseTime = totalResponseTime / numberOfRequests;
        double successRate = (double)successfulRequests / numberOfRequests * 100;

        // Output statistics
        Console.WriteLine($"Total Requests: {numberOfRequests}");
        Console.WriteLine($"Successful Requests: {successfulRequests}");
        Console.WriteLine($"Average Response Time: {averageResponseTime} ms");
        Console.WriteLine($"Success Rate: {successRate}%");
    }

    static async Task SendRequest(HttpClient httpClient, string url, int requestNumber, ref int successfulRequests, ref double totalResponseTime)
    {
        try
        {
            // Start the stopwatch to measure response time
            var stopwatch = Stopwatch.StartNew();

            // Send the HTTP GET request
            var response = await httpClient.GetAsync(url);

            // Stop the stopwatch
            stopwatch.Stop();

            // Record the response time
            double responseTime = stopwatch.Elapsed.TotalMilliseconds;
            totalResponseTime += responseTime;

            // Read the response content
            var content = await response.Content.ReadAsStringAsync();

            // Log the status code, response, and response time
            Console.WriteLine($"Request {requestNumber}: {response.StatusCode}, Response Time: {responseTime} ms");

            // Check if the request was successful (2xx status code)
            if (response.IsSuccessStatusCode)
            {
                successfulRequests++;
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions
            Console.WriteLine($"Request {requestNumber} failed: {ex.Message}");
        }
    }
}
