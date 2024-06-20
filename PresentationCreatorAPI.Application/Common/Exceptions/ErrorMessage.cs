namespace PresentationCreatorAPI.Application.Common.Exceptions;

public class ErrorMessage
{
    public ushort StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
}