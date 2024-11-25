namespace Core.DTOs;

public class InstallmentDTO
{
    public int Id { get; set; }
    public decimal CapitalAmount { get; set; }
    public decimal InterestAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime DueDate { get; set; }
    public string Status { get; set; } = string.Empty;
}
