using Common.Core.TcpServer;
using Common.Core.TcpServer.AsyncTCPServerContracts;

namespace QueueSocket
{
    public interface IReceiveDataAction
    {
        void DataReceived(object? sender, AsyncEventArgs e);
    }
}
