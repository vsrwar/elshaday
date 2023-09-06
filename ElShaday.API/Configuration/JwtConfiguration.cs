namespace ElShaday.API.Configuration;

public sealed class JwtConfiguration
{
    public string Secret { get; private set; }
    public string Audience { get; private set; }
    public string Issuer { get; private set; }

    public JwtConfiguration(string secret, string audience, string issuer)
    {
        ValidateFields(secret, audience, issuer);
        Secret = secret;
        Audience = audience;
        Issuer = issuer;
    }

    private void ValidateFields(string secret, string audience, string issuer)
    {
        if (string.IsNullOrEmpty(secret))
            throw new ArgumentException("Jwt Secret is null or empty.");
        if (string.IsNullOrEmpty(audience))
            throw new ArgumentException("Jwt Audience is null or empty.");
        if (string.IsNullOrEmpty(issuer))
            throw new ArgumentException("Jwt Issuer is null or empty.");
    }
}