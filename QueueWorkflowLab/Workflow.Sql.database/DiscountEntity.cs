using System;
using System.Collections.Generic;
using System.Text;

namespace Workflow.Sql.database
{
    public class DiscountEntity
    {
        public string Id { get; set; }

        public bool IsOccupied { get; set; }

        public string RowVersion { get; set; }
    }
}
