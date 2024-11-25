namespace Core.DTOs.Request;

public class LoanRequestDTO
{
    public int CustomerId { get; set; }
    public string LoanType { get; set; } = string.Empty;
    public int TermInMonths { get; set; }
    public decimal Amount { get; set; }
}
