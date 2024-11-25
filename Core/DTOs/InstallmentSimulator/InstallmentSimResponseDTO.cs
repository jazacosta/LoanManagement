namespace Core.DTOs.InstallmentSimulator;

public class InstallmentSimResponseDTO
{
    public decimal MonthlyInstallment { get; set; } //monthly amount
    public decimal TotalToPay { get; set; }
}
