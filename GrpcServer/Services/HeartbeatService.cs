using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcModels.Models;
using GrpcProtos;
using GrpcServer.Database;

namespace GrpcServer.Services
{
    public class HeartbeatService : Heartbeat.HeartbeatBase
    {
        private readonly ILogger<HeartbeatService> _logger;
        private readonly SqlProvider _provider;

        public HeartbeatService(ILogger<HeartbeatService> logger, SqlProvider provider)
        {
            _logger = logger;
            _provider = provider;
        }
        public override Task<ReceiveHeartbeat> Send(GetComputerInformation request, ServerCallContext context)
        {
            var computerInformation = new ComputerInformation()
            {
                TimeStamp = request.Timestamp.ToDateTime().ToLocalTime(),
                CpuTemperatur = request.CpuTemperatur,
                CpuSpeed = request.CpuSpeed,
                GpuTemperatur = request.GpuTemperatur,
                ComputerName = request.ComputerName,
                ProcessorIdentifier = request.ProcessorIdentifier,
                CurrentMemoryUsage = request.CurrentMemoryUsage,
                AvailableMemory = request.AvailableMemory
            };
            _provider.Save(computerInformation);
            _logger.LogInformation("Computer information was saved {computerName}", computerInformation.ComputerName);
            return Task.FromResult(new ReceiveHeartbeat
            {
                Recieved = true,
            });
        }
        public override Task<Empty> ShutdowMessage(SendShutdown request, ServerCallContext context)
        {
            _logger.LogInformation("Message: {message}", request.Shutdownmessage);
            return Task.FromResult(new Empty());
        }
    }
}
