using System.Security.Cryptography;

public static class PkceUtils
{
    public static Dictionary<string, string> GeneratePkceData()
    {
        string codeVerifier = GenerateCodeVerifier();
        string codeChallenge = GenerateCodeChallenge(codeVerifier);
        string state = GenerateState();

        return new()
        {
            { "code_verifier", codeVerifier },
            { "code_challenge", codeChallenge },
            { "state", state }
        };
    }
    private static string GenerateState()
    {
        return Guid.NewGuid().ToString();
    }
    private static string GenerateCodeVerifier()
    {
        byte[] randomBytes = RandomNumberGenerator.GetBytes(32);
        return Base64UrlEncode(randomBytes);
    }

    private static string GenerateCodeChallenge(string codeVerifier)
    {
        byte[] sha256Bytes = SHA256.HashData(
            System.Text.Encoding.UTF8.GetBytes(codeVerifier)
        );
        return Base64UrlEncode(sha256Bytes);
    }

    private static string Base64UrlEncode(byte[] input)
    {
        return Convert.ToBase64String(input)
            .TrimEnd('=')
            .Replace('+', '-')
            .Replace('/', '_');
    }
}