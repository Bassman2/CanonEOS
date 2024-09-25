namespace CanonEos.CcApi;

internal class CCException : Exception
{
    public CCException(string? message, string? requestUri, HttpStatusCode statusCode) : base($"{statusCode}: \"{requestUri}\" {message}")
    { }
}
