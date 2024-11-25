using Core.DTOs.InstallmentSimulator;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

namespace Infrastructure.Services;

public class SimulatorService : ISimulatorService
{
    private readonly ITermInterestRateRepository _termInterestRateRepository;

    public SimulatorService(ITermInterestRateRepository termInterestRateRepository)
    {
        _termInterestRateRepository = termInterestRateRepository;
    }

    public async Task<InstallmentSimResponseDTO> SimulateInstallment(InstallmentSimDTO installmentSimDTO)
    {
        var termInterestRate = await _termInterestRateRepository.GetInterestRateByTerm(installmentSimDTO.TermInMonths);

        // Perform calculation
        var monthlyInterestRate = termInterestRate.InterestRate / 12 / 100;
        var monthlyInstallment = installmentSimDTO.Amount 
                                * (monthlyInterestRate * Math.Pow(1 + monthlyInterestRate, installmentSimDTO.TermInMonths)) 
                                / (Math.Pow(1 + monthlyInterestRate, installmentSimDTO.TermInMonths) - 1);
        var totalToPay = monthlyInstallment * installmentSimDTO.TermInMonths;

        // Return response
        return new InstallmentSimResponseDTO
        {
            TotalToPay = Math.Round((decimal)totalToPay, 2),
            MonthlyInstallment = Math.Round((decimal)monthlyInstallment, 2)
        };
    }
}
