using System;
using System.Collections.Generic;
using System.Text;

namespace Workflow.Sql.database
{
    public interface IDiscountRepository
    {
        DiscountEntity Load(string id);

        DiscountEntity LoadAvailable();


    }
}
