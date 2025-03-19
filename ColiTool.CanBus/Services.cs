using System;
using System.IO;
using System.Runtime.InteropServices;

namespace ColiTool.CanBus
{
    public class CanBusService
    {
        private static bool IsInitialized = false;

        private static int baudRate = 500000;
        //public static bool SimulateDeviceConnection { get; set; } = false;

        public static void Initialize()
        {
            if (!IsInitialized)
            {
                Console.WriteLine($"Initializing CAN Bus adapter");
                IsInitialized = true;
            }
        }
        public static object GetStatus()
        {
            if (!IsInitialized)
            {
                Initialize();
            }
            return new { Status = IsDeviceConnected() ? "Connected" : "Not Connected" };
        }

        private static bool IsDeviceConnected()
        {
            try
            {   /*
                TAKISTO SIMULACIA PRE MOJE USPOKOJENIE
                if (SimulateDeviceConnection)
                {
                    return true;
                }*/
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    return File.Exists(@"\\.\COM1");
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    return File.Exists("/dev/ttyUSB0");
                }
                else
                {
                    throw new PlatformNotSupportedException();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error checking device connection: {ex.Message}");
                return false;
            }
        }
    }
}
