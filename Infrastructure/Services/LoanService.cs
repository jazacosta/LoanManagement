using Core.DTOs.LoanManagement;
using Core.DTOs.Request;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Infrastructure.Repositories;
using Mapster;

namespace Infrastructure.Services;

public class LoanService : ILoanService
{
    private readonly ILoanRepository _loanRepository;
    private readonly IInstallmentRepository _installmentRepository;
    private readonly ITermInterestRateRepository _termInterestRateRepository;

    public LoanService(ILoanRepository loanRepository, IInstallmentRepository installmentRepository, ITermInterestRateRepository termInterestRateRepository)
    {
        _loanRepository = loanRepository;
        _installmentRepository = installmentRepository;
        _termInterestRateRepository = termInterestRateRepository;
    }

    public async Task ApproveLoan(int Id)
    {
        var loanRequest = await _loanRepository.GetLoanRequestById(Id);
        if (loanRequest == null || loanRequest.Status != "Pending Approval")
            throw new InvalidOperationException("The request is not available for approval.");

        var term = await _termInterestRateRepository.GetInterestRateByTerm(loanRequest.TermInMonths);
        if (term == null)
            throw new KeyNotFoundException($"No interest rate found for term: {loanRequest.TermInMonths} months.");

        var approvedLoan = loanRequest.Adapt<ApprovedLoan>();
        approvedLoan.InterestRate = term.InterestRate;
        await _loanRepository.SaveApprovedLoan(approvedLoan);

        var installments = GenerateInstallments(approvedLoan.RequestAmount, approvedLoan.InterestRate, loanRequest.TermInMonths);

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


    public async Task RejectLoan(int Id, string RejectionReason)
    {
        var loanRequest = await _loanRepository.GetLoanRequestById(Id);

        if (loanRequest == null || loanRequest.Status != "Pending Approval")
            throw new InvalidOperationException("The loan request is not available for rejection.");

        if (string.IsNullOrWhiteSpace(RejectionReason))
            throw new ArgumentException("You must provide a reason for the rejection.");

        loanRequest.Status = "Rejected";
        loanRequest.RejectionReason = RejectionReason;
        await _loanRepository.UpdateLoanRequest(loanRequest);
    }


    public async Task<DetailedLoanDTO> GetLoanDetails(int approvedLoanId, CancellationToken cancellationToken = default)
    {
        var loan = await _loanRepository.GetApprovedLoanById(approvedLoanId, cancellationToken);
        var term = await _loanRepository.GetTerm(loan.TermInterestRateId);

        if (loan == null)
            throw new KeyNotFoundException($"No approved loan found with ID {approvedLoanId}");

        var totalToPay = loan.Installments.Sum(i => i.TotalAmount);
        var paidInstallments = loan.Installments.Count(i => i.Status == "Paid");
        var pendingInstallments = loan.Installments.Count(i => i.Status == "Pending");

        
        var dueDate = loan.Installments
                              .Where(i => i.Status == "Pending")
                              .OrderBy(i => i.DueDate)
                              .FirstOrDefault();

        string message = dueDate != null
            ? dueDate.DueDate.ToString("yyyy-MM-dd")
            : "Todas las cuotas estan pagadas";

        var response = loan.Adapt<DetailedLoanDTO>();
        response.TotalToPay = Math.Round(totalToPay);
        response.Profit = Math.Round(totalToPay - loan.RequestAmount);
        response.TermInMonths = term.TermInMonths;
        response.PaidInstallments = paidInstallments;
        response.PendingInstallments = pendingInstallments;
        response.NextDueDate = message;
        response.Status = pendingInstallments == 0 ? "All installments are paid" : "Pending installments";

        return response;
    }
}
