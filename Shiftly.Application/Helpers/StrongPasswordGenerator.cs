namespace Shiftly.Application.Helpers;

public static class StrongPasswordGenerator
{
    public static string Generate()
    {
        // 16-char strong password with upper/lower/digits/symbols
        const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string lower = "abcdefghijklmnopqrstuvwxyz";
        const string digits = "0123456789";
        const string symbols = "!@#$%^&*()-_=+[]{};:,.<>?";
        var all = upper + lower + digits + symbols;

        var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
        Span<byte> buffer = stackalloc byte[16];
        rng.GetBytes(buffer);

        var chars = new char[16];
        for (int i = 0; i < chars.Length; i++)
        {
            chars[i] = all[buffer[i] % all.Length];
        }
        return new string(chars);
    }
}