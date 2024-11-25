namespace Core.Entities;

public class TermInterestRate
{
    public int Id { get; set; }
    public int TermInMonths { get; set; }
    public float InterestRate { get; set; }

    public List<Request> Requests { get; set; } = [];
}