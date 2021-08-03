using System;
using Common.Core.DependencyInjection;
using TCPServer;
using Workflow;

namespace QueueSocket
{
    [ServiceLocate(typeof(ISocketService))]
    public class SocketService : ISocketService
    {
        private readonly int ListenPort = 6005;

        private readonly ITCPServer _tcpServer;
        private readonly IQueueService<WorkModel> _queueService;

        public SocketService(
            ITCPServer tcpServer,
            IQueueService<WorkModel> queueService)
        {
            _tcpServer = tcpServer;
            _queueService = queueService;
        }

        public void Start()
        {
            _tcpServer.Start(ListenPort);
            _tcpServer.SetupDataReceiveEventHandler(_queueService.OnDataReceive);
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
