namespace ElShaday.Domain.ValueObjects.Documents;

public sealed class Cnpj : Document
{
    public Cnpj(string value)
    {
        if (string.IsNullOrEmpty(value) || value.Length != 14)
            throw new ArgumentException("CNPJ inválido.");

        Value = value;
    }
}