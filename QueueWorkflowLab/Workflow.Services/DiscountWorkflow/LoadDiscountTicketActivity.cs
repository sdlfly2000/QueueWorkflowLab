using Common.Core.DependencyInjection;
using Workflow.Sql.database;

namespace Workflow.Services.DiscountWorkflow
{
    [ServiceLocate(typeof(ILoadDiscountTicketActivity))]
    public class LoadDiscountTicketActivity : ILoadDiscountTicketActivity
    {
        private readonly IDiscountRepository _discountRepository;

        public LoadDiscountTicketActivity(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        public void Execute(GetDiscountWorkflowContext context)
        {
            var discount = _discountRepository.LoadAvailable();
            context.DiscountId = discount.Id;
        }
    }
}
