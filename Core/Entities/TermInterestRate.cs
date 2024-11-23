namespace Core.Entities;

public class TermInterestRate //guardar el plazo y relacionar con el interes
{
    public int Id { get; set; }
    public int TermInMonths { get; set; }
    public float InterestRate { get; set; }

    public List<Request> Requests { get; set; } = [];
}