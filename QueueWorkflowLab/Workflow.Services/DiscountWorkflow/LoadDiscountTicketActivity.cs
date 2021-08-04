using Common.Core.DependencyInjection;
using System;

namespace Workflow.Services.DiscountWorkflow
{
    [ServiceLocate(typeof(ILoadDiscountTicketActivity))]
    public class LoadDiscountTicketActivity : ILoadDiscountTicketActivity
    {
        public LoadDiscountTicketActivity()
        {

        }

        public void Execute(GetDiscountWorkflowContext context)
        {
            throw new NotImplementedException();
        }
    }
}
