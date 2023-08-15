using ElShaday.Domain.ValueObjects;
using ElShaday.Domain.ValueObjects.Documents;

namespace ElShaday.Domain.Factories;

public static class DocumentFactory
{
    public const int CPF_LENGTH = 11;
    public const int CNPJ_LENGTH = 14;

    public static Document Create(string value)
    {
        switch (value.Length)
        {
            case CPF_LENGTH:
                return new Cpf(value);
            case CNPJ_LENGTH:
                return new Cnpj(value);
            default:
                throw new ArgumentException("Invalid document length");
        }
    }
}