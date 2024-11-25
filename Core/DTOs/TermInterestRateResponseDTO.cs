namespace Core.DTOs;

public class TermInterestRateResponseDTO
{
    public int Id { get; set; }
    public int TermInMonths { get; set; }
    public float InterestRate { get; set; }
}