namespace Core.DTOs.LoanManagement;

public class RejectedLoanDTO
{
    public int LoanRequestId { get; set; }
    public string RejectionReason { get; set; } = string.Empty;
}
