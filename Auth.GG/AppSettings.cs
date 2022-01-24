namespace Auth.GG;

public class ApplicationSettings
{
    public static bool Status => AppSettings.Status;

    public static bool DeveloperMode => AppSettings.DeveloperMode;

    public static string Hash => AppSettings.Hash;

    public static string Version => AppSettings.Version;

    public static string UpdateLink => AppSettings.UpdateLink;

    public static bool FreeMode => AppSettings.FreeMode;

    public static bool Login => AppSettings.Login;

    public static string Name => AppSettings.Name;

    public static bool Register => AppSettings.Register;

    public static string TotalUsers => AppSettings.TotalUsers;
}
internal class AppSettings
{
    internal static bool Status { get; set; }

    internal static bool DeveloperMode { get; set; }

    internal static string Hash { get; set; }

    internal static string Version { get; set; }

    internal static string UpdateLink { get; set; }

    internal static bool FreeMode { get; set; }

    internal static bool Login { get; set; }

    internal static string Name { get; set; }

    internal static bool Register { get; set; }

    internal static string TotalUsers { get; set; }
}