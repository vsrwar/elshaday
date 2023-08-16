using System.Security.Cryptography;
using System.Text;

namespace ElShaday.Domain.ValueObjects;

public sealed class Password
{
    private const int KeySize = 64;
    private const int Iterations = 350000;
    private static readonly HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA512;

    public string Hash { get; private set; }
    public byte[] Salt { get; private set; }

    // EF Constructor
    private Password() { } 

    public Password(string password)
    {
        Salt = RandomNumberGenerator.GetBytes(KeySize);
        Hash = ConvertToHash(password);
    }

    public bool Compare(string password)
    {
        var hashedPassword = ConvertToHash(password);

        var left = Convert.FromHexString(hashedPassword);
        var right = Convert.FromHexString(Hash);

        return CryptographicOperations.FixedTimeEquals(left, right);
    }

    private string ConvertToHash(string password)
    {
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            password: Encoding.UTF8.GetBytes(password),
            Salt,
            Iterations,
            HashAlgorithm,
            outputLength: KeySize
        );

        return Convert.ToHexString(hash);
    }
}