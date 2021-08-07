using Common.Core.DependencyInjection;
using System;
using System.Transactions;
using Workflow.Sql.database;

namespace Workflow.Services.DiscountWorkflow
{
    [ServiceLocate(typeof(ISaveDiscountActivity))]
    public class SaveDiscountActivity : ISaveDiscountActivity
    {
        private readonly IUnitOfWork _uow;

        public SaveDiscountActivity(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void Execute(GetDiscountWorkflowContext context)
        { 
            if (string.IsNullOrEmpty(context.DiscountId))
            {
                return;
            }

            var option = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadUncommitted
            };

            using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, option))
            {
                _uow.Add(new DiscountObtainedEntity
                {
                    Id = Guid.NewGuid().ToString(),
                    WorkflowName = context.Name,
                    DiscountId = context.DiscountId
                });

                _uow.OccupyDiscount(context.DiscountId);

                scope.Complete();
            }
        }
    }
}
