namespace GrpcModels.Entities
{
    public class HostSettings
    {
        private readonly string _host;
        private readonly int _port;

        public HostSettings(string host, int port)
        {
            _host = host;
            _port = port;
            ForAddress = _host + _port;
        }

        public HostSettings(string host, string port)
        {
            _host = host;
            _port = Int32.Parse(port);
            ForAddress = _host + _port;
        }
        public string ForAddress { get; init; }
    }
}
