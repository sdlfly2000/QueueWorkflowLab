using System;
using Common.Core.DependencyInjection;
using QueueSocket.Actions;
using TCPServer;

namespace QueueSocket
{
    [ServiceLocate(typeof(ISocketService))]
    public class SocketService : ISocketService
    {
        private readonly int ListenPort = 6005;

        private readonly ITCPServer _tcpServer;
        private readonly IOnDataReceivedAction _onDataReceivedAction;

        public SocketService(
            ITCPServer tcpServer,
            IOnDataReceivedAction onDataReceivedAction)
        {
            _tcpServer = tcpServer;
            _onDataReceivedAction = onDataReceivedAction;
        }

        public void Start()
        {
            _tcpServer.Start(ListenPort);
            _tcpServer.SetupDataReceiveEventHandler(_onDataReceivedAction.OnDataReceive);
        }

        public void Stop()
        {
            _tcpServer.Stop();
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
