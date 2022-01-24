namespace Auth.GG;

public class UserSystem
{
    public static bool AIO(string AIO)
    {
        return AIOLogin(AIO) || AIORegister(AIO);
    }

    public static bool AIOLogin(string AIO)
    {
        Utility.IsInitialized();
        Utility.LoginInfo(AIO, Utility.Type.Login);
        using var wc = new WebClient();
        try
        {
            Security.Start();
            wc.Proxy = null;
            var response = Encryption.DecryptService(Encoding.Default.GetString(wc.UploadValues(Constants.ApiUrl, new NameValueCollection
            {
                ["token"] = Encryption.EncryptService(Constants.Token),
                ["timestamp"] = Encryption.EncryptService(DateTime.Now.ToString(CultureInfo.InvariantCulture)),
                ["aid"] = Encryption.APIService(AuthGG.AID),
                ["session_id"] = Constants.IV,
                ["api_id"] = Constants.APIENCRYPTSALT,
                ["api_key"] = Constants.APIENCRYPTKEY,
                ["username"] = Encryption.APIService(AIO),
                ["password"] = Encryption.APIService(AIO),
                ["hwid"] = Encryption.APIService(Constants.HWID()),
                ["session_key"] = Constants.Key,
                ["secret"] = Encryption.APIService(AuthGG.Secret),
                ["type"] = Encryption.APIService("login")
            }))).Split("|".ToCharArray());
            return LoginResponse(response);
        }
        catch (Exception ex)
        {
            Utility.MsgShowError(ex.Message, AppSettings.Name);
            Security.End();
            Process.GetCurrentProcess().Kill();
        }
        return false;
    }

    public static bool Login(string username, string password)
    {
        Utility.IsInitialized();
        Utility.LoginInfo(new List<string> { username, password }, Utility.Type.Login);
        using var wc = new WebClient();
        try
        {
            Security.Start();
            wc.Proxy = null;
            var response = Encryption.DecryptService(Encoding.Default.GetString(wc.UploadValues(Constants.ApiUrl, new NameValueCollection
            {
                ["token"] = Encryption.EncryptService(Constants.Token),
                ["timestamp"] = Encryption.EncryptService(DateTime.Now.ToString(CultureInfo.InvariantCulture)),
                ["aid"] = Encryption.APIService(AuthGG.AID),
                ["session_id"] = Constants.IV,
                ["api_id"] = Constants.APIENCRYPTSALT,
                ["api_key"] = Constants.APIENCRYPTKEY,
                ["username"] = Encryption.APIService(username),
                ["password"] = Encryption.APIService(password),
                ["hwid"] = Encryption.APIService(Constants.HWID()),
                ["session_key"] = Constants.Key,
                ["secret"] = Encryption.APIService(AuthGG.Secret),
                ["type"] = Encryption.APIService("login")
            }))).Split("|".ToCharArray());
            return LoginResponse(response);
        }
        catch (Exception ex)
        {
            Utility.MsgShowError(ex.Message, AppSettings.Name);
            Security.End();
            Process.GetCurrentProcess().Kill();
        }
        return false;
    }

    private static bool LoginResponse(IReadOnlyList<string> response)
    {
        if (response[0] != Constants.Token)
        {
            Utility.MsgShowError("Security error has been triggered!", AuthGG.AppName);
            Process.GetCurrentProcess().Kill();
        }
        if (Security.MaliciousCheck(response[1]))
        {
            Utility.MsgShowWarning("Possible malicious activity detected!", AuthGG.AppName);
            Process.GetCurrentProcess().Kill();
        }
        if (Constants.Breached)
        {
            Utility.MsgShowWarning("Possible malicious activity detected!", AuthGG.AppName);
            Process.GetCurrentProcess().Kill();
        }
        switch (response[2])
        {
            case "success":
                Security.End();
                UserInfo.ID = response[3];
                UserInfo.Username = response[4];
                UserInfo.Password = response[5];
                UserInfo.Email = response[6];
                UserInfo.HWID = response[7];
                UserInfo.UserVariable = response[8];
                UserInfo.Rank = response[9];
                UserInfo.IP = response[10];
                UserInfo.Expiry = response[11];
                UserInfo.LastLogin = response[12];
                UserInfo.RegisterDate = response[13];
                var variables = response[14];
                if (!AuthGG.IsConsole) UserInfo.ProfilePicture = response[15];
                foreach (var var in variables.Split('~'))
                {
                    var items = var.Split('^');
                    try
                    {
                        App.Variables.Add(items[0], items[1]);
                    }
                    catch
                    {
                        //If some are null or not loaded, just ignore.
                        //Error will be shown when loading the variable anyways
                    }
                }
                return true;

            case "invalid_details":
                Security.End();
                return false;

            case "time_expired":
                Utility.MsgShowWarning("Your subscription has expired!", AppSettings.Name);
                Security.End();
                if (AuthGG.IsConsole) Process.GetCurrentProcess().Kill();
                return false;

            case "hwid_updated":
                Utility.MsgShowInfo("New machine has been binded, re-open the application!", AppSettings.Name);
                Security.End();
                if (AuthGG.IsConsole) Process.GetCurrentProcess().Kill();
                return false;

            case "invalid_hwid":
                Utility.MsgShowError("This user is binded to another computer, please contact support!", AppSettings.Name);
                Security.End();
                if (AuthGG.IsConsole) Process.GetCurrentProcess().Kill();
                return false;
        }
        return false;
    }

