using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;

class Program
{
    static async Task Main(string[] args)
    {
        string apiKey = "js_msakSADPcldDadcAfFGbHhzLsdsDWLqOERIclfkwuWa";

        Console.Write("------Context Area------");
        Console.WriteLine();   
        Console.WriteLine();

        Console.Write("Please your context here: ");
        var context = Console.ReadLine();

        Console.WriteLine();
        Console.WriteLine();
        Console.Write("------Question Area------");
        Console.WriteLine();
        Console.WriteLine();

        var question = Console.ReadLine();

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var requestBody = new
            {
                inputs = new
                {
                    question = question,
                    context = context
                }
            };
            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://api-inference.huggingface.co/models/roberta-base-squad2", content);

            var responseString = await response.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(responseString);

            if(doc.RootElement.TryGetProperty("answer", out var answer))
            {
                Console.WriteLine($"Soru: " + question);
                Console.WriteLine($"Metin: " + context);
                Console.WriteLine($"Cevap: " + answer.GetString);
            }
            else
            {
                Console.WriteLine("No answer found.");
                Console.WriteLine(responseString);
            }
        }

        Console.WriteLine();
    }
}