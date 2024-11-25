namespace Core.DTOs;

public class TermInterestRateResponseDTO
{
    public int Id { get; set; } //ver si esto me pedira en el endpoint installment-simulator
    public int TermInMonths { get; set; }
    public float InterestRate { get; set; }
}