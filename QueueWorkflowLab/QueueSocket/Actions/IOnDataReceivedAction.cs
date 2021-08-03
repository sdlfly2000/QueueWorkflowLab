using TCPServer;

namespace QueueSocket.Actions
{
    public interface IOnDataReceivedAction
    {
        void OnDataReceive(object sender, WorkflowEventArgs e);
    }
}
