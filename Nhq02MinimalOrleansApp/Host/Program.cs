using Orleans.Runtime.Host;
using Orleans;
using System.Net;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using GrainInterfaces;

class Program
{
    [DllImport("user32.dll")]
    public static extern bool ShowWindow(System.IntPtr hWnd, int cmdShow);

    static SiloHost _siloHost;

    static void Main(string[] args)
    {
        Maximize();

        // Orleans should run in its own AppDomain, we set it up like this
        var hostDomain = AppDomain.CreateDomain(
            "OrleansHost",
            null,
            new AppDomainSetup { AppDomainInitializer = InitSilo }
        );

        DoSomeClientWork();

        Console.WriteLine("Orleans Silo is running.\nPress Enter to terminate...");
        Console.ReadLine();

        // We do a clean shutdown in the other AppDomain
        hostDomain.DoCallBack(ShutdownSilo);
    }


    static void DoSomeClientWork()
    {
        // Orleans comes with a rich XML and programmatic configuration. Here we're just going to set up with basic programmatic config
        var config = Orleans.Runtime.Configuration.ClientConfiguration.LocalhostSilo(30000);
        GrainClient.Initialize(config);

        var friend = GrainClient.GrainFactory.GetGrain<IHello>(0);
        var result = friend.SayHello("Goodbye").Result;
        Console.WriteLine(result);

    }

    static void InitSilo(string[] args)
    {
        // The Cluster config is quirky and weird to configure in code, so we're going to use a config file
        _siloHost = new SiloHost(Dns.GetHostName()) { ConfigFileName = "OrleansConfiguration.xml" };

        _siloHost.InitializeOrleansSilo();
        var startedok = _siloHost.StartOrleansSilo();
        if (!startedok)
        {
            throw new SystemException($"Failed to start Orleans silo '{_siloHost.Name}' as a {_siloHost.Type} node");
        }
    }

    static void ShutdownSilo()
    {
        if (_siloHost != null)
        {
            _siloHost.Dispose();
            GC.SuppressFinalize(_siloHost);
            _siloHost = null;
        }
    }

    private static void Maximize()
    {
        Process p = Process.GetCurrentProcess();
        ShowWindow(p.MainWindowHandle, 3); //SW_MAXIMIZE = 3
    }
}
