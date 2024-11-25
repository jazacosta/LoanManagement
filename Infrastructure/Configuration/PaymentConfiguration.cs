using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> entity)
        {
            entity.HasKey(x => x.Id);

            entity
                .Property(x => x.PaymentAmount)
                .IsRequired();
            entity
                .Property(x => x.PaymentDate)
                .IsRequired();
            entity
                .Property(x => x.Status)
                .IsRequired();

            entity
                .HasOne(x => x.Installment)
                .WithMany(x => x.Payments)
                .HasForeignKey(x => x.InstallmentId);

        }

    }
}