    public static bool AIORegister(string AIO)
    {
        Utility.IsInitialized(true);
        Utility.LoginInfo(AIO, Utility.Type.Register);
        using var wc = new WebClient();
        try
        {
            Security.Start();
            wc.Proxy = null;

            var response = Encryption.DecryptService(Encoding.Default.GetString(wc.UploadValues(Constants.ApiUrl, new NameValueCollection
            {
                ["token"] = Encryption.EncryptService(Constants.Token),
                ["timestamp"] = Encryption.EncryptService(DateTime.Now.ToString(CultureInfo.InvariantCulture)),
                ["aid"] = Encryption.APIService(AuthGG.AID),
                ["session_id"] = Constants.IV,
                ["api_id"] = Constants.APIENCRYPTSALT,
                ["api_key"] = Constants.APIENCRYPTKEY,
                ["session_key"] = Constants.Key,
                ["secret"] = Encryption.APIService(AuthGG.Secret),
                ["type"] = Encryption.APIService("register"),
                ["username"] = Encryption.APIService(AIO),
                ["password"] = Encryption.APIService(AIO),
                ["email"] = Encryption.APIService(AIO),
                ["license"] = Encryption.APIService(AIO),
                ["hwid"] = Encryption.APIService(Constants.HWID()),
            }))).Split("|".ToCharArray());
            return RegisterResponse(response);
        }
        catch (Exception ex)
        {
            Utility.MsgShowError(ex.Message, AppSettings.Name);
            Process.GetCurrentProcess().Kill();
        }
        return false;
    }

    public static bool Register(string username, string password, string email, string license)
    {
        Utility.IsInitialized(true);
        Utility.LoginInfo(new List<string> { username, password, email, license }, Utility.Type.Register);
        using var wc = new WebClient();
        try
        {
            Security.Start();
            wc.Proxy = null;

            var response = Encryption.DecryptService(Encoding.Default.GetString(wc.UploadValues(Constants.ApiUrl, new NameValueCollection
            {
                ["token"] = Encryption.EncryptService(Constants.Token),
                ["timestamp"] = Encryption.EncryptService(DateTime.Now.ToString(CultureInfo.InvariantCulture)),
                ["aid"] = Encryption.APIService(AuthGG.AID),
                ["session_id"] = Constants.IV,
                ["api_id"] = Constants.APIENCRYPTSALT,
                ["api_key"] = Constants.APIENCRYPTKEY,
                ["session_key"] = Constants.Key,
                ["secret"] = Encryption.APIService(AuthGG.Secret),
                ["type"] = Encryption.APIService("register"),
                ["username"] = Encryption.APIService(username),
                ["password"] = Encryption.APIService(password),
                ["email"] = Encryption.APIService(email),
                ["license"] = Encryption.APIService(license),
                ["hwid"] = Encryption.APIService(Constants.HWID()),
            }))).Split("|".ToCharArray());
            return RegisterResponse(response);
        }
        catch (Exception ex)
        {
            Utility.MsgShowError(ex.Message, AppSettings.Name);
            Process.GetCurrentProcess().Kill();
        }
        return false;
    }

    private static bool RegisterResponse(IReadOnlyList<string> response)
    {
        if (response[0] != Constants.Token)
        {
            Utility.MsgShowError("Security error has been triggered!", AuthGG.AppName);
            Security.End();
            Process.GetCurrentProcess().Kill();
        }
        if (Security.MaliciousCheck(response[1]))
        {
            Utility.MsgShowWarning("Possible malicious activity detected!", AuthGG.AppName);
            Process.GetCurrentProcess().Kill();
        }
        if (Constants.Breached)
        {
            Utility.MsgShowWarning("Possible malicious activity detected!", AuthGG.AppName);
            Process.GetCurrentProcess().Kill();
        }
        Security.End();
        switch (response[2])
        {
            case "success":
                Security.End();
                return true;

            case "error":
                return false;

            case "invalid_license":
                Utility.MsgShowError("License does not exist!", AppSettings.Name);
                Security.End();
                if (AuthGG.IsConsole) Process.GetCurrentProcess().Kill();
                return false;

            case "email_used":
                Utility.MsgShowError("Email has already been used!", AppSettings.Name);
                Security.End();
                if (AuthGG.IsConsole) Process.GetCurrentProcess().Kill();
                return false;

            case "invalid_username":
                Utility.MsgShowError("You entered an invalid/used username!", AppSettings.Name);
                Security.End();
                if (AuthGG.IsConsole) Process.GetCurrentProcess().Kill();
                return false;
        }

        return false;
    }

