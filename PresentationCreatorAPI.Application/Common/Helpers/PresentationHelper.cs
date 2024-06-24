using PresentationCreatorAPI.Domain.Enums;

namespace PresentationCreatorAPI.Application.Common.Helpers;

public class PresentationHelper
{
    public static string GetPlanWithLang(PresentationLanguage language)
    {
        var plan = "Reja:";
        if (language == PresentationLanguage.English)
            plan = "Plan:";
        else if (language == PresentationLanguage.Russian)
            plan = "План:";
        else if (language == PresentationLanguage.German)
            plan = "Planen";
        else if (language == PresentationLanguage.French)
            plan = "Plan";
        return plan;
    } 

    public static async Task<List<string>> GetTitlesAsync(PresentationLanguage language, string theme)
    {
        //var titles = await GeminiHelper.GetTitlesFromGeminiAsync(theme, language);
        string text = await File.ReadAllTextAsync("geminiresponsetext.txt");
        List<string> titles = new List<string>(text.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries));

        return titles;
    }

    public static async Task<string> GetInformationAsync(PresentationLanguage language, string theme)
    {
        var text = await GeminiHelper.GetInfoFromGeminiAsync(theme, language);
        //string text = await File.ReadAllTextAsync("geminiresponseinformation.txt");
        return text;
    }
}
