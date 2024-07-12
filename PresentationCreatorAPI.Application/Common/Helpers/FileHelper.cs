using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using PresentationCreatorAPI.Application.Common.Exceptions;
using System.Net;
using System.Security.Policy;
using System.Text.RegularExpressions;

namespace PresentationCreatorAPI.Application.Common.Helpers;

public static class FileHelper
{
    public static string SaveFile(IFormFile file, string rootPath)
    {
        rootPath = $"wwwroot/{rootPath}";
        if (file == null || file.Length == 0)
            throw new ArgumentException("File is invalid.");

        // Fayl yo'li mavjud bo'lmasa, uni yaratish
        if (!Directory.Exists(rootPath))
            Directory.CreateDirectory(rootPath);

        // Generate unique file name to avoid conflicts
        var fileName = Path.GetFileNameWithoutExtension(file.FileName) + "_" + Guid.NewGuid() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(rootPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            file.CopyTo(stream);
        }
        filePath = filePath.Replace("wwwroot/", "");

        return filePath;
    }

    public static string PresentationFilePathCreator(string rootPath)
    {
        rootPath = $"wwwroot/{rootPath}";
        // Fayl yo'li mavjud bo'lmasa, uni yaratish
        if (!Directory.Exists(rootPath))
            Directory.CreateDirectory(rootPath);

        // Generate unique file name to avoid conflicts
        var fileName = "Talabajon_" + Guid.NewGuid() + ".pptx";
        var filePath = (Path.Combine(rootPath, fileName)).Replace("wwwroot/", "");

        return filePath;
    }

    public static List<string> ImageFilePathsCreator(string rootPath, byte count)
    {

        // Fayl yo'li mavjud bo'lmasa, uni yaratish
        if (!Directory.Exists(rootPath))
            Directory.CreateDirectory(rootPath);

        // Generate unique file name to avoid conflicts
        List<string> paths = new List<string>();
        while (count > 0)
        {
            var fileName = "Image_" + Guid.NewGuid() + ".jpg";
            var filePath = Path.Combine(rootPath, fileName);
            paths.Add(filePath);
        }

        return paths;
    }


    private static async Task<string> SavePaymentFileAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("File is invalid.");

        var rootPath = Path.Combine("wwwroot", "uploads", "payments");
        // Fayl yo'li mavjud bo'lmasa, uni yaratish
        if (!Directory.Exists(rootPath))
            Directory.CreateDirectory(rootPath);

        var filePath = Path.Combine(rootPath, file.FileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return filePath;
    }