    public static bool ExtendSubscription(string username, string password, string license)
    {
        Utility.IsInitialized(true);
        Utility.LoginInfo(new List<string> { username, password, license }, Utility.Type.Subscription);
        using var wc = new WebClient();
        try
        {
            Security.Start();
            wc.Proxy = null;
            var response = Encryption.DecryptService(Encoding.Default.GetString(wc.UploadValues(Constants.ApiUrl, new NameValueCollection
            {
                ["token"] = Encryption.EncryptService(Constants.Token),
                ["timestamp"] = Encryption.EncryptService(DateTime.Now.ToString(CultureInfo.InvariantCulture)),
                ["aid"] = Encryption.APIService(AuthGG.AID),
                ["session_id"] = Constants.IV,
                ["api_id"] = Constants.APIENCRYPTSALT,
                ["api_key"] = Constants.APIENCRYPTKEY,
                ["session_key"] = Constants.Key,
                ["secret"] = Encryption.APIService(AuthGG.Secret),
                ["type"] = Encryption.APIService("extend"),
                ["username"] = Encryption.APIService(username),
                ["password"] = Encryption.APIService(password),
                ["license"] = Encryption.APIService(license),
            }))).Split("|".ToCharArray());
            if (response[0] != Constants.Token)
            {
                Utility.MsgShowError("Security error has been triggered!", AuthGG.AppName);
                Security.End();
                Process.GetCurrentProcess().Kill();
            }
            if (Security.MaliciousCheck(response[1]))
            {
                Utility.MsgShowWarning("Possible malicious activity detected!", AuthGG.AppName);
                Process.GetCurrentProcess().Kill();
            }
            if (Constants.Breached)
            {
                Utility.MsgShowWarning("Possible malicious activity detected!", AuthGG.AppName);
                Process.GetCurrentProcess().Kill();
            }
            switch (response[2])
            {
                case "success":
                    Security.End();
                    return true;

                case "invalid_token":
                    Utility.MsgShowError("Token does not exist!", AppSettings.Name);
                    Security.End();
                    Process.GetCurrentProcess().Kill();
                    return false;

                case "invalid_details":
                    Utility.MsgShowError("Your user details are invalid!", AppSettings.Name);
                    Security.End();
                    Process.GetCurrentProcess().Kill();
                    return false;
            }
        }
        catch (Exception ex)
        {
            Utility.MsgShowError(ex.Message, AppSettings.Name);
            Process.GetCurrentProcess().Kill();
        }
        return false;
    }

    public static void UploadPic(string username, string path)
    {
        Utility.LoginInfo(new List<string> { username, path }, Utility.Type.Picture);
        using var wc = new WebClient();
        try
        {
            wc.Proxy = null;
            Security.Start();
            var response = Encryption.DecryptService(Encoding.Default.GetString(wc.UploadValues(Constants.ApiUrl, new NameValueCollection
            {
                ["token"] = Encryption.EncryptService(Constants.Token),
                ["timestamp"] = Encryption.EncryptService(DateTime.Now.ToString()),
                ["aid"] = Encryption.APIService(AuthGG.AID),
                ["username"] = Encryption.APIService(username),
                ["picbytes"] = Encryption.APIService(path),
                ["session_id"] = Constants.IV,
                ["api_id"] = Constants.APIENCRYPTSALT,
                ["api_key"] = Constants.APIENCRYPTKEY,
                ["session_key"] = Constants.Key,
                ["secret"] = Encryption.APIService(AuthGG.Secret),
                ["type"] = Encryption.APIService("uploadpic")
            }))).Split("|".ToCharArray());
            switch (response[0])
            {
                case "success":
                    Utility.MsgShowInfo("Successfully updated profile picture!", AuthGG.AppName);
                    Security.End();
                    return;

                case "permissions":
                    Utility.MsgShowError("Please upgrade your plan to use this feature!", AuthGG.AppName);
                    Security.End();
                    return;

                case "maxsize":
                    if (AuthGG.IsConsole) return;
                    Utility.MsgShowError("Image cannot be greater than 1 MB!", AuthGG.AppName);
                    Security.End();
                    return;

                case "failed":
                    Utility.MsgShowError("Failed to upload profile picture!", AuthGG.AppName);
                    Security.End();
                    return;
            }
        }
        catch (Exception ex)
        {
            Utility.MsgShowError(ex.Message, AppSettings.Name);
            Process.GetCurrentProcess().Kill();
        }
    }


}