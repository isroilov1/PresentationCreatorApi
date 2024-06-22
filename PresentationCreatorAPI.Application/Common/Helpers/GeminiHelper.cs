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
        string query = $"I need 20 titles on {theme} in {language} language";
        string cseId = "0aa7d95ba3daf77dd0418c878fe7a370f552fff1";
        List<string> titles = new List<string>();
        byte numTitles = 20;
        HttpClient client = new HttpClient();

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash-latest:generateContent?key={apiKey}");
        string jsonContent = $"{{\"contents\":[{{\"parts\":[{{\"text\":\"{query}\"}}]}}]}}";
        request.Content = new StringContent(jsonContent);
        request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        HttpResponseMessage response = await client.SendAsync(request);
        if (response.EnsureSuccessStatusCode().StatusCode != HttpStatusCode.OK)
            throw new StatusCodeException(HttpStatusCode.BadRequest, "Geminidan so'rov qabul qilishda muammo");
        string responseBody = await response.Content.ReadAsStringAsync();
        JObject json = JObject.Parse(responseBody);
        var items = json["items"];
        foreach (var item in items)
        {
            if (titles.Count >= numTitles) break;
            string title = item["title"].ToString();
            titles.Add(title);
        }
        return titles;
    }
}
