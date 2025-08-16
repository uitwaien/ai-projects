using System.Net.Http.Headers;
using System.Text.Json;

class Program
{
    static async Task Main(string[] args)
    {
        var apiKey = "js_msakSADPcldDadcAfFGbHhzLsdsDWLqOERIclfkwuWa";
        Console.Write("Enter your commment here: ");
        string inputText = Console.ReadLine();

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            var request = new
            {
                inputs = inputText,
            };
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://api-inference.huggingface.co/models/joeddav/toxic-bert", content);

            var responseString = await response.Content.ReadAsStringAsync();

            if(!responseString.TrimStart().StartsWith("["))
            {
                Console.WriteLine("Model yüklenirken bir hata oluştu.");
                Console.WriteLine(responseString);
                return;
            }

            var doc = JsonDocument.Parse(responseString);
            Console.WriteLine("\n Yorum analiz sonucu: \n");
            foreach (var item in doc.RootElement[0].EnumerateArray())
            {
                string label = item.GetProperty("label").GetString();
                double score = Math.Round(item.GetProperty("score").GetDouble() * 100, 2);

                Console.WriteLine($"{label.ToUpper()} -->  %{score}");

            }
        }
    }
}