using Common.Core.DependencyInjection;
using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace TCPServer
{
    [ServiceLocate(typeof(ITCPServer), ServiceType.Singleton)]
    public class TCPServer : ITCPServer, IDisposable
    {
        private object _syncListner = new object();
        private volatile TcpListener _listener;

        public TCPServer()
        {
        }

        public void Start(int listenPort)
        {
            _listener = TcpListener.Create(listenPort);
            _listener.Start();
            StartMonitor();
            //Task.Run(StartMonitor);
        }

        private void StartMonitor()
        {
            Console.WriteLine("--------------------");
            Console.WriteLine($"Thread: {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"Listener: {_listener.GetHashCode()}");

            while(true)
            {
                var client = _listener.AcceptTcpClient();
                Console.WriteLine($"client: {client.GetHashCode()}");
                HandleAcceptedClient(client);
                client.Close();                
            }
        }

        private void HandleAcceptedClient(TcpClient client)
        {
            var dataStream = client.GetStream();
            var dataSize = client.Available;
            var buffer = new byte[dataSize];

            if (dataSize > 0)
            {
                var rev = dataStream.Read(buffer, 0, dataSize);
                Console.WriteLine($"Data Number: {rev}");
                Console.WriteLine($"Data Received: {buffer[0]}");                
            }
            else
            {
                Console.WriteLine("Data not Available");
            }
        }

        public void Stop()
        {            
            _listener.Stop();
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
