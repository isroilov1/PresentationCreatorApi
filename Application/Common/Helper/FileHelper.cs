

namespace Application.Common.Helper;

public static class FileHelper
{
    public static string SaveFile(IFormFile file, string rootPath)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("File is invalid.");

        // Generate unique file name to avoid conflicts
        var fileName = Path.GetFileNameWithoutExtension(file.FileName) + "_" + Guid.NewGuid() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(rootPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            file.CopyTo(stream);
        }

        return filePath;
    }
}