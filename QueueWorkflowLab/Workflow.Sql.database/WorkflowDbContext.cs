using Microsoft.EntityFrameworkCore;

namespace Workflow.Sql.database
{
    public class WorkflowDbContext : DbContext, IWorkflowDbContext
    {
        public WorkflowDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DiscountEntityConfiguration());
            modelBuilder.ApplyConfiguration(new DiscountObtainedEntityConfiguration());
        }

        public DbSet<DiscountEntity> Discounts;

        public DbSet<DiscountObtainedEntity> DiscountObtained;
    }
}
