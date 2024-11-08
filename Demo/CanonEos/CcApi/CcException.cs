namespace CanonEos.CcApi;

public class CcException : Exception
{
    public CcException(string? message) : base(message)
    { }

    public CcException(string? message, string? requestUri, HttpStatusCode statusCode) : base($"{statusCode}: \"{requestUri}\" {message}")
    { }
}
