using Common.Core.DependencyInjection;
using Workflow.Core;

namespace Workflow.Services.DiscountWorkflow
{
    [ServiceLocate(typeof(IGetDiscountWorkflow))]
    public class GetDiscountWorkflow : SequencialActivity<GetDiscountWorkflowContext>, IGetDiscountWorkflow
    {
        public GetDiscountWorkflow(
            ILoadDiscountTicketActivity loadDiscountTicketActivity,
            ISaveDiscountActivity saveDiscountActivity)
            : base(loadDiscountTicketActivity, saveDiscountActivity)
        {
        }
    }
}
