using Common.Core.DependencyInjection;
using Microsoft.Extensions.Logging;
using Workflow;
using Workflow.Services;

namespace QueueSocket.Actions
{
    [ServiceLocate(typeof(IConsumeWorkAction))]
    public class ConsumeWorkAction : IConsumeWorkAction
    {
        private static object _sync = new object();

        private readonly ILogger<ConsumeWorkAction> _logger;
        private readonly IQueueService<GetDiscountWorkflowRequest> _queueService;
        private readonly IWorkflowService _workflowService;

        public ConsumeWorkAction(
            ILogger<ConsumeWorkAction> logger,
            IQueueService<GetDiscountWorkflowRequest> queueService,
            IWorkflowService workflowService)
        {
            _logger = logger;
            _queueService = queueService;
            _workflowService = workflowService;
        }

        public void Consume(object state)
        {
            if (_queueService.Queue.Count > 0)
            {
                var workContext = _queueService.PopFromQueue();

                if (workContext != default(GetDiscountWorkflowRequest))
                {
                    lock (_sync)
                    {
                        _workflowService.CreateGetDiscountWorkflow(new GetDiscountWorkflowRequest
                        {
                            WorkName = workContext.WorkName
                        });
                        _logger.LogInformation($"Consumed {workContext.WorkName}, Total Count in Queue: {_queueService.Queue.Count}");
                    }
                }
            }
        }
    }
}
