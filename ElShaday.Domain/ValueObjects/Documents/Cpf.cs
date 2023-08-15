namespace ElShaday.Domain.ValueObjects.Documents;

public sealed class Cpf : Document
{
    public Cpf(string value)
    {
        if (string.IsNullOrEmpty(value) || value.Length != 11)
            throw new ArgumentException("CPF inválido.");

        Value = value;
    }
}