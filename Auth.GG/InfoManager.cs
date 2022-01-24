using Timer = System.Threading.Timer;

namespace Auth.GG;

internal class InfoManager
{
    private Timer _timer;
    private string _lastGateway;

    internal InfoManager()
    {
        _lastGateway = GetGatewayMAC();
    }

    internal void StartListener()
    {
        _timer = new Timer(_ => OnCallBack(), null, 5000, Timeout.Infinite);
    }

    private void OnCallBack()
    {
        _timer.Dispose();
        if (GetGatewayMAC() != _lastGateway)
        {
            Constants.Breached = true;
            Utility.MsgShowError("ARP Cache poisoning has been detected!", AuthGG.Name);
            Process.GetCurrentProcess().Kill();
        }
        else
        {
            _lastGateway = GetGatewayMAC();
        }
        _timer = new Timer(_ => OnCallBack(), null, 5000, Timeout.Infinite);
    }

    private static IPAddress GetDefaultGateway()
    {
        return NetworkInterface
            .GetAllNetworkInterfaces()
            .Where(n => n.OperationalStatus == OperationalStatus.Up)
            .Where(n => n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
            .SelectMany(n => n.GetIPProperties()?.GatewayAddresses)
            .Select(g => g.Address)
            .Where(a => a != null)
            .FirstOrDefault();
    }

    private static string GetArpTable()
    {
        var drive = Path.GetPathRoot(Environment.SystemDirectory);

        var start = new ProcessStartInfo
        {
            FileName = $@"{drive}Windows\System32\arp.exe",
            Arguments = "-a",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = !AuthGG.IsConsole
        };

        using var process = Process.Start(start);
        if (process == null) return "";
        using var reader = process.StandardOutput;
        return reader.ReadToEnd();
    }

    private static string GetGatewayMAC()
    {
        var routerIP = GetDefaultGateway().ToString();
        var regex = new Regex($@"({routerIP} [\W]*) ([a-z0-9-]*)");
        var matches = regex.Match(GetArpTable());
        return matches.Groups[2].ToString();
    }
}