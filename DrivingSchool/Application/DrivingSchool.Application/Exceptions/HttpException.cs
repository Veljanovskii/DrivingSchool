using System.Net;

namespace DrivingSchool.Application.Exceptions;

public class HttpException(HttpStatusCode httpStatusCode, string? message = null, object? objectData = null, Exception? innerException = null)
    : Exception(message ?? httpStatusCode.ToString(), innerException)
{
    public HttpStatusCode StatusCode => httpStatusCode;
    public object? ObjectData => objectData;
}