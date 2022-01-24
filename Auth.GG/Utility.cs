namespace Auth.GG;

public static class Utility
{
    internal static void IsInitialized(bool end = false)
    {
        if (Constants.Initialized) return;
        MsgShowError("Please initialize your application first!", AuthGG.Name);
        if (end)
            Security.End();
        Process.GetCurrentProcess().Kill();
    }

    internal static void LoginInfo(string AIO, Type type)
    {
        if (IsEmpty(AIO))
        {
            MsgShowError(MessageLogin(type), AppSettings.Name);
            Process.GetCurrentProcess().Kill();
        }
    }

    internal static void LoginInfo(List<string> str, Type type)
    {
        if (IsEmpty(str))
        {
            MsgShowError(MessageLogin(type), AppSettings.Name);
            Process.GetCurrentProcess().Kill();
        }
    }

    internal static string MessageLogin(Type type)
    {
        return type switch
        {
            Type.Login => "Missing user login information!",
            Type.Register => "Invalid registrar information!",
            Type.Subscription => "Invalid registrar information!",
            Type.Picture => "Invalid Picture information!",
            Type.Log => "Missing log information!",
            _ => ""
        };
    }

    internal enum Type
    {
        Login,
        Register,
        Subscription,
        Picture,
        Log
    }

    public static bool IsEmpty(string str)
    {
        return string.IsNullOrWhiteSpace(str);
    }

    public static bool IsEmpty(List<string> str)
    {
        return str.All(string.IsNullOrWhiteSpace);
    }

    public static void MsgShowError(string text, string caption)
    {
        MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
    public static void MsgShowWarning(string text, string caption)
    {
        MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
    public static void MsgShowInfo(string text, string caption)
    {
        MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}