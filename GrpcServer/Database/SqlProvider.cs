using GrpcModels.Models;
using System.Data.SqlClient;

namespace GrpcServer.Database
{
    public class SqlProvider
    {
        private readonly SqlConnection _connection;

        public SqlProvider(IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionStrings:DefaultConnection"];
            _connection = new SqlConnection(connectionString);
        }

        public void Open()
        {
            _connection.Open();
        }

        public void Close()
        {
            _connection.Close();
        }
        public void Save(object request)
        {
            ComputerInformation? value = request as ComputerInformation;
            Open();
            using (SqlCommand command = new SqlCommand(SqlQueryProvider.SaveComputerInformationQuery(request), _connection))
            {
                command.Parameters.AddWithValue("@timestamp", value!.TimeStamp);
                command.Parameters.AddWithValue("@cpu_temperatur", value.CpuTemperatur);
                command.Parameters.AddWithValue("@cpu_speed", value.CpuSpeed);
                command.Parameters.AddWithValue("@gpu_temperatur", value.GpuTemperatur);
                command.Parameters.AddWithValue("@computer_name", value.ComputerName);
                command.Parameters.AddWithValue("@processor_identifier", value.ProcessorIdentifier);
                command.Parameters.AddWithValue("@current_memory_usage", value.CurrentMemoryUsage);
                command.Parameters.AddWithValue("@available_memory", value.AvailableMemory);

                command.ExecuteNonQuery();
            }
            Close();
        }
    }
}
