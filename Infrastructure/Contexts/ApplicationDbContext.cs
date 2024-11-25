using Core.Entities;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts
{
    public partial class ApplicationDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<TermInterestRate> TermInterestRates { get; set; }
        public DbSet<LoanRequest> LoanRequests { get; set; }
        public DbSet<ApprovedLoan> ApprovedLoans { get; set; }
        public DbSet<Installment> Installments { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new TermInterestRateConfiguration());
            modelBuilder.ApplyConfiguration(new LoanRequestConfiguration());
            modelBuilder.ApplyConfiguration(new ApprovedLoanConfiguration());
            modelBuilder.ApplyConfiguration(new InstallmentConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentConfiguration());
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}