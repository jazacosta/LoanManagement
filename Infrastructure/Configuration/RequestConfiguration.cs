using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class RequestConfiguration : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> entity)
        {
            entity.HasKey(x => x.Id);

            entity
                .Property(x => x.LoanType)
                .IsRequired();
            entity
                .Property(x => x.Amount)
                .IsRequired();
            entity
                .Property(x => x.Status)
                .IsRequired();
            entity
                .Property(x => x.TermInterestRateId)
                .IsRequired();

            entity.HasOne(x => x.Customer)
             .WithMany(x => x.Requests)
             .HasForeignKey(x => x.CustomerId);

            entity.HasOne(x => x.TermInterestRate)
                  .WithMany(x => x.Requests)
                  .HasForeignKey(x => x.TermInterestRateId);

            entity.HasOne(x => x.ApprovedLoan)
                  .WithOne(x => x.Request)
                  .HasForeignKey<ApprovedLoan>(x => x.RequestId);
        }
    }
}