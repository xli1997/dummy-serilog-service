using System;
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

        // Create an array of tasks to hold the HTTP requests
        Task[] tasks = new Task[numberOfRequests];

        // Send the requests concurrently
        for (int i = 0; i < numberOfRequests; i++)
        {
            tasks[i] = SendRequest(httpClient, url, i);
        }

        // Wait for all tasks to complete
        await Task.WhenAll(tasks);

        Console.WriteLine("All requests have been sent.");
    }

    static async Task SendRequest(HttpClient httpClient, string url, int requestNumber)
    {
        try
        {
            // Send the HTTP GET request
            var response = await httpClient.GetAsync(url);

            // Read the response
            var content = await response.Content.ReadAsStringAsync();

            // Log the status code and response for each request
            Console.WriteLine($"Request {requestNumber}: {response.StatusCode}");
            Console.WriteLine($"Response {requestNumber}: {content}");
        }
        catch (Exception ex)
        {
            // Handle exceptions
            Console.WriteLine($"Request {requestNumber} failed: {ex.Message}");
        }
    }
}
