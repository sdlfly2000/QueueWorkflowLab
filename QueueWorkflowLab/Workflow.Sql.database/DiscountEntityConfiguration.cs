using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Workflow.Sql.database
{
    public class DiscountEntityConfiguration : IEntityTypeConfiguration<DiscountEntity>
    {
        public void Configure(EntityTypeBuilder<DiscountEntity> builder)
        {
            builder
                .Property(e => e.Id).HasColumnName("discountId")
                .IsRequired();
            builder
                .Property(e => e.RowVersion).HasColumnName("rowVersion").IsRowVersion();

            builder.HasKey(e => e.Id);

            builder.ToTable("Discounts");
        }
    }
}
