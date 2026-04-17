using System.Net.Http;

public class LoadGenerator : BackgroundService
{
    private readonly HttpClient _httpClient = new();

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await _httpClient.GetAsync("http://localhost:5000/api/hello");
            await Task.Delay(1000, stoppingToken);
        }
    }
}