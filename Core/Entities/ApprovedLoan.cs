namespace Core.Entities;

public class ApprovedLoan
{
    public int Id { get; set; }
    public string LoanType { get; set; } = string.Empty;
    public decimal RequestAmount { get; set; }
    public float InterestRate { get; set; }
    public DateTime? ApprovalDate { get; set; }
    public int TermInterestRateId { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    public int LoanRequestId { get; set; }
    public LoanRequest LoanRequest { get; set; } = null!;
    //public TermInterestRate TermInterestRate { get; set; } = null!;

    public List<Installment> Installments { get; set; } = [];
}