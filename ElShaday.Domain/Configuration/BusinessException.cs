namespace ElShaday.Domain.Configuration;

public class BusinessException : Exception
{
    public BusinessException(string message) : base(message)
    { }
}