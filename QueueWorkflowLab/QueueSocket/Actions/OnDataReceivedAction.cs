using System.Threading;
using Common.Core.Cache.Client.Utils;
using Common.Core.DependencyInjection;
using Microsoft.Extensions.Logging;
using TCPServer;
using Workflow;

namespace QueueSocket.Actions
{
    [ServiceLocate(typeof(IOnDataReceivedAction))]
    public class OnDataReceivedAction : IOnDataReceivedAction
    {
        private readonly IQueueService<GetDiscountWorkflowRequest> _queueService;
        private readonly ILogger<OnDataReceivedAction> _logger;

        public OnDataReceivedAction(
            ILogger<OnDataReceivedAction> logger,
            IQueueService<GetDiscountWorkflowRequest> queueService)
        {
            _logger = logger;
            _queueService = queueService;
        }

        public void OnDataReceive(object sender, WorkflowEventArgs e)
        {
            var workModel = new GetDiscountWorkflowRequest
            {
                WorkName = ConvertTools.BytesToString(e.Payload)
            };

            _queueService.PushToQueue(workModel);

            _logger.LogInformation($"Add {workModel.WorkName}, Total Count in Queue: {_queueService.Queue.Count}");

            ThreadPool.QueueUserWorkItem(_queueService.ConsumeWork);
        }
    }
}
