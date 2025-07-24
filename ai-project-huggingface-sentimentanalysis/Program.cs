﻿using System.Globalization;
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

var doc = JsonDocument.Parse(result);
var items = doc.RootElement[0];

var topLabel = items.EnumerateArray().OrderByDescending(x => x.GetProperty("score").GetDouble()).First();

var label = topLabel.GetProperty("label").GetString();
var score = topLabel.GetProperty("score").GetDouble();

string labelText = label switch
{
    "LABEL_0" => "Negative 😒",
    "LABEL_1" => "Neutral 😐",
    "LABEL_2" => "Positive 😊",
    _ => "Unknown"
};

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("\n Girdi Metni: ");
Console.WriteLine($"{text}");

Console.WriteLine("Duygu Analizi: ");
Console.WriteLine($"Duygu Durumu: {labelText}");
Console.WriteLine($"Güven Skoru: %{(score*100).ToString("F2", CultureInfo.InvariantCulture)}");