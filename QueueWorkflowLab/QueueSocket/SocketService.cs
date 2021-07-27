using Common.Core.DependencyInjection;
using Common.Core.TcpServer;
using Core;
using System;

namespace QueueSocket
{
    [ServiceLocate(typeof(ISocketService))]
    public class SocketService : ISocketService, IDisposable
    {
        private readonly int ListenPort = 6005;
        private readonly AsyncTCPServer _tcpServer;

        public SocketService(IReceiveDataAction receiveDataAction)
        {
            _tcpServer = Factory.SocketBuilder(ListenPort);
            _tcpServer.DataReceived += receiveDataAction.DataReceived;
        }

        public void Start()
        {
            _tcpServer.Start();
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
