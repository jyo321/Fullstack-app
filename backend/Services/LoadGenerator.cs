using System.Net.Http;

public class LoadGenerator : BackgroundService
{
    private readonly HttpClient _httpClient = new();

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
{
    while (!stoppingToken.IsCancellationRequested)
    {
        try
        {
            await _httpClient.GetAsync("http://backend:5000/api/load");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Load error: {ex.Message}");
        }

        await Task.Delay(1000);
    }
}
}