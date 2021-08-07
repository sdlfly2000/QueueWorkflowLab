using System;

namespace Workflow.Sql.database
{
    public interface IUnitOfWork : IDiscountObtainedRepository, IDiscountRepository, IDisposable
    {
    }
}
