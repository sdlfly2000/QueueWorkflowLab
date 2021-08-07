using Common.Core.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Workflow.Sql.database
{
    [ServiceLocate(typeof(IUnitOfWork))]
    public class WorkflowUnitOfWork : IUnitOfWork
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IDiscountObtainedRepository _discountObtainedRepository;
        private readonly IWorkflowDbContext _context;

        public WorkflowUnitOfWork(IServiceScopeFactory serviceScopeFactory)
        {
            _context = serviceScopeFactory
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<IWorkflowDbContext>();

            _discountRepository = new DiscountRepository(_context);
            _discountObtainedRepository = new DiscountObtainedRepository(_context);
        }

        public void Add(DiscountObtainedEntity entity)
        {
            Console.WriteLine($"Instance id: {_context.GetHashCode()}");
            _discountObtainedRepository.Add(entity);
        }

        public DiscountEntity Load(string id)
        {
            Console.WriteLine($"Instance id: {_context.GetHashCode()}");
            return _discountRepository.Load(id);
        }

        public DiscountEntity LoadAvailable()
        {
            Console.WriteLine($"Instance id: {_context.GetHashCode()}");
            return _discountRepository.LoadAvailable();
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
