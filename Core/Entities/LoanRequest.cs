namespace Core.Entities;

public class LoanRequest
{
    public LoanRequest()
    {
        
    }
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string LoanType { get; set; } = string.Empty;
    public int TermInMonths { get; set; }
    public decimal Amount { get; set; }
    public DateTime RequestDate { get; set; }
    public string Status { get; set; } = "Pending Approval";
    public string? RejectionReason { get; set; }

    public Customer Customer { get; set; } = null!;
    public int TermInterestRateId { get; set; }
    public TermInterestRate TermInterestRate { get; set; } = null!;
    public ApprovedLoan ApprovedLoan { get; set; } = null!;
}