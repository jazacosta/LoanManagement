namespace Core.DTOs.Request;

public class LoanRequestResponseDTO
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string LoanType { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public int TermInMonths { get; set; }
    public string Status { get; set; } = "Pending Approval"; //intialize in mapping / Default State: Pending Approval
}
