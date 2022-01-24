namespace Auth.GG;

internal class Constants
{
    internal static string Token { get; set; }

    internal static string Date { get; set; }

    internal static string APIENCRYPTKEY { get; set; }

    internal static string APIENCRYPTSALT { get; set; }

    internal static bool Breached = false;

    internal static bool Started = false;

    internal static string IV = null;

    internal static string Key = null;

    internal static string ApiUrl = "https://api.auth.gg/csharp/";

    internal static bool Initialized = false;

    internal static Random Random = new();

    internal static string RandomString(int length)
    {
        return new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", length).Select(s => s[Random.Next(s.Length)]).ToArray());
    }

    internal static string HWID()
    {
        var securityIdentifier = WindowsIdentity.GetCurrent().User;
        return securityIdentifier != null ? securityIdentifier.Value : "";
    }
}