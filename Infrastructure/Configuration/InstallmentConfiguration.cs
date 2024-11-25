﻿using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class InstallmentConfiguration : IEntityTypeConfiguration<Installment>
    {
        public void Configure(EntityTypeBuilder<Installment> entity)
        {
            entity.HasKey(x => x.Id);

            entity
                .Property(x => x.CapitalAmount)
                .IsRequired();
            entity
                .Property(x => x.InterestAmount)
                .IsRequired();
            entity
                .Property(x => x.TotalAmount)
                .IsRequired();
            entity
                .Property(x => x.DueDate)
                .IsRequired();
            entity
                .Property(x => x.Status)
                .IsRequired();

            entity
                .HasOne(x => x.ApprovedLoan)
                .WithMany(x => x.Installments)
                .HasForeignKey(x => x.ApprovedLoanId);

            entity
                .HasMany(x => x.Payments)
                .WithOne(x => x.Installment)
                .HasForeignKey(x => x.InstallmentId);
        }
    }
}