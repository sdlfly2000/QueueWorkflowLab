using System.Collections.Concurrent;
using Common.Core.Cache.Client.Utils;
using Common.Core.DependencyInjection;
using TCPServer;
using Workflow;
using Microsoft.Extensions.Logging;

namespace QueueSocket
{
    using System.Linq;

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

        public WorkModel PopFromQueue()
        {
            if (_queue.TryDequeue(out var model))
            {
                return model;
            }

            return default(WorkModel);
        }

        public void OnDataReceive(object sender, WorkflowEventArgs e)
        {
            var workModel = new WorkModel
            {
                WorkName = ConvertTools.BytesToString(e.Payload)
            };

            PushToQueue(workModel);

            _logger.LogInformation($"Total Count in Queue: {_queue.Count()}");
        }

        public void PushToQueue(WorkModel model)
        {
            _queue.Enqueue(model);
        }

        public void ClearQueue()
        {
            _queue.Clear();
        }
    }
}
