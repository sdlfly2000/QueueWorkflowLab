using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Workflow.Sql.database
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IWorkflowDbContext _context;

        public DiscountRepository(IWorkflowDbContext context)
        {
            _context = context;
        }

        public DiscountEntity Load(string id)
        {
            return All().FirstOrDefault(e => e.Id.Equals(id));
        }

        public DiscountEntity LoadAvailable()
        {
            return All().FirstOrDefault(e => !e.IsOccupied);
        }

        public void OccupyDiscount(string id)
        {
            var discount = All().First(e => e.Id.Equals(id));
            discount.IsOccupied = true;
            _context.UpdateEntity(discount);
            _context.Save();
        }

        private IQueryable<DiscountEntity> All()
        {
            return _context.Get<DiscountEntity>().AsNoTracking().AsQueryable();
        }
    }
}
