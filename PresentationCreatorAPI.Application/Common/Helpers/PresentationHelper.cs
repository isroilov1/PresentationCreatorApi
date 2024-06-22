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

    public static Task<List<string>> GetTitlesAsync(PresentationLanguage language, string theme)
    {
        var text = GeminiHelper.GetTitlesFromGeminiAsync(theme, language);
        return text;
    }
}
