using System.Net.Http.Headers;

class Program
{
    static async Task Main(string[] args)
    {

        string apiKey = "sk-ant-api01-SAOF9DJoHDOCpKND9E8F8V-0d-vjdIO8DEOI_ILB5Fİh6ouljAOS";
        Console.WriteLine("Anthropic Claude Chat Example");
        string prompt = Console.ReadLine();

        using var client = new HttpClient();
        client.BaseAddress = new Uri("https://api.anthropic.com/v1/");
        client.DefaultRequestHeaders.Add("x-api-key",apiKey);
        client.DefaultRequestHeaders.Add("anthropic-version", "2023-10-22");
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

    }
}