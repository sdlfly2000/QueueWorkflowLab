using System.Collections.Concurrent;
using Common.Core.DependencyInjection;
using Workflow;

namespace QueueSocket
{
    [ServiceLocate(typeof(IQueueService<GetDiscountWorkflowRequest>), ServiceType.Singleton)]
    public class QueueService : IQueueService<GetDiscountWorkflowRequest>
    {
        private static ConcurrentQueue<GetDiscountWorkflowRequest> _queue;

        public QueueService()
        {
            _queue = new ConcurrentQueue<GetDiscountWorkflowRequest>();
        }

        public ConcurrentQueue<GetDiscountWorkflowRequest> Queue
        {
            get => _queue;
        }
        
        public GetDiscountWorkflowRequest PopFromQueue()
        {
            if (_queue.TryDequeue(out var model))
            {
                return model;
            }

            return default(GetDiscountWorkflowRequest);
        }

        public void PushToQueue(GetDiscountWorkflowRequest model)
        {
            _queue.Enqueue(model);
        }

        public void ClearQueue()
        {
            _queue.Clear();
        }
    }
}
