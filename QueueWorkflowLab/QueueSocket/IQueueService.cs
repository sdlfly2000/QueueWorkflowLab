using System.Collections.Concurrent;
using Workflow;

namespace QueueSocket
{
    public interface IQueueService<T>
    {
        void PushToQueue(T number);

        T PopFromQueue();

        void ConsumeWork(object state);

        ConcurrentQueue<WorkModel> Queue { get; }
    }
}
