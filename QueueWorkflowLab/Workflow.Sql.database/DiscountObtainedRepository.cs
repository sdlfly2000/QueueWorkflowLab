namespace Workflow.Sql.database
{
    public class DiscountObtainedRepository : IDiscountObtainedRepository
    {
        private readonly IWorkflowDbContext _context;

        public DiscountObtainedRepository(IWorkflowDbContext context)
        {
            _context = context;
        }

        public void Add(DiscountObtainedEntity entity)
        {
            _context.Get<DiscountObtainedEntity>().Add(entity);
            _context.Save();
        }
    }
}
