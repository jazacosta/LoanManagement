﻿using Core.DTOs.InstallmentSimulator;
using Core.DTOs.Request;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using FluentValidation;
using Mapster;

namespace Infrastructure.Services;

public class SimulatorService : ISimulatorService
{
    private readonly IValidator<InstallmentSimDTO> _validator;
    private readonly ITermInterestRateRepository _termInterestRateRepository;

    public SimulatorService(IValidator<InstallmentSimDTO> validator, ITermInterestRateRepository termInterestRateRepository)
    {
        _validator = validator;
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
        var validationResult = await _validator.ValidateAsync(installmentSimDTO);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var termInterestRate = await _termInterestRateRepository.GetTermByMonth(installmentSimDTO.TermInMonths);
        if (termInterestRate == null)
            throw new KeyNotFoundException($"No interest rate found for term: {installmentSimDTO.TermInMonths} months.");

        var monthlyInterestRate = (decimal)(termInterestRate.InterestRate / 100) / 12;
        
        var numerator = monthlyInterestRate * Pow(1 + monthlyInterestRate, installmentSimDTO.TermInMonths);
        var denominator = Pow(1 + monthlyInterestRate, installmentSimDTO.TermInMonths) - 1;
       
        var monthlyInstallment = installmentSimDTO.Amount * numerator / denominator;
        var totalToPay = monthlyInstallment * installmentSimDTO.TermInMonths;


        var response = installmentSimDTO.Adapt<InstallmentSimResponseDTO>();
        response.MonthlyInstallment = Math.Round(monthlyInstallment);
        response.TotalToPay = Math.Round(totalToPay);
        return response;

    }
}
