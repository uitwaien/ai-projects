using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

class program
{
    static async Task Main(string[] args)
    {
      Console.Write("Please input a sentence: ");
        var input = Console.ReadLine();

        using (var client = new HttpClient())
        {
            var apiKey = "js_msakSADPcldDadcAfFGbHhzLsdsDWLqOERIclfkwuWa";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var requestBody = new
            {
                inputs = input,
            };
            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://api-inference.huggingface.co/models/Jean-Baptiste/roberta-large-ner-english", content);
            var responseString = await response.Content.ReadAsStringAsync();

            Console.WriteLine(" NER Çıktısı");
            Console.WriteLine();

            var doc = JsonDocument.Parse(responseString);
            foreach (var item in doc.RootElement.EnumerateArray())
            {
                string entityType = item.GetProperty("entity_group").GetString();
                string word = item.GetProperty("word").GetString();
                double score = Math.Round(item.GetProperty("score").GetDouble() * 100, 2);

                Console.WriteLine($" --> {word}");
                Console.WriteLine($"     Entity Type: {entityType}");
                Console.WriteLine($"     Score: {score}%");
            }
        }

    }
}
