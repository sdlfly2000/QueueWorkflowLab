using Common.Core.DependencyInjection;
using System;

namespace Workflow.Services.DiscountWorkflow
{
    [ServiceLocate(typeof(ISaveDiscountActivity))]
    public class SaveDiscountActivity : ISaveDiscountActivity
    {
        public SaveDiscountActivity()
        {

        }

        public void Execute(GetDiscountWorkflowContext context)
        {
            throw new NotImplementedException();
        }
    }
}
