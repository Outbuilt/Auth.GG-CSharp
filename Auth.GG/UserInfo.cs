namespace Auth.GG;

public class User
{
    public static string ID => UserInfo.ID;

    public static string Username => UserInfo.Username;

    public static string Password => UserInfo.Password;

    public static string Email => UserInfo.Email;

    public static string HWID => UserInfo.HWID;

    public static string IP => UserInfo.IP;

    public static string UserVariable => UserInfo.UserVariable;

    public static string Rank => UserInfo.Rank;

    public static string Expiry => UserInfo.Expiry;

    public static string LastLogin => UserInfo.LastLogin;

    public static string RegisterDate => UserInfo.RegisterDate;

    public static string ProfilePicture => UserInfo.ProfilePicture;
}

internal class UserInfo
{
    public static string ID { get; set; }

    public static string Username { get; set; }

    public static string Password { get; set; }

    public static string Email { get; set; }

    public static string HWID { get; set; }

    public static string IP { get; set; }

    public static string UserVariable { get; set; }

    public static string Rank { get; set; }

    public static string Expiry { get; set; }

    public static string LastLogin { get; set; }

    public static string RegisterDate { get; set; }

    public static string ProfilePicture { get; set; }
}