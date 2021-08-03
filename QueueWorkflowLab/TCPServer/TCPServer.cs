using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using Common.Core.Cache.Client.Utils;
using Common.Core.DependencyInjection;
using Microsoft.Extensions.Logging;
using Workflow;

namespace TCPServer
{
    [ServiceLocate(typeof(ITCPServer), ServiceType.Singleton)]
    public class TCPServer : ITCPServer
    {
        private volatile TcpListener _listener;

        private readonly ILogger<TCPServer> _logger;

        public TCPServer(ILogger<TCPServer> logger)
        {
            _logger = logger;
        }

        public void Start(int listenPort)
        {
            _listener = TcpListener.Create(listenPort);
            _listener.Start();
            _logger.LogInformation($"Start to listen on port: {listenPort}");
            Task.Run(StartMonitor);
        }

        private void StartMonitor()
        {
            try
            {
                _logger.LogInformation($"Pending on accept client.");
                var client = _listener.AcceptTcpClient();
                
                HandleAcceptedClient(client);

                client.Client.Send(new byte[] { 0x10 });
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            Task.Run(StartMonitor);
        }

        private void HandleAcceptedClient(TcpClient client)
        {
            var dataStream = client.GetStream();
            var dataSize = client.Available;
            var buffer = new byte[dataSize];

            if (dataSize > 0)
            { 
                var rev = dataStream.Read(buffer, 0, dataSize);
                var model = ParseModel(buffer, rev);
                _logger.LogInformation($"Data Received.");
            }
            else
            {
                _logger.LogInformation($"Data not Available.");
            }
        }

        private WorkModel ParseModel(byte[] buffer, int recvCount)
        {
            if (recvCount <= 0)
            {
                return new WorkModel();
            }

            var workNameBuffer = new Span<byte>(buffer, 0, recvCount);

            return new WorkModel
            {
                WorkName = ConvertTools.BytesToString(workNameBuffer.ToArray())
            };
        }

        public void Stop()
        {
            if (_listener != null)
            {
                _logger.LogInformation($"Listener stops.");
                _listener.Stop();
            }
        }

        public virtual void Dispose()
        {
            Stop();
            _listener = null;
        }
    }
}
