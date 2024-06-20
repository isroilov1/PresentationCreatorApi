using System.Net;

namespace PresentationCreatorAPI.Application.Common.Exceptions;

public class StatusCodeException : Exception
{
    public HttpStatusCode StatusCode { get; }

    public StatusCodeException(HttpStatusCode code, string message)
        : base(message)
    {
        StatusCode = code;
    }
}
