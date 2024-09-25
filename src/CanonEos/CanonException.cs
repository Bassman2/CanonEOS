namespace CanonEos;

public class CanonException : Exception
{
    public CanonException(string message) : base(message) { }

    public CanonException(string device, string message) : base($"{device}: {message}") { }
}
