using Common.Core.DependencyInjection;
using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Workflow;

namespace TCPServer
{
    using System.Linq;
    using System.Text;

    using Common.Core.Cache.Client.Utils;

    using Microsoft.Extensions.Primitives;

    [ServiceLocate(typeof(ITCPServer), ServiceType.Singleton)]
    public class TCPServer : ITCPServer
    {
        private volatile TcpListener _listener;

        public TCPServer()
        {
        }

        public void Start(int listenPort)
        {
            _listener = TcpListener.Create(listenPort);
            _listener.Start();
            Task.Run(StartMonitor);
        }

        private void StartMonitor()
        {
            try
            {
                var client = _listener.AcceptTcpClient();
                HandleAcceptedClient(client);
                client.Client.Send(new byte[] { 0x10 });
            }
            catch (Exception e)
            {
                Console.WriteLine($"Stop with Error Message: {e.Message}");
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

                Console.WriteLine($"Data Received: {model.WorkName}");                
            }
            else
            {
                Console.WriteLine("Data not Available");
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
