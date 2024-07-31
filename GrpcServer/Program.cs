using GrpcServer.Database;
using GrpcServer.Services;
namespace GrpcServer
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddGrpc();

            builder.Services.AddControllers();
            builder.Services.AddScoped<SqlProvider>();

            builder.Configuration.AddJsonFile("appsettings.json", true, true);

            var app = builder.Build();

            app.MapGrpcService<HeartbeatService>();
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
            app.Run();
        }
    }
}