using Core.DTOs.LoanManagement;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

namespace Infrastructure.Services;

public class LoanService : ILoanService
{
    private readonly ILoanRepository _loanRepository;
    private readonly IInstallmentRepository _installmentRepository;

    public LoanService(ILoanRepository loanRepository, IInstallmentRepository installmentRepository)
    {
        _loanRepository = loanRepository;
        _installmentRepository = installmentRepository;
    }

    public async Task ApproveLoan(ApprovedLoanDTO loanApproval, CancellationToken cancellationToken = default)
    {
        var loanRequest = await _loanRepository.GetLoanRequestById(loanApproval.LoanRequestId);
        if (loanRequest == null || loanRequest.Status != "Pending Approval")
            throw new InvalidOperationException("The request is not available for approval.");

        var term = await _loanRepository.GetTerm(loanRequest.TermInterestRateId);
        if (term == null)
            throw new InvalidOperationException("No valid interest rate was found for this term.");

        
        var approvedLoan = new ApprovedLoan
        {
            LoanType = loanRequest.LoanType,
            RequestAmount = loanRequest.Amount,
            InterestRate = term.InterestRate,
            ApprovalDate = DateTime.UtcNow,
            CustomerId = loanRequest.CustomerId,
            LoanRequestId = loanRequest.Id,
            Installments = [],
            TermInterestRateId = term.Id  
        };
        await _loanRepository.SaveApprovedLoan(approvedLoan, cancellationToken);

        var installments = GenerateInstallments(approvedLoan.RequestAmount, approvedLoan.InterestRate, term.TermInMonths);

        foreach (var installment in installments)
        {
            installment.ApprovedLoanId = approvedLoan.LoanRequestId;
            await _installmentRepository.AddInstallment(installment);
        }


        loanRequest.Status = "Approved";
        await _loanRepository.UpdateLoanRequest(loanRequest);
    }

    private List<Installment> GenerateInstallments(decimal Amount, float InterestRate, int TermInMonths) 
    {
        var installments = new List<Installment>();
        var monthlyRate = InterestRate / 12 / 100;
        var remainingAmount = Amount;

        for (int i = 1; i <= TermInMonths; i++)
        {
            var interestAmount = remainingAmount * (decimal)monthlyRate;
            var capitalAmount = Amount / TermInMonths;
            remainingAmount -= capitalAmount;

            installments.Add(new Installment
            {
                CapitalAmount = capitalAmount,
                InterestAmount = interestAmount,
                TotalAmount = capitalAmount + interestAmount,
                DueDate = DateTime.UtcNow.AddMonths(i).ToUniversalTime(),
                Status = "Pending",
                //ApprovedLoanId = 0
            });
        }

        return installments;
    }


    public async Task RejectLoan(RejectedLoanDTO loanRejection)
    {
        var loanRequest = await _loanRepository.GetLoanRequestById(loanRejection.LoanRequestId);

        if (loanRequest == null || loanRequest.Status != "Pending Approval") //searches for a lrID
            throw new InvalidOperationException("The loan request is not available for rejection.");

        if (string.IsNullOrWhiteSpace(loanRejection.RejectionReason))
            throw new ArgumentException("You must provide a reason for the rejection.");

        loanRequest.Status = "Rejected";
        loanRequest.RejectionReason = loanRejection.RejectionReason;
        await _loanRepository.UpdateLoanRequest(loanRequest);
    }
}
