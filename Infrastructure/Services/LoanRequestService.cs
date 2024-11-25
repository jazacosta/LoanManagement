using Core.DTOs.Request;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
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
        if (term == null) throw new ArgumentException("The specified term is not valid.");

        var loanRequest = new LoanRequest //mappear!!
        {
            CustomerId = loanRequestDTO.CustomerId,
            LoanType = loanRequestDTO.LoanType,
            Amount = loanRequestDTO.Amount,
            TermInterestRateId = term.Id,
            Status = "Pending Approval", // Initial state
            //CustomerId = 1 // Replace with actual CustomerId (e.g., from Auth context)
        };

        var result = await _loanRequestRepository.AddLoanRequest(loanRequest);

        return new LoanRequestResponseDTO //mappear!!
        {
            Id = result.Id,
            CustomerId = result.CustomerId,
            LoanType = result.LoanType,
            Amount = result.Amount,
            TermInMonths = term.TermInMonths,
            Status = result.Status
        };
    }
}
