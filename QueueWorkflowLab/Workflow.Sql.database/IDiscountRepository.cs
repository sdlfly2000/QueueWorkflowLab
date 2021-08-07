namespace Workflow.Sql.database
{
    public interface IDiscountRepository
    {
        DiscountEntity Load(string id);

        DiscountEntity LoadAvailable();

        void OccupyDiscount(string id);

    }
}
