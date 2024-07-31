using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using GrpcClient.Services;
using GrpcModels.Entities;
using GrpcProtos;
using Microsoft.Extensions.Configuration;

namespace GrpcClient
{
    public static class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = BuildConfiguration();
            var hostSettings = RetrieveHostSettings(configuration);

            var channel = GrpcChannel.ForAddress(hostSettings.ForAddress);

            if (channel.State == Grpc.Core.ConnectivityState.Idle)
            {
                Console.WriteLine($"Connected to {channel.Target}");
            }

            var service = new Heartbeat.HeartbeatClient(channel);

            while (channel.State != Grpc.Core.ConnectivityState.Shutdown)
            {
                
                await Task.Delay(5000);
                DateTime dateTime = DateTime.UtcNow;
                Timestamp timestamp = Timestamp.FromDateTimeOffset(dateTime);

                var memory = Temperature.GetMemoryLoad();

                var computerInformation = new GetComputerInformation()
                {
                    Timestamp = timestamp,
                    CpuTemperatur = Temperature.GetCpuTemperature(),
                    CpuSpeed = Temperature.GetCpuLoad(),
                    GpuTemperatur = Temperature.GetGpuTemperatur(),
                    ComputerName = Environment.MachineName,
                    ProcessorIdentifier = Temperature.GetCpuName(),
                    CurrentMemoryUsage = memory.MemortyUsage,
                    AvailableMemory = memory.AvailableMemoryMBytes
                };
                var heartbeat = await service.SendAsync(computerInformation);

                if (heartbeat.Recieved)
                {
                    await Console.Out.WriteLineAsync($"Heartbeat received {timestamp} : {heartbeat.Recieved}");
                }
            }
        }


        static IConfiguration BuildConfiguration()
        {
            return new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build();
        }
        static HostSettings RetrieveHostSettings(IConfiguration configuration)
        {
            var host = configuration["Setting:Host"];
            var port = configuration["Setting:Port"];
            return new HostSettings(host, port);
        }
    }
}