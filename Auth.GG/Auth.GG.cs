global using System;
global using System.Collections.Generic;
global using System.Collections.Specialized;
global using System.Diagnostics;
global using System.IO;
global using System.Linq;
global using System.Net;
global using System.Net.NetworkInformation;
global using System.Net.Security;
global using System.Security.Cryptography;
global using System.Security.Cryptography.X509Certificates;
global using System.Security.Principal;
global using System.Text;
global using System.Text.RegularExpressions;
global using System.Threading;
global using System.Windows;
using System.Globalization;
using System.Xml;

namespace Auth.GG;

public class AuthGG
{
    internal static string Name { get; set; }
    internal static string AID { get; set; }
    internal static string Secret { get; set; }
    internal static string Version { get; set; }
    internal static bool IsConsole { get; set; }
    internal AuthGG()
    {

    }
    public AuthGG(string name, string aid, string secret, string version, bool isConsole = false)
    {
        if (Utility.IsEmpty(new List<string> { name, aid, secret, version }))
        {
            Utility.MsgShowError("Failed to initialize your application correctly in Program.cs!", Name);
            Process.GetCurrentProcess().Kill();
            return;
        }

        Name = name;
        AID = aid;
        Secret = secret;
        Version = version;
        IsConsole = isConsole;
        Initialize();
    }

    private void Initialize()
    {
        using WebClient wc = new();
        try
        {
            wc.Proxy = null;
            Security.Start();
            var response = (Encryption.DecryptService(Encoding.Default.GetString(wc.UploadValues(Constants.ApiUrl,
                new NameValueCollection
                {
                    ["token"] = Encryption.EncryptService(Constants.Token),
                    ["timestamp"] = Encryption.EncryptService(DateTime.Now.ToString(CultureInfo.InvariantCulture)),
                    ["aid"] = Encryption.APIService(AID),
                    ["session_id"] = Constants.IV,
                    ["api_id"] = Constants.APIENCRYPTSALT,
                    ["api_key"] = Constants.APIENCRYPTKEY,
                    ["session_key"] = Constants.Key,
                    ["secret"] = Encryption.APIService(Secret),
                    ["type"] = Encryption.APIService("start")
                }))).Split("|".ToCharArray()));

            if (Security.MaliciousCheck(response[1]))
            {
                Utility.MsgShowWarning("Possible malicious activity detected!", Name);
                Process.GetCurrentProcess().Kill();
            }

            if (Constants.Breached)
            {
                Utility.MsgShowWarning("Possible malicious activity detected!", Name);
                Process.GetCurrentProcess().Kill();
            }

            if (response[0] != Constants.Token)
            {
                Utility.MsgShowError("Security error has been triggered!", Name);
                Process.GetCurrentProcess().Kill();
            }

            switch (response[2])
            {
                case "success":
                    Constants.Initialized = true;
                    if (response[3] == "Enabled")
                        AppSettings.Status = true;
                    if (response[4] == "Enabled")
                        AppSettings.DeveloperMode = true;
                    AppSettings.Hash = response[5];
                    AppSettings.Version = response[6];
                    AppSettings.Update_Link = response[7];
                    if (response[8] == "Enabled")
                        AppSettings.FreeMode = true;
                    if (response[9] == "Enabled")
                        AppSettings.Login = true;
                    AppSettings.Name = response[10];
                    if (response[11] == "Enabled")
                        AppSettings.Register = true;
                    AppSettings.TotalUsers = response[13];
                    if (AppSettings.DeveloperMode)
                    {
                        Utility.MsgShowWarning("Application is in Developer Mode, bypassing integrity and update check!", Name);
                        File.Create(Environment.CurrentDirectory + "/integrity.log").Close();
                        var processModule = Process.GetCurrentProcess().MainModule;
                        if (processModule is { FileName: { } })
                        {
                            var hash = Security.Integrity(processModule.FileName);
                            File.WriteAllText(Environment.CurrentDirectory + "/integrity.log", hash);
                        }

                        Utility.MsgShowInfo("Your applications hash has been saved to integrity.txt, please refer to this when your application is ready for release!", Name);
                    }
                    else
                    {
                        if (AppSettings.Version != Version)
                        {
                            Utility.MsgShowError($"Update {AppSettings.Version} available, redirecting to update!", Name);
                            Process.Start(AppSettings.Update_Link);
                            Process.GetCurrentProcess().Kill();
                        }

                        if (response[12] == "Enabled")
                        {
                            var processModule = Process.GetCurrentProcess().MainModule;
                            if (processModule is { FileName: { } })
                            {
                                if (AppSettings.Hash != Security.Integrity(processModule.FileName))
                                {
                                    Utility.MsgShowError($"File has been tampered with, couldn't verify integrity!", Name);
                                    Process.GetCurrentProcess().Kill();
                                }
                            }
                        }
                    }

                    if (AppSettings.Status == false)
                    {
                        Utility.MsgShowError("Looks like this application is disabled, please try again later!", Name);
                        Process.GetCurrentProcess().Kill();
                    }

                    break;

                case "binderror":
                    Utility.MsgShowError(Encryption.Decode("RmFpbGVkIHRvIGJpbmQgdG8gc2VydmVyLCBjaGVjayB5b3VyIEFJRCAmIFNlY3JldCBpbiB5b3VyIGNvZGUh"), Name);
                    Process.GetCurrentProcess().Kill();
                    return;

                case "banned":
                    Utility.MsgShowError("This application has been banned for violating the TOS" + Environment.NewLine +
                                         "Contact us at support@auth.gg", Name);
                    Process.GetCurrentProcess().Kill();
                    return;
            }

            Security.End();
        }
        catch (Exception ex)
        {
            Utility.MsgShowError(ex.Message, Name);
            Process.GetCurrentProcess().Kill();
        }
    }


}