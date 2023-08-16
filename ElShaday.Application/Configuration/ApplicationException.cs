namespace ElShaday.Application.Configuration;

public class ApplicationException : Exception
{
    public ApplicationException(string message) : base(message)
    { }
}