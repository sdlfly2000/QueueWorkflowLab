using System.Linq;
using Common.Core.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Workflow.Sql.database
{
    [ServiceLocate(typeof(IDiscountRepository))]
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IWorkflowDbContext _context;

        public DiscountRepository(IServiceScopeFactory serviceScopeFactory)
        {
            _context = serviceScopeFactory
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<IWorkflowDbContext>();
        }

        public DiscountEntity Load(string id)
        {
            return All().FirstOrDefault(e => e.Id.Equals(id));
        }

        public DiscountEntity LoadAvailable()
        {
            return All().FirstOrDefault(e => !e.IsOccupied);
        }

        private IQueryable<DiscountEntity> All()
        {
            return _context.Get<DiscountEntity>().AsNoTracking().AsQueryable();
        }
    }
}
