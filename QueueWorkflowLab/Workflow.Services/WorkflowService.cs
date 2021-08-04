using Common.Core.DependencyInjection;
using Workflow.Services.DiscountWorkflow;

namespace Workflow.Services
{
    [ServiceLocate(typeof(IWorkflowService))]
    public class WorkflowService : IWorkflowService
    {
        private readonly IGetDiscountWorkflow _getDiscountWorkflow;

        public WorkflowService(IGetDiscountWorkflow getDiscountWorkflow)
        {
            _getDiscountWorkflow = getDiscountWorkflow;
        }

        public void CreateGetDiscountWorkflow(GetDiscountWorkflowRequest request)
        {
            var context = new GetDiscountWorkflowContext
            {
                Name = request.WorkName
            };

            _getDiscountWorkflow.Execute(context);
        }
    }
}
