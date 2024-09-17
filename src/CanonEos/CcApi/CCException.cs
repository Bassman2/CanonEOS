using System.Net;

namespace CanonEos.CcApi;

internal class CCException : Exception
{
    public CCException(string? message, HttpStatusCode statusCode) : base($"{statusCode}: {message}")
    { }
}
