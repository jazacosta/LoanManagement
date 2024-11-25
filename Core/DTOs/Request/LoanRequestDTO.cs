namespace Core.DTOs.Request;

public class LoanRequestDTO
{
    public string LoanType { get; set; } = string.Empty;
    public int TermInMonths { get; set; }
    public int Amount { get; set; } //change to decimal
}
