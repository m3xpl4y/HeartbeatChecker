namespace GrpcModels.Models
{
    public class ComputerInformation
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public double CpuTemperatur { get; set; }
        public double CpuSpeed { get; set; }
        public double GpuTemperatur { get; set; }
        public string ComputerName { get; set; } = string.Empty;
        public string ProcessorIdentifier { get; set; } = string.Empty;
        public float CurrentMemoryUsage { get; set; }
        public float AvailableMemory { get; set; }
    }
}
