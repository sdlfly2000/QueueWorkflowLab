using System.Collections.Concurrent;
using Workflow;

namespace QueueSocket
{
    public interface IQueueService<T>
    {
        void PushToQueue(T number);

        T PopFromQueue();

        ConcurrentQueue<GetDiscountWorkflowRequest> Queue { get; }
    }
}
