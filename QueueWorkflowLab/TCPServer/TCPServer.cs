using Common.Core.DependencyInjection;
using System;
using System.Net.Sockets;
using System.Threading;

namespace TCPServer
{
    [ServiceLocate(typeof(ITCPServer), ServiceType.Singleton)]
    public class TCPServer : ITCPServer
    {       
        private TcpListener _listener;
        private TcpClient _client;

        private bool _state = false;

        public TCPServer()
        {
        }

        public void Start(int listenPort)
        {
            _listener = TcpListener.Create(listenPort);
            _listener.Start();

            ThreadPool.QueueUserWorkItem(
                AcceptConnection,
                _state,
                true);
        }

        private void AcceptConnection(bool state)
        {
            var client = _listener.AcceptTcpClient();
            var dataStream = client.GetStream();
            var buffer = new byte[5];
            if (dataStream.DataAvailable)
            {
                var rev = dataStream.Read(buffer, 0,2);
                Console.WriteLine($"Data Received: {rev}");
            }
            client.Close();
            ThreadPool.QueueUserWorkItem(
                AcceptConnection,
                _state,
                true);
        }

        public void Stop()
        {
            _listener.Stop();
        }

        public void Dispose()
        {
            _listener.Stop();
        }
    }
}
