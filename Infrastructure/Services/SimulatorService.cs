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

    private static decimal Pow(decimal baseValue, int exponent)
    {
        decimal result = 1;
        for (int i = 0; i < exponent; i++)
        {
            result *= baseValue;
        }
        return result;
    }

    public async Task<InstallmentSimResponseDTO> SimulateInstallment(InstallmentSimDTO installmentSimDTO)
    {
        var termInterestRate = await _termInterestRateRepository.GetInterestRateByTerm(installmentSimDTO.TermInMonths);

        //obtains the monthly interest rate
        var monthlyInterestRate = (decimal)(termInterestRate.InterestRate / 100) / 12;
        
        //uses Pow (to manage decimals) method to calculate monthly installment
        var numerator = monthlyInterestRate * Pow(1 + monthlyInterestRate, installmentSimDTO.TermInMonths);
        var denominator = Pow(1 + monthlyInterestRate, installmentSimDTO.TermInMonths) - 1;
        //this uses the french system btw
        //?
        var monthlyInstallment = installmentSimDTO.Amount * numerator / denominator;
        var totalToPay = monthlyInstallment * installmentSimDTO.TermInMonths;

        //mappear!!
        return new InstallmentSimResponseDTO
        {
            MonthlyInstallment = Math.Round(monthlyInstallment),
            TotalToPay = Math.Round(totalToPay)
        };
    }
}
