using Microsoft.AspNetCore.Http;

namespace PresentationCreatorAPI.Application.Common.Helpers;

public static class FileHelper
{
    public static string SaveFile(IFormFile file, string rootPath)
    {
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

        return filePath;
    }

    public static string SavePresentationFile(IFormFile file, string rootPath)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("File is invalid.");

        // Fayl yo'li mavjud bo'lmasa, uni yaratish
        if (!Directory.Exists(rootPath))
            Directory.CreateDirectory(rootPath);

        // Generate unique file name to avoid conflicts
        var fileName = Path.GetFileNameWithoutExtension(file.FileName) + "_" + Guid.NewGuid() + Path.GetExtension(file.FileName) + ".pptx";
        var filePath = Path.Combine(rootPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            file.CopyTo(stream);
        }

        return filePath;
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
}
