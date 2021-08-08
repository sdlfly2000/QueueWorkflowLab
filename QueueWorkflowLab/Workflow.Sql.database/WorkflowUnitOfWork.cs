using Common.Core.DependencyInjection;
using Microsoft.EntityFrameworkCore.Internal;
using System;

namespace Workflow.Sql.database
{
    [ServiceLocate(typeof(IUnitOfWork), ServiceType.Transient)]
    public class WorkflowUnitOfWork : IUnitOfWork
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IDiscountObtainedRepository _discountObtainedRepository;
        private readonly IWorkflowDbContext _context;

        public WorkflowUnitOfWork(DbContextPool<WorkflowDbContext> contextPool)
        {
            _context = contextPool.Rent();

            Console.WriteLine($"Create Instance id: {_context.GetHashCode()}");

            _discountRepository = new DiscountRepository(_context);
            _discountObtainedRepository = new DiscountObtainedRepository(_context);
        }

        public void Add(DiscountObtainedEntity entity)
        {
            Console.WriteLine($"From Add DiscountObtained Instance id: {_context.GetHashCode()}");
            _discountObtainedRepository.Add(entity);
        }

        public DiscountEntity Load(string id)
        {
            Console.WriteLine($"Fromn Load Discount Instance id: {_context.GetHashCode()}");
            return _discountRepository.Load(id);
        }

        public DiscountEntity LoadAvailable()
        {
            Console.WriteLine($"From Load Available Instance id: {_context.GetHashCode()}");
            return _discountRepository.LoadAvailable();
        }

        public void OccupyDiscount(string id)
        {
            Console.WriteLine($"From Occupy Discount Instance id: {_context.GetHashCode()}");
            _discountRepository.OccupyDiscount(id);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        ~WorkflowUnitOfWork()
        {
            if (_context != null)
            {
                Dispose();
            }
        }
    }
}
