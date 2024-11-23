namespace Core.Entities;

public class Installment
{
    public int Id { get; set; }
    public decimal CapitalAmount { get; set; }
    public decimal InterestAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime DueDate { get; set; }
    public string Status { get; set; } = string.Empty;

    public int ApprovedLoanId { get; set; }
    public ApprovedLoan ApprovedLoan { get; set; } = null!;
    public Payment Payment { get; set; } = null!; //1:1
}