namespace Core.Entities;

public class Request
{
    public int Id { get; set; }
    public string LoanType { get; set; } = string.Empty;
    public int Amount { get; set; } //change to decimal
    public string Status { get; set; } = string.Empty;

    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    public int TermInterestRateId { get; set; }
    public TermInterestRate TermInterestRate { get; set; } = null!;
    public ApprovedLoan ApprovedLoan { get; set; } = null!; //1:1
}