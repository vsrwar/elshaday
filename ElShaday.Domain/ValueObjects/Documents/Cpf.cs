namespace ElShaday.Domain.ValueObjects.Documents;

public sealed class Cpf : Document
{
    public Cpf(string value)
    {
        Value = value;
    }
}