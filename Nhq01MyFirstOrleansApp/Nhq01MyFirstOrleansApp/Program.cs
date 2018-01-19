using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Nhq01MyFirstOrleansApp.GrainInterfaces;
using Orleans;
using Orleans.Runtime.Configuration;

namespace Nhq01MyFirstOrleansApp.SiloHost
{
    /// <summary>
    /// Orleans test silo host
    /// </summary>
    public class Program
    {
        private const int SiloPort = 30000;

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int cmdShow);

        static void Main(string[] args)
        {
            //MaximizeWindow();

            Console.WriteLine("Waiting for Orleans Silo to start. Press Enter to proceed...");
            Console.ReadLine();

            var config = ClientConfiguration.LocalhostSilo(SiloPort);
            GrainClient.Initialize(config);
            //var client = new ClientBuilder().UseConfiguration(config).Build();
            //client.Connect().Wait();

            //DoSomeClientWork(client);

            //Console.WriteLine("Client connected.");

            //Console.WriteLine("\nPress Enter to terminate...");
            //Console.ReadLine();

            //client.Close();
        }

        private static void DoSomeClientWork(IClusterClient client)
        {
            var grain = client.GetGrain<IGrain1>(Guid.Empty);
            Console.WriteLine(grain.SayHello().Result);
        }

        private static void MaximizeWindow()
        {
            Process p = Process.GetCurrentProcess();
            ShowWindow(p.MainWindowHandle, 3); //SW_MAXIMIZE = 3
        }
    }
}
