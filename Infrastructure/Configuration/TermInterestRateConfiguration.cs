using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class TermInterestRateConfiguration : IEntityTypeConfiguration<TermInterestRate>
    {
        public void Configure(EntityTypeBuilder<TermInterestRate> entity)
        {
            entity.HasKey(x => x.Id);

            entity
                .Property(x => x.TermInMonths)
                .IsRequired();
            entity
                .Property(x => x.InterestRate)
                .IsRequired();

            entity
                .HasMany(x => x.LoanRequests)
                .WithOne(x => x.TermInterestRate)
                .HasForeignKey(x => x.TermInterestRateId);
        }
    }
}