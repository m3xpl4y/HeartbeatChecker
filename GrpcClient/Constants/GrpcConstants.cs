namespace GrpcClient.Constants
{
    public static class GrpcConstants
    {
        public const string TEMPERATURE_SCOPE = "root\\WMI";
        public const string TEMPERATURE_QUERY = "SELECT * FROM MSAcpi_ThermalZoneTemperature";
        public const string CURRENT_TEMPERATURE = "CurrentTemperature";
        public const string CPU_SCOPE = "root\\CIMV2";
        public const string CPU_QUERY = "SELECT * FROM Win32_Processor";
        public const string MAX_CLOCK_SPEED = "CurrentClockSpeed";
        public const string PROCESSOR_IDENTIFIER = "PROCESSOR_IDENTIFIER";
        public const string GPU_FILENAME = "nvidia-smi";
        public const string GPU_ARGUMENTS = "--query-gpu=temperature.gpu --format=csv,noheader";
    }
}
