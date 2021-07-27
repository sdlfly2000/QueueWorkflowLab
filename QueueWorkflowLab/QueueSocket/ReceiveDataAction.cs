using Common.Core.DependencyInjection;
using Common.Core.TcpServer.AsyncTCPServerContracts;
using System;

namespace QueueSocket
{
    [ServiceLocate(typeof(IReceiveDataAction))]
    public class ReceiveDataAction : IReceiveDataAction
    {
        public void DataReceived(object sender, AsyncEventArgs e)
        {
            Console.WriteLine("Message Received!");
        }
    }
}
