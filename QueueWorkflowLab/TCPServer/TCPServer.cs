using Common.Core.DependencyInjection;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace TCPServer
{
    [ServiceLocate(typeof(ITCPServer), ServiceType.Singleton)]
    public class TCPServer : ITCPServer
    {       
        private TcpListener _listener;
        private TcpClient _client;

        public TCPServer()
        {
            
        }

        public void Start(int listenPort)
        {
            _listener = TcpListener.Create(listenPort);
            _listener.Start();

            _client = AcceptConnection().Result;
        } 

        private async Task<TcpClient> AcceptConnection()
        {
            return await _listener.AcceptTcpClientAsync();
        }

        public void Stop()
        {
            _listener.Stop();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
