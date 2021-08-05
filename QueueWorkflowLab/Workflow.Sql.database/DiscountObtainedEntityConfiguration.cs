using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Workflow.Sql.database
{
    public class DiscountObtainedEntityConfiguration : IEntityTypeConfiguration<DiscountObtainedEntity>
    {
        public void Configure(EntityTypeBuilder<DiscountObtainedEntity> builder)
        {
            builder.Property(e => e.Id).HasColumnName("discountObtainedId").IsRequired();
            builder.Property(e => e.DiscountId).HasColumnName("discountId").IsRequired();
            builder.Property(e => e.WorkflowName).HasColumnName("workflowName");

            builder.HasKey(e => e.Id);
            builder
                .HasOne(e => e.Discount)
                .WithOne()
                .HasForeignKey<DiscountObtainedEntity>(e => e.DiscountId);
            builder.ToTable("DiscountObtained");
        }
    }
}
