namespace ElShaday.Domain.ValueObjects.Documents;

public sealed class Cpf : Document
{
    private const int CpfLength = 11;

    public Cpf(string value)
    {
        if (string.IsNullOrEmpty(value) || value.Length != CpfLength)
            Valid = false;
        else
        {
            Valid = true;
            Value = value;
        }
    }
    
    // EF Constructor
    protected Cpf() { }
}