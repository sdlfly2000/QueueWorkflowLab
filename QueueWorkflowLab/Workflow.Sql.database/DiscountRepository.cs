using Common.Core.DependencyInjection;

namespace Workflow.Sql.database
{
    [ServiceLocate(typeof(IDiscountRepository))]
    public class DiscountRepository : IDiscountRepository
    {
    }
}