    // Upload images
    public static async Task<List<string>> UploadImagesForTheme(string theme, byte imageCount)
    {
        var query = theme.Replace(' ', '+');
        HttpClient client = new HttpClient();

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://yandex.com/images/search?tmpl_version=releases%2Ffrontend%2Fimages%2Fv1.1343.0%2304a6efc88b355ad6960f2c301ee84a0925ad6f23&format=json&request=%7B%22blocks%22%3A%5B%7B%22block%22%3A%22extra-content%22%2C%22params%22%3A%7B%7D%2C%22version%22%3A2%7D%2C%7B%22block%22%3A%7B%22block%22%3A%22i-react-ajax-adapter%3Aajax%22%7D%2C%22params%22%3A%7B%22type%22%3A%22ImagesApp%22%2C%22ajaxKey%22%3A%22serpList%2Ffetch%22%7D%2C%22version%22%3A2%7D%5D%2C%22metadata%22%3A%7B%22bundles%22%3A%7B%22lb%22%3A%22k%2BNw%7DkFub%5D%22%7D%2C%22assets%22%3A%7B%22las%22%3A%22justifier-height%3D1%3Bjustifier-setheight%3D1%3Bfitimages-height%3D1%3Bjustifier-fitincuts%3D1%3Breact-with-dom%3D1%3B228.0%3D1%3B236.0%3D1%3Bdb1076.0%3D1%3B5b34af.0%3D1%22%7D%2C%22extraContent%22%3A%7B%22names%22%3A%5B%22i-react-ajax-adapter%22%5D%7D%7D%7D&yu=4288966981718803568&lr=10336&p=1&rpt=image&serpListType=horizontal&serpid=uEls36H-l8M_Y2pkbOqQZA&text={query}&uinfo=sw-1536-sh-864-ww-994-wh-730-pd-1.25-wp-16x9_1920x1080");

        request.Headers.Add("accept", "application/json, text/javascript, */*; q=0.01");
        request.Headers.Add("accept-language", "uz-UZ,uz;q=0.9,en-US;q=0.8,en;q=0.7,ru-RU;q=0.6,ru;q=0.5");
        request.Headers.Add("cookie", "yandex_gid=10336; yuidss=4288966981718803568; is_gdpr=0; is_gdpr_b=COi0exDYggIoAg==; yandexuid=4288966981718803568; yashr=9055964611718803568; receive-cookie-deprecation=1; _ym_uid=1718803570615085201; my=YwA=; ymex=2034163571.yrts.1718803571; _ym_d=1719214219; font_loaded=YSv1; i=DTNTs0Nq2D0A4oopeboxJlPohsC9FXVyF8RHugXyyUtsd3pVOnKU43sOVvieVX5r6ITixt4BsvAJEKFyBSxKxc9r0oY=; gdpr=0; yabs-vdrf=A0; spravka=dD0xNzIwNjIxMjY3O2k9OTIuNjMuMjA0LjY3O0Q9MUFDMEMyNkYwNjJFMUU0NERBN0JBNDdBOUI5RjlGQTNFNjI2QkE3NDAyQzFFRERCN0RGNDBEMkNFM0FBNzExNzRFREQ5MDIxOEQwNzg3NEVGN0ZGRUJCREFEMkQyQzZFMDM0MDg2QUNGNTc0RDJEMUU2QjRFQTk2N0I1NTA1QTU4MDM4QjQ5QzQ0QkVGNDVGRTk7dT0xNzIwNjIxMjY3Mzg3ODUyNzQ0O2g9Nzc0Y2Q4OGUzYmJhNTljMGNjMmZiYTQzNzIyOGU0Y2E=; _ym_isad=2; _yasc=W6cWeZsuA3Lx1eOQKvVDmtl13JyTTp9ZxB409aD1XSpKMqZD9XkQdzZF0gZhg/TIrxswdn4ICl+JFULd; bh=EkAiTm90L0EpQnJhbmQiO3Y9IjgiLCAiQ2hyb21pdW0iO3Y9IjEyNiIsICJHb29nbGUgQ2hyb21lIjt2PSIxMjYiGgUieDg2IiIQIjEyNi4wLjY0NzguMTI3IioCPzAyAiIiOgkiV2luZG93cyJCCCIxNS4wLjAiSgQiNjQiUlwiTm90L0EpQnJhbmQiO3Y9IjguMC4wLjAiLCAiQ2hyb21pdW0iO3Y9IjEyNi4wLjY0NzguMTI3IiwgIkdvb2dsZSBDaHJvbWUiO3Y9IjEyNi4wLjY0NzguMTI3IloCPzBgweG/tAY=; cycada=YSsPQuwdDycQyZKGsB7Fn3hrV2Kev/w24RWdr83NZak=; yp=1721209611.szm.1_25:1536x864:978x730#1725893319.atds.1");
        request.Headers.Add("device-memory", "8");
        request.Headers.Add("downlink", "7.25");
        request.Headers.Add("dpr", "1.25");
        request.Headers.Add("ect", "4g");
        request.Headers.Add("priority", "u=1, i");
        request.Headers.Add("referer", $"https://yandex.com/images/search?text={query}");
        request.Headers.Add("rtt", "100");
        request.Headers.Add("sec-ch-ua", "\"Not/A)Brand\";v=\"8\", \"Chromium\";v=\"126\", \"Google Chrome\";v=\"126\"");
        request.Headers.Add("sec-ch-ua-arch", "\"x86\"");
        request.Headers.Add("sec-ch-ua-bitness", "\"64\"");
        request.Headers.Add("sec-ch-ua-full-version", "\"126.0.6478.127\"");
        request.Headers.Add("sec-ch-ua-full-version-list", "\"Not/A)Brand\";v=\"8.0.0.0\", \"Chromium\";v=\"126.0.6478.127\", \"Google Chrome\";v=\"126.0.6478.127\"");
        request.Headers.Add("sec-ch-ua-mobile", "?0");
        request.Headers.Add("sec-ch-ua-model", "\"\"");
        request.Headers.Add("sec-ch-ua-platform", "\"Windows\"");
        request.Headers.Add("sec-ch-ua-platform-version", "\"15.0.0\"");
        request.Headers.Add("sec-ch-ua-wow64", "?0");
        request.Headers.Add("sec-fetch-dest", "empty");
        request.Headers.Add("sec-fetch-mode", "cors");
        request.Headers.Add("sec-fetch-site", "same-origin");
        request.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36");
        request.Headers.Add("viewport-width", "994");
        request.Headers.Add("x-requested-with", "XMLHttpRequest");

        HttpResponseMessage response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();

        var json = JObject.Parse(responseBody);

        if (json is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "Mavzu bo'yicha rasm qidirishda muammo yuzaga keldi!");

        // Extracting origUrl values
        var origUrls = new List<string>();
        var items = json["blocks"][1]["params"]["adapterData"]["serpList"]["items"]["entities"];
        var rootPath = "wwwroot/uploads/images";
        if(!Directory.Exists(rootPath))
            Directory.CreateDirectory(rootPath);

        int count = 0;

        foreach (var item in items)
        {
            var origUrl = (string)item.First["origUrl"];
            if ((!string.IsNullOrEmpty(origUrl) && (origUrl.EndsWith(".jpg") || origUrl.EndsWith(".png") || origUrl.EndsWith(".jpeg"))))
            {
                count++;
                var path = rootPath + $"/image{count}_{Guid.NewGuid()}.jpg";
                await DownloadImage(origUrl, path);
                path = path.Replace("wwwroot/", "");
                origUrls.Add(path);
                if (count == imageCount)
                {
                    break;
                }
            }
        }
        return origUrls;
    }

    static async Task DownloadImage(string url, string filePath)
    {
        using (HttpClient client = new HttpClient())
        {
            byte[] imageBytes = await client.GetByteArrayAsync(url);
            await File.WriteAllBytesAsync(filePath, imageBytes);
        }
    }
}

