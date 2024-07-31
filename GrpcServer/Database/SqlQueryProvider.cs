using GrpcModels.Models;
using System.Globalization;
using System.Text;

namespace GrpcServer.Database
{
    public static class SqlQueryProvider
    {
        public static string SaveComputerInformationQuery(object request)
        {
            ComputerInformation? value = request as ComputerInformation;
            if(value == null )
            {
                return string.Empty;
            }
            CultureInfo culture = new CultureInfo("en-US");
            CultureInfo.CurrentCulture = culture;
            var query = new StringBuilder();
            query.Append("INSERT INTO ");
            query.Append("computerinformations(timestamp, cpu_temperatur, cpu_speed, gpu_temperatur, computer_name, processor_identifier, current_memoryusage, available_memory)");
            query.Append("VALUES");
            query.Append("(");
            query.Append("'" + value.TimeStamp + "'");
            query.Append(", ");
            query.Append(value.CpuTemperatur);
            query.Append(", ");
            query.Append(value.CpuSpeed);
            query.Append(", ");
            query.Append(value.GpuTemperatur);
            query.Append(", ");
            query.Append("'" + value.ComputerName + "'");
            query.Append(", ");
            query.Append("'" + value.ProcessorIdentifier + "'");
            query.Append(", ");
            query.Append(value.CurrentMemoryUsage);
            query.Append(", ");
            query.Append(value.AvailableMemory);
            query.Append(");");
            return query.ToString();
        }
    }
}
