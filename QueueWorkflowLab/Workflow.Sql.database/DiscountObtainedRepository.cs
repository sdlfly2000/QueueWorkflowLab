using Common.Core.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Workflow.Sql.database
{
    [ServiceLocate(typeof(IDiscountObtainedRepository))]
    public class DiscountObtainedRepository : IDiscountObtainedRepository
    {
        private readonly IWorkflowDbContext _context;

        public DiscountObtainedRepository(IServiceScopeFactory serviceScopeFactory)
        {
            _context = serviceScopeFactory
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<IWorkflowDbContext>();
        }

        public void Add(DiscountObtainedEntity entity)
        {
            _context.Get<DiscountObtainedEntity>().Add(entity);
            _context.Save();
        }
    }
}
