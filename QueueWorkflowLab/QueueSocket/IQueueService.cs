using TCPServer;

namespace QueueSocket
{
    public interface IQueueService<T>
    {
        void PushToQueue(T number);

        T PopFromQueue();

        void OnDataReceive(object sender, WorkflowEventArgs e);

        void ConsumeWork(object state);
    }
}
