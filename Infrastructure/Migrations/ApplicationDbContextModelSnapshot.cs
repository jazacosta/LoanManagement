﻿// <auto-generated />
using System;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Core.Entities.ApprovedLoan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ApprovalDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<float>("InterestRate")
                        .HasColumnType("real");

                    b.Property<string>("LoanType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("RequestAmount")
                        .HasColumnType("numeric");

                    b.Property<int>("RequestId")
                        .HasColumnType("integer");

                    b.Property<int>("TermInterestRateId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("RequestId")
                        .IsUnique();

                    b.HasIndex("TermInterestRateId");

                    b.ToTable("ApprovedLoans");
                });

            modelBuilder.Entity("Core.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Core.Entities.Installment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ApprovedLoanId")
                        .HasColumnType("integer");

                    b.Property<decimal>("CapitalAmount")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("InterestAmount")
                        .HasColumnType("numeric");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("ApprovedLoanId");

                    b.ToTable("Installments");
                });

            modelBuilder.Entity("Core.Entities.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("InstallmentId")
                        .HasColumnType("integer");

                    b.Property<decimal>("PaymentAmount")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("InstallmentId")
                        .IsUnique();

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("Core.Entities.Request", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<string>("LoanType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TermInterestRateId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("TermInterestRateId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("Core.Entities.TermInterestRate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<float>("InterestRate")
                        .HasColumnType("real");

                    b.Property<int>("TermInMonths")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("TermInterestRates");
                });

            modelBuilder.Entity("Core.Entities.ApprovedLoan", b =>
                {
                    b.HasOne("Core.Entities.Customer", "Customer")
                        .WithMany("ApprovedLoans")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.Request", "Request")
                        .WithOne("ApprovedLoan")
                        .HasForeignKey("Core.Entities.ApprovedLoan", "RequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.TermInterestRate", "TermInterestRate")
                        .WithMany()
                        .HasForeignKey("TermInterestRateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Request");

                    b.Navigation("TermInterestRate");
                });

            modelBuilder.Entity("Core.Entities.Installment", b =>
                {
                    b.HasOne("Core.Entities.ApprovedLoan", "ApprovedLoan")
                        .WithMany("Installments")
                        .HasForeignKey("ApprovedLoanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApprovedLoan");
                });

            modelBuilder.Entity("Core.Entities.Payment", b =>
                {
                    b.HasOne("Core.Entities.Installment", "Installment")
                        .WithOne("Payment")
                        .HasForeignKey("Core.Entities.Payment", "InstallmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Installment");
                });

            modelBuilder.Entity("Core.Entities.Request", b =>
                {
                    b.HasOne("Core.Entities.Customer", "Customer")
                        .WithMany("Requests")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.TermInterestRate", "TermInterestRate")
                        .WithMany("Requests")
                        .HasForeignKey("TermInterestRateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("TermInterestRate");
                });

            modelBuilder.Entity("Core.Entities.ApprovedLoan", b =>
                {
                    b.Navigation("Installments");
                });

            modelBuilder.Entity("Core.Entities.Customer", b =>
                {
                    b.Navigation("ApprovedLoans");

                    b.Navigation("Requests");
                });

            modelBuilder.Entity("Core.Entities.Installment", b =>
                {
                    b.Navigation("Payment")
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Entities.Request", b =>
                {
                    b.Navigation("ApprovedLoan")
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Entities.TermInterestRate", b =>
                {
                    b.Navigation("Requests");
                });
#pragma warning restore 612, 618
        }
    }
}
