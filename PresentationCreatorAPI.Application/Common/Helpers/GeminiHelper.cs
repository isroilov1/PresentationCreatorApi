using Newtonsoft.Json.Linq;
using PresentationCreatorAPI.Application.Common.Exceptions;
using PresentationCreatorAPI.Domain.Enums;
using System.Net;
using System.Net.Http.Headers;

namespace PresentationCreatorAPI.Application.Common.Helpers;

public class GeminiHelper
{
    public static async Task<List<string>> GetTitlesFromGeminiAsync(string theme, PresentationLanguage language)
    {
        string apiKey = "AIzaSyDHzIy7ZJSEpHB9hSbcz8fwWUabY9CUaZw";
        string query = $"i need only 18 topics related for {theme} topic in {language} and it must be only topics and numbers nothing more";
        
        HttpClient client = new HttpClient();

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash-latest:generateContent?key={apiKey}");
        string jsonContent = $"{{\"contents\":[{{\"parts\":[{{\"text\":\"{query}\"}}]}}]}}";
        request.Content = new StringContent(jsonContent);
        request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        HttpResponseMessage response = await client.SendAsync(request);
        if (response.StatusCode != HttpStatusCode.OK)
            throw new StatusCodeException(HttpStatusCode.BadRequest, "Geminidan so'rov qabul qilishda muammo");
        string responseBody = await response.Content.ReadAsStringAsync();
        await File.WriteAllTextAsync("geminiresponsebody.txt", responseBody);
        JObject json = JObject.Parse(responseBody);
        var text = json["candidates"]?[0]?["content"]?["parts"]?[0]?["text"]?.ToString();
        
        // Faylga yozish
        await File.WriteAllTextAsync("geminiresponsetext.txt", text);
        if (text is null)
            throw new StatusCodeException(HttpStatusCode.BadRequest, "So'rov bo'yicha hech qanday ma'lumot topilmadi!");
        List<string> titles = new List<string>(text.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries));
        
        return titles;
    }

    public static async Task<string> GetInfoFromGeminiAsync(string theme, PresentationLanguage language)
    {
        string apiKey = "AIzaSyDHzIy7ZJSEpHB9hSbcz8fwWUabY9CUaZw";
        string query = $"i need only information with a minimum of 50 and a maximum of 200 words related for {theme} topic in {language} and it must be only information and numbers nothing more";

        HttpClient client = new HttpClient();
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash-latest:generateContent?key={apiKey}");
        string jsonContent = $"{{\"contents\":[{{\"parts\":[{{\"text\":\"{query}\"}}]}}]}}";
        request.Content = new StringContent(jsonContent);
        request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        HttpResponseMessage response = await client.SendAsync(request);
        if (response.StatusCode != HttpStatusCode.OK)
            throw new StatusCodeException(HttpStatusCode.BadRequest, "Geminidan so'rov qabul qilishda muammo");
        string responseBody = await response.Content.ReadAsStringAsync();
        await File.WriteAllTextAsync("geminiresponsebody2.txt", responseBody);
        JObject json = JObject.Parse(responseBody);
        var text = json["candidates"]?[0]?["content"]?["parts"]?[0]?["text"]?.ToString();

        // Faylga yozish
        await File.WriteAllTextAsync("geminiresponseinformation.txt", text);
        if (text is null)
            throw new StatusCodeException(HttpStatusCode.BadRequest, "So'rov bo'yicha hech qanday ma'lumot topilmadi!");

        return text;
    }
}
