using System.Net;
using Microsoft.AspNetCore.Identity;

namespace ISP.Application.Exceptions;

public class CustomException : Exception
{
    protected CustomException(string message, List<string>? errors = default,
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        : base(message)
    {
        ErrorMessages = errors;
        StatusCode = statusCode;
    }

    protected CustomException(string message, HttpStatusCode statusCode, List<IdentityError> errors) : base(message)
    {
        StatusCode = statusCode;
        Errors = errors;
    }

    protected CustomException(string message, HttpStatusCode statusCode) : base(message)
    {
        StatusCode = statusCode;
    }

    protected CustomException(string message, List<IdentityError> errors) : base(message)
    {
        Errors = errors;
    }

    protected CustomException(List<IdentityError> errors)
    {
        Errors = errors;
    }


    public List<string>? ErrorMessages { get; }
    public HttpStatusCode StatusCode { get; }
    public List<IdentityError>? Errors { get; }
}