using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

namespace Infrastructure.Services;

public class InstallmentService : IInstallmentService
{
    private readonly IInstallmentRepository _installmentRepository;
    private readonly IMapper _mapper;

    public InstallmentService(IInstallmentRepository installmentRepository, IMapper mapper)
    {
        _installmentRepository = installmentRepository;
        _mapper = mapper;
    }

    public Task<List<InstallmentDTO>> GenerateInstallment(ApprovedLoan approvedLoan)
    {
        throw new NotImplementedException();

        /*
        using System;

        class Program
        {
            static void Main()
            {
                double capital = 100000; // Monto del préstamo
                double tasaAnual = 0.09; // Tasa de interés anual
                int plazoMeses = 12; // Plazo en meses

                double tasaMensual = tasaAnual / 12; // Tasa mensual
                double cuota = capital * (tasaMensual * Math.Pow(1 + tasaMensual, plazoMeses)) /
                               (Math.Pow(1 + tasaMensual, plazoMeses) - 1);

                Console.WriteLine($"Cuota fija mensual: {cuota:F2}");

                double saldo = capital;

                Console.WriteLine("\nTabla de Amortización:");
                Console.WriteLine("Mes\tCuota\tInterés\tCapital\tSaldo");

                for (int mes = 1; mes <= plazoMeses; mes++)
                {
                    double interes = saldo * tasaMensual;
                    double amortizacion = cuota - interes;
                    saldo -= amortizacion;

                    Console.WriteLine($"{mes}\t{cuota:F2}\t{interes:F2}\t{amortizacion:F2}\t{saldo:F2}");
                }
            }
        }
        */
    }
}
