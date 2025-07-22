using System.Net.Http.Headers;
using System.Text.Json;

var apiKey = "28a2894ae72a89a3473e274db923cc49";
var filePath = "seungmin.mp3";

if(!File.Exists(filePath))
{
    Console.WriteLine($"File not found: {filePath}");
    return;
}
using var client = new HttpClient();
client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", apiKey);
using var fileStream = File.OpenRead(filePath);

var content = new StreamContent(fileStream);
content.Headers.ContentType = new MediaTypeHeaderValue("audio/mp3");

var response = await client.PostAsync("https://api.deepgram.com/v1/listen", content);
var json = await response.Content.ReadAsStringAsync();

try
{
    var doc = JsonDocument.Parse(json);
    var transcription = doc.RootElement.GetProperty("results").GetProperty("channels").GetProperty("alternatives")[0].GetProperty("transcript").GetString();
    Console.WriteLine();
    Console.WriteLine("Transcription:\n");
    Console.WriteLine(transcription);
}
catch (Exception ex)
{
    Console.WriteLine("Json Çözümleme sırasında hata yapılmıştır.");
    Console.WriteLine(ex.Message);
    Console.WriteLine("JSON içeriği:\n");
    throw;
}