using Microsoft.EntityFrameworkCore;
using Common.Core.DependencyInjection;

namespace Workflow.Sql.database
{
    [ServiceLocate(typeof(IWorkflowDbContext), ServiceType.Scoped)]
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

        public DbSet<TEntity> Get<TEntity>()
            where TEntity : class
        {
            return Set<TEntity>();
        }

        public void Save()
        {
            SaveChanges();
        }

        public void UpdateEntity<TEntity>(TEntity entity)
        {
            Update(entity);
        }
    }
}
