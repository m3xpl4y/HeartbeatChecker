using GrpcClient.Constants;
using GrpcModels.Entities;
using System.Diagnostics;
using System.Globalization;
using System.Management;

namespace GrpcClient.Services
{
    public static class Temperature
    {
        public static string GetCpuName()
        {
            var cpuName = Environment.GetEnvironmentVariable(GrpcConstants.PROCESSOR_IDENTIFIER);
            if (string.IsNullOrEmpty(cpuName))
            {
                return string.Empty;
            }
            return cpuName;
        }
        public static double GetGpuTemperatur()
        {
            CultureInfo culture = new CultureInfo("en-US");
            CultureInfo.CurrentCulture = culture;
            var process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();

            startInfo.FileName = GrpcConstants.GPU_FILENAME;
            startInfo.Arguments = GrpcConstants.GPU_ARGUMENTS;
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            process.StartInfo = startInfo;
            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            return double.Parse(output.Trim());
        }
        public static MemoryLoad GetMemoryLoad()
        {
            CultureInfo culture = new CultureInfo("en-US");
            CultureInfo.CurrentCulture = culture;
            PerformanceCounter cpuCounter;
            cpuCounter = new PerformanceCounter("Memory", "Available MBytes");
            var mb = GC.GetGCMemoryInfo();
            var mmb = mb.MemoryLoadBytes / 1024;
            var mmbb = (float)mmb / 1024;
            return new MemoryLoad()
            {
                AvailableMemoryMBytes = cpuCounter.NextValue(),
                MemortyUsage = mmbb / 1024
            };
        }
        public static double GetCpuLoad()
        {
            uint cpuSpeed = 0;
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(GrpcConstants.CPU_SCOPE, GrpcConstants.CPU_QUERY);
                foreach (ManagementObject obj in searcher.Get())
                {
                    cpuSpeed = (uint)obj[GrpcConstants.MAX_CLOCK_SPEED];
                }
            }
            catch (ManagementException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return cpuSpeed;
        }
        public static double GetCpuTemperature()
        {
            double temperature = 0;
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(GrpcConstants.TEMPERATURE_SCOPE, GrpcConstants.TEMPERATURE_QUERY);
                CultureInfo culture = new CultureInfo("en-US"); 
                CultureInfo.CurrentCulture = culture;
                foreach (ManagementObject obj in searcher.Get())
                {
                    temperature = Convert.ToDouble(obj[GrpcConstants.CURRENT_TEMPERATURE].ToString());
                    temperature = (temperature - 2732) / 10.0;
                }
            }
            catch (ManagementException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return temperature;
        }
    }
}
