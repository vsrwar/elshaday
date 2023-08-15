namespace ElShaday.Domain.ValueObjects.Documents;

public sealed class Cnpj : Document
{
    public Cnpj(string value)
    {
        Value = value;
    }
}