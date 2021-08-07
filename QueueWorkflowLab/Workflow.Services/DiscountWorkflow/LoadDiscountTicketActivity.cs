using Common.Core.DependencyInjection;
using Workflow.Sql.database;

namespace Workflow.Services.DiscountWorkflow
{
    [ServiceLocate(typeof(ILoadDiscountTicketActivity))]
    public class LoadDiscountTicketActivity : ILoadDiscountTicketActivity
    {
        private readonly IUnitOfWork _uow;

        public LoadDiscountTicketActivity(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void Execute(GetDiscountWorkflowContext context)
        {
            var discount = _uow.LoadAvailable();
            context.DiscountId = discount != null 
                ? discount.Id 
                : string.Empty;
        }
    }
}
