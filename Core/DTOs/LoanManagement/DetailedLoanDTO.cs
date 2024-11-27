namespace Core.DTOs.LoanManagement
{
    public class DetailedLoanDTO
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public DateTime? ApprovalDate { get; set; }
        public decimal RequestedAmount { get; set; }
        public decimal TotalToPay { get; set; }
        public decimal Profit { get; set; }
        public int TermInMonths { get; set; }
        public string LoanType { get; set; } = string.Empty;
        public float InterestRate { get; set; }
        public int PaidInstallments { get; set; }
        public int PendingInstallments { get; set; }
        public string NextDueDate { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
