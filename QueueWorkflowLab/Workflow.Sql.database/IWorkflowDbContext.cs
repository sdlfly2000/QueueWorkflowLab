using Microsoft.EntityFrameworkCore;

namespace Workflow.Sql.database
{
    public interface IWorkflowDbContext
    {
        DbSet<TEntity> Get<TEntity>() where TEntity : class;
    }
}
