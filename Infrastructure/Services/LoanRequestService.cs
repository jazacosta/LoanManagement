using Core.DTOs.InstallmentSimulator;
using Core.DTOs.Request;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Identity.Data;

namespace Infrastructure.Services;

public class LoanRequestService : ILoanRequestService
{
    private readonly IValidator<LoanRequestDTO> _validator;
    private readonly ILoanRequestRepository _loanRequestRepository;
    private readonly ITermInterestRateRepository _termInterestRateRepository;

    public LoanRequestService(
        IValidator<LoanRequestDTO> validator,
        ILoanRequestRepository repository,
        ITermInterestRateRepository termInterestRateRepository)
    {
        _validator = validator;
        _loanRequestRepository = repository;
        _termInterestRateRepository = termInterestRateRepository;
    }

    public async Task<LoanRequestResponseDTO> CreateLoanRequest(LoanRequestDTO loanRequestDTO)
    {
        var validationResult = await _validator.ValidateAsync(loanRequestDTO);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var term = await _termInterestRateRepository.GetTermByMonth(loanRequestDTO.TermInMonths);
        if (term == null) 
            throw new KeyNotFoundException($"No interest rate found for term: {loanRequestDTO.TermInMonths} months.");

        var loanRequest = loanRequestDTO.Adapt<LoanRequest>();
        loanRequest.TermInMonths = term.TermInMonths;
        loanRequest.TermInterestRateId = term.Id;

        await _loanRequestRepository.AddLoanRequest(loanRequest);

        return loanRequest.Adapt<LoanRequestResponseDTO>();
    }
}
