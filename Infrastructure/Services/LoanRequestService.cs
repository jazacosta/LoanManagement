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
    //    Console.WriteLine($"Loan Request Details:\n" +
    //$"Customer ID: {loanRequestDTO.CustomerId}\n" +
    //$"Loan Type: {loanRequestDTO.LoanType}\n" +
    //$"Amount: {loanRequestDTO.Amount}\n" +
    //$"Term Interest Rate ID: {loanRequestDTO.TermInMonths}\n" +
    //$"Status: Pending Approval");

        //Environment.Exit(0 );
        var term = await _termInterestRateRepository.GetInterestRateByTerm(loanRequestDTO.TermInMonths);
        if (term == null) 
            throw new KeyNotFoundException($"No interest rate found for term: {loanRequestDTO.TermInMonths} months.");

        DateTime dateTime = DateTime.UtcNow;

        var loanRequest = new LoanRequest 
        {
            CustomerId = loanRequestDTO.CustomerId,
            RequestDate = dateTime,
            LoanType = loanRequestDTO.LoanType,
            Amount = loanRequestDTO.Amount,
            TermInterestRateId = term.Id,   
            Status = "Pending Approval"
        };

        var result = await _loanRequestRepository.AddLoanRequest(loanRequest);

        return new LoanRequestResponseDTO
        {
            Id = result.Id,
            CustomerId = result.CustomerId,
            LoanType = result.LoanType,
            Amount = result.Amount,
            RequestDate = dateTime,
            TermInMonths = term.TermInMonths,
            Status = result.Status
        };
    }
}
