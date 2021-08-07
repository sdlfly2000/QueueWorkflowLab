using System;

namespace Workflow.Sql.database
{
    public class DiscountEntity
    {
        public string Id { get; set; }

        public bool IsOccupied { get; set; }

        public DateTime RowVersion { get; set; }
    }
}
