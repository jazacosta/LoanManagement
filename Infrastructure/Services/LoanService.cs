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

        var firstDueDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1).AddMonths(1);

        for (int i = 0; i < TermInMonths; i++)
        {
            var interestAmount = remainingAmount * (decimal)monthlyRate;
            var capitalAmount = Amount / TermInMonths;
            remainingAmount -= capitalAmount;

            installments.Add(new Installment
            {
                CapitalAmount = Math.Round(capitalAmount),
                InterestAmount = Math.Round(interestAmount),
                TotalAmount = Math.Round(capitalAmount + interestAmount),
                DueDate = firstDueDate.AddMonths(i).ToUniversalTime(),
                Status = "Pending",
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


    public async Task<DetailedLoanDTO> GetLoanDetails(int approvedLoanId, CancellationToken cancellationToken = default)
    {
        var loan = await _loanRepository.GetApprovedLoanById(approvedLoanId, cancellationToken);

        if (loan == null)
            throw new KeyNotFoundException($"No approved loan found with ID {approvedLoanId}");

        var totalToPay = loan.Installments.Sum(i => i.TotalAmount);
        var paidInstallments = loan.Installments.Count(i => i.Status == "Paid");
        var pendingInstallments = loan.Installments.Count(i => i.Status == "Pending");
        var nextDueDate = loan.Installments
                              .Where(i => i.Status == "Pending")
                              .OrderBy(i => i.DueDate)
                              .Select(i => (DateTime?)i.DueDate)
                              .FirstOrDefault();

        return new DetailedLoanDTO
        {
            CustomerId = loan.Customer.Id,
            CustomerName = loan.Customer.FirstName,
            ApprovalDate = loan.ApprovalDate,
            RequestedAmount = loan.RequestAmount,
            TotalToPay = Math.Round(totalToPay),
            Profit = Math.Round(totalToPay - loan.RequestAmount),
            TermInMonths = loan.TermInterestRate.TermInMonths,
            LoanType = loan.LoanType,
            InterestRate = loan.InterestRate,
            PaidInstallments = paidInstallments,
            PendingInstallments = pendingInstallments,
            NextDueDate = nextDueDate,
            Status = pendingInstallments == 0 ? "All installments are paid" : "Pending installments"
        };
    }
}
