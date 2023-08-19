using System.Net;
using Microsoft.AspNetCore.Identity;

namespace ISP.Application.Exceptions;

public class InternalServerException : CustomException
{
    public InternalServerException(string message, List<string>? errors = default)
        : base(message, errors)
    {
    }

    public InternalServerException(string message, HttpStatusCode statusCode, List<IdentityError>? errors) : base(message,
        statusCode, errors)
    {
    }

    public InternalServerException(string message, HttpStatusCode statusCode) : base(message, statusCode)
    {
    }

    public InternalServerException(string message, List<IdentityError> errors) : base(message, errors)
    {
    }

    public InternalServerException(List<IdentityError> errors) : base(errors)
    {
    }
}