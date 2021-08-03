using TCPServer;

namespace QueueSocket.Actions
{
    internal interface IOnDataReceivedAction
    {
        void OnDataReceive(object sender, WorkflowEventArgs e);
    }
}
