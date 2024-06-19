namespace Application.Common.Helper;

public class ImageUploader
{
    private async Task<bool> UploadImageAsync(string filePath, int presentationId)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            using (MultipartFormDataContent content = new MultipartFormDataContent())
            {
                byte[] imageData = File.ReadAllBytes(filePath);
                ByteArrayContent byteContent = new ByteArrayContent(imageData);
                byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");

                content.Add(byteContent, "file", Path.GetFileName(filePath));

                HttpResponseMessage response = await client.PostAsync($"uploads/presentationPage/{presentationId}", content);

                return response.IsSuccessStatusCode;
            }
        }
        catch
        {
            return false;
        }
    }
}
