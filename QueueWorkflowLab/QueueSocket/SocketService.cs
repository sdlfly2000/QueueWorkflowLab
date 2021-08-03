using Common.Core.DependencyInjection;
using System;

namespace QueueSocket
{
    using TCPServer;

    [ServiceLocate(typeof(ISocketService))]
    public class SocketService : ISocketService
    {
        private readonly int ListenPort = 6005;
        private readonly ITCPServer _tcpServer;

        public SocketService(ITCPServer tcpServer)
        {
            _tcpServer = tcpServer;
        }

        public void Start()
        {
            _tcpServer.Start(ListenPort);
        }

        public void Dispose()
        {
            if (_tcpServer != null)
            {
                _tcpServer.Dispose();
                GC.SuppressFinalize(true);
            }
        }
    }
}
