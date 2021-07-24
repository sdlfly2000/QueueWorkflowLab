using Common.Core.TcpServer;

namespace Core
{
    public static class Factory
    {
        public static AsyncTCPServer SocketBuilder(int listenPort)
        {
            return new AsyncTCPServer(listenPort);
        }
    }
}
