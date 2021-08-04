using System.Collections.Concurrent;
using Common.Core.DependencyInjection;
using Microsoft.Extensions.Logging;
using Workflow;

namespace QueueSocket
{
    [ServiceLocate(typeof(IQueueService<GetDiscountWorkflowRequest>), ServiceType.Singleton)]
    public class QueueService : IQueueService<GetDiscountWorkflowRequest>
    {
        private static ConcurrentQueue<GetDiscountWorkflowRequest> _queue;
        private static ILogger<QueueService> _logger;

        public QueueService(ILogger<QueueService> logger)
        {
            _queue = new ConcurrentQueue<GetDiscountWorkflowRequest>();
            _logger = logger;
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

        public void ConsumeWork(object state)
        {
            if (_queue.Count > 0)
            {
                var workContext = PopFromQueue();

                if (workContext != default(GetDiscountWorkflowRequest))
                {
                    _logger.LogInformation($"Consumed {workContext.WorkName}, Total Count in Queue: {_queue.Count}");
                }
            }
        }

        public void ClearQueue()
        {
            _queue.Clear();
        }
    }
}
