using Core.DTOs.Request;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Mapster;
using Microsoft.AspNetCore.Identity.Data;

namespace Infrastructure.Services;

public class LoanRequestService : ILoanRequestService
{
    private readonly ILoanRequestRepository _loanRequestRepository;
    private readonly ITermInterestRateRepository _termInterestRateRepository;

    public LoanRequestService(
        ILoanRequestRepository repository,
        ITermInterestRateRepository termInterestRateRepository)
    {
        _loanRequestRepository = repository;
        _termInterestRateRepository = termInterestRateRepository;
    }

    public async Task<LoanRequestResponseDTO> CreateLoanRequest(LoanRequestDTO loanRequestDTO)
    {
        var term = await _termInterestRateRepository.GetInterestRateByTerm(loanRequestDTO.TermInMonths);
        if (term == null) 
            throw new KeyNotFoundException($"No interest rate found for term: {loanRequestDTO.TermInMonths} months.");


        var loanRequest = loanRequestDTO.Adapt<LoanRequest>();
        loanRequest.TermInMonths = term.TermInMonths;

        await _loanRequestRepository.AddLoanRequest(loanRequest);

        return loanRequest.Adapt<LoanRequestResponseDTO>();
    }
}
