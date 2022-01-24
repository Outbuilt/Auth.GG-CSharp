namespace Auth.GG;

internal class App
{
    public static Dictionary<string, string> Variables = new Dictionary<string, string>();

    public static string GrabVariable(string name)
    {
        try
        {
            if (Utility.IsEmpty(new List<string> { UserInfo.ID, UserInfo.HWID, UserInfo.IP }) || !Constants.Breached)
            {
                Constants.Breached = true;
                return "User is not logged in, possible breach detected!";
            }
            return Variables[name];
        }
        catch
        {
            return "N/A";
        }
    }
    public static void Log(string username, string action)
    {
        Utility.IsInitialized();
        Utility.LoginInfo(new List<string> { username, action }, Utility.Type.Log);
        using var wc = new WebClient();
        try
        {
            Security.Start();
            wc.Proxy = null;
            var _ = Encryption.DecryptService(Encoding.Default.GetString(wc.UploadValues(Constants.ApiUrl, new NameValueCollection
            {
                ["token"] = Encryption.EncryptService(Constants.Token),
                ["aid"] = Encryption.APIService(AuthGG.AID),
                ["username"] = Encryption.APIService(username),
                ["pcuser"] = Encryption.APIService(Environment.UserName),
                ["session_id"] = Constants.IV,
                ["api_id"] = Constants.APIENCRYPTSALT,
                ["api_key"] = Constants.APIENCRYPTKEY,
                ["data"] = Encryption.APIService(action),
                ["session_key"] = Constants.Key,
                ["secret"] = Encryption.APIService(AuthGG.Secret),
                ["type"] = Encryption.APIService("log")
            }))).Split("|".ToCharArray());
            Security.End();
        }
        catch (Exception ex)
        {
            Utility.MsgShowError(ex.Message, AuthGG.Name);
            Process.GetCurrentProcess().Kill();
        }
    }
}