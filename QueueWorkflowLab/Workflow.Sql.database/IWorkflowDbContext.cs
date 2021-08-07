using Microsoft.EntityFrameworkCore;
using System;

namespace Workflow.Sql.database
{
    public interface IWorkflowDbContext : IDisposable
    {
        DbSet<TEntity> Get<TEntity>() where TEntity : class;

        void Save();

        void UpdateEntity<TEntity>(TEntity entity);
    }
}
