using Core.DTOs.LoanManagement;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

namespace Infrastructure.Services;

public class LoanService : ILoanService
{
    private readonly ILoanRepository _loanRepository;

    public LoanService(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }

    public async Task ApproveLoan(ApprovedLoanDTO loanApproval)
    {
        var loanRequest = await _loanRepository.GetLoanRequestById(loanApproval.LoanRequestId);
        if (loanRequest == null || loanRequest.Status != "Pending Approval")
            throw new InvalidOperationException("The request is not available for approval.");

        // var termInterestRate = loanRequest.TermInterestRate;
        var termn = await _loanRepository.GetTerm(loanRequest.TermInterestRateId);
        if (termn == null)
            throw new InvalidOperationException("No valid interest rate was found for this term.");

        // Convertir la tasa de interés de float a decimal
        //decimal interestRateDecimal = (decimal)termInterestRate.InterestRate;

        var approvedLoan = new ApprovedLoan
        {
            LoanType = loanRequest.LoanType,
            RequestAmount = loanRequest.Amount,
            InterestRate = termn.InterestRate,
            ApprovalDate = DateTime.UtcNow,
            CustomerId = loanRequest.CustomerId,
            LoanRequestId = loanRequest.Id,
            Installments = GenerateInstallments(loanRequest.Amount, termn.InterestRate, loanRequest.TermInMonths),
            TermInterestRateId = termn.Id
            
        };

        //saves the loan app with its installments
        await _loanRepository.SaveApprovedLoan(approvedLoan);
        await _loanRepository.SaveInstallments(approvedLoan.Installments);

        loanRequest.Status = "Approved";
        await _loanRepository.UpdateLoanRequest(loanRequest);
    }

    private List<Installment> GenerateInstallments(decimal Amount, float InterestRate, int TermInMonths) //on installments ig¿
    {
        var installments = new List<Installment>();
        var monthlyRate = InterestRate / 12 / 100;
        var totalAmount = Amount * (decimal)(1 + monthlyRate * TermInMonths);

        for (int i = 1; i <= TermInMonths; i++)
        {
            var capitalAmount = Amount / TermInMonths;
            var interestAmount = totalAmount / TermInMonths - capitalAmount;
            installments.Add(new Installment
            {
                CapitalAmount = capitalAmount,
                InterestAmount = interestAmount,
                TotalAmount = capitalAmount + interestAmount,
                DueDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month + i, 1).ToUniversalTime(),
                Status = "Pending"
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
