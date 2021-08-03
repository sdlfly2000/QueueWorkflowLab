using System.Collections.Concurrent;
using Common.Core.DependencyInjection;
using Microsoft.Extensions.Logging;
using Workflow;

namespace QueueSocket
{
    [ServiceLocate(typeof(IQueueService<WorkModel>), ServiceType.Singleton)]
    public class QueueService : IQueueService<WorkModel>
    {
        private static ConcurrentQueue<WorkModel> _queue;
        private static ILogger<QueueService> _logger;

        public QueueService(ILogger<QueueService> logger)
        {
            _queue = new ConcurrentQueue<WorkModel>();
            _logger = logger;
        }

        public ConcurrentQueue<WorkModel> Queue
        {
            get => _queue;
        }
        
        public WorkModel PopFromQueue()
        {
            if (_queue.TryDequeue(out var model))
            {
                return model;
            }

            return default(WorkModel);
        }

        public void PushToQueue(WorkModel model)
        {
            _queue.Enqueue(model);
        }

        public void ConsumeWork(object state)
        {
            if (_queue.Count > 0)
            {
                var workContext = PopFromQueue();

                if (workContext != default(WorkModel))
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
