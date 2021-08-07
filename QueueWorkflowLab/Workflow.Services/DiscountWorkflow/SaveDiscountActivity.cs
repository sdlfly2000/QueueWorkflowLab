using Common.Core.DependencyInjection;
using System;
using System.Transactions;
using Workflow.Sql.database;

namespace Workflow.Services.DiscountWorkflow
{
    [ServiceLocate(typeof(ISaveDiscountActivity))]
    public class SaveDiscountActivity : ISaveDiscountActivity
    {
        private readonly IDiscountObtainedRepository _discountObtainedRepository;

        public SaveDiscountActivity(IDiscountObtainedRepository discountObtainedRepository)
        {
            _discountObtainedRepository = discountObtainedRepository;
        }

        public void Execute(GetDiscountWorkflowContext context)
        {
            var option = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted
            };

            if (string.IsNullOrEmpty(context.DiscountId))
            {
                return;
            }

            using (var scope = new TransactionScope(TransactionScopeOption.Required, option))
            {
                _discountObtainedRepository.Add(new DiscountObtainedEntity
                {
                    Id = Guid.NewGuid().ToString(),
                    WorkflowName = context.Name,
                    DiscountId = context.DiscountId
                });

                scope.Complete();
            }
        }
    }
}
