using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

var apiKey = "js_msakSADPcldDadcAfFGbHhzLsdsDWLqOERIclfkwuWa";
var text = "this is a test";    

var modelUrl = "https://api-inference.huggingface.co/models/cardiffnlp/twitter-roberta-base-sentiment";

using var client = new HttpClient();
client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

var json = JsonSerializer.Serialize(new { inputs = text });
var content = new StringContent(json, Encoding.UTF8, "application/json");

var response = await client.PostAsync(modelUrl, content);
var result = await response.Content.ReadAsStringAsync();