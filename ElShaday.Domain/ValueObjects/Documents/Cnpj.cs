namespace ElShaday.Domain.ValueObjects.Documents;

public sealed class Cnpj : Document
{
    private const int CnpjLength = 14;

    public Cnpj(string value)
    {
        if (string.IsNullOrEmpty(value) || value.Length != CnpjLength)
            Valid = false;
        else
        {
            Valid = true;
            Value = value;    
        }
    }
    
    // EF Constructor
    protected Cnpj() { }
}