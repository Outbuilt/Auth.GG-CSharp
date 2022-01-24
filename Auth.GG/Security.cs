namespace Auth.GG;

internal class Security
{
    private const string Key = "3082010A0282010100CEB689728FB489BA9512B64F5A6AC786FCCEB9518720A4AD3AA9538A45984B500A8EFFD8A878684D857E5876C8D94CF30414E44D7445025D5A388D1FD5EF91352E3FEB7EC7C0D53FE86D3C49DC17426F217B7B2C1E029B9D60580CF041B3C8632A8D62F5998AF93C0C7E357C266256ACB15969523CCE326B49A1E3371571C0DCFCF41D36F4C66555D674884F4B41673E105E1C1A44266D0225F2A0B1D39D2D99860432DE4972E8CDF4F3BBC92C091791811E513291415949E169747EB7E85D229DFD6FDC6EDC6CE35D62A2CBDBB473B0E112A110479ADCC4EFAF33DEEB6A58BC0E14E74BBDF8C83EEC426C387160A673A2318722096B050F1293933443420D630203010001";

    internal static void Start()
    {
        var drive = Path.GetPathRoot(Environment.SystemDirectory);
        if (Constants.Started)
        {
            Utility.MsgShowWarning("A session has already been started, please end the previous one!", AuthGG.AppName);
            Process.GetCurrentProcess().Kill();
        }
        else
        {
            using (var sr = new StreamReader($@"{drive}Windows\System32\drivers\etc\hosts"))
            {
                var contents = sr.ReadToEnd();
                if (contents.Contains("api.auth.gg"))
                {
                    Constants.Breached = true;
                    Utility.MsgShowError("DNS redirecting has been detected!", AuthGG.AppName);
                    Process.GetCurrentProcess().Kill();
                }
            }
            var infoManager = new InfoManager();
            infoManager.StartListener();
            Constants.Token = Guid.NewGuid().ToString();
            ServicePointManager.ServerCertificateValidationCallback += PinPublicKey;
            Constants.APIENCRYPTKEY = Convert.ToBase64String(Encoding.Default.GetBytes(Session(32)));
            Constants.APIENCRYPTSALT = Convert.ToBase64String(Encoding.Default.GetBytes(Session(16)));
            Constants.IV = Convert.ToBase64String(Encoding.Default.GetBytes(Constants.RandomString(16)));
            Constants.Key = Convert.ToBase64String(Encoding.Default.GetBytes(Constants.RandomString(32)));
            Constants.Started = true;
        }
    }

    private static bool PinPublicKey(object sender, X509Certificate certificate, X509Chain? chain, SslPolicyErrors sslPolicyErrors)
    {
        return certificate != null && certificate.GetPublicKeyString() == Key;
    }

    private static string Session(int length)
    {
        var random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
    }

    internal static void End()
    {
        if (!Constants.Started)
        {
            Utility.MsgShowWarning("No session has been started, closing for security reasons!", AuthGG.AppName);
            Process.GetCurrentProcess().Kill();
        }
        else
        {
            Constants.Token = null;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            Constants.APIENCRYPTKEY = null;
            Constants.APIENCRYPTSALT = null;
            Constants.IV = null;
            Constants.Key = null;
            Constants.Started = false;
        }
    }

    internal static string Integrity(string filename)
    {
        using var md = MD5.Create();
        using var fileStream = File.OpenRead(filename);
        var value = md.ComputeHash(fileStream);
        return BitConverter.ToString(value).Replace("-", "").ToLowerInvariant();
    }

    internal static bool MaliciousCheck(string date)
    {
        var dt1 = DateTime.Parse(date); //time sent
        var dt2 = DateTime.Now; //time received
        var d3 = dt1 - dt2;
        if (Convert.ToInt32(d3.Seconds.ToString().Replace("-", "")) >= 5 ||
            Convert.ToInt32(d3.Minutes.ToString().Replace("-", "")) >= 1)
        {
            Constants.Breached = true;
            return true;
        }
        return false;
    }

    internal static string Signature(string value)
    {
        using var md5 = MD5.Create();
        var input = Encoding.UTF8.GetBytes(value);
        var hash = md5.ComputeHash(input);
        return BitConverter.ToString(hash).Replace("-", "");
    }

    internal static string Obfuscate(int length)
    {
        var random = new Random();
        const string chars = "gd8JQ57nxXzLLMPrLylVhxoGnWGCFjO4knKTfRE6mVvdjug2NF/4aptAsZcdIGbAPmcx0O+ftU/KvMIjcfUnH3j+IMdhAW5OpoX3MrjQdf5AAP97tTB5g1wdDSAqKpq9gw06t3VaqMWZHKtPSuAXy0kkZRsc+DicpcY8E9+vWMHXa3jMdbPx4YES0p66GzhqLd/heA2zMvX8iWv4wK7S3QKIW/a9dD4ALZJpmcr9OOE=";
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
    }
}