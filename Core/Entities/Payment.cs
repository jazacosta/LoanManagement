namespace Core.Entities;

public class Payment
{
    public int Id { get; set; }
    public decimal PaymentAmount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string Status { get; set; } = string.Empty;

    public int InstallmentId { get; set; }
    public Installment Installment { get; set; } = null!;
}