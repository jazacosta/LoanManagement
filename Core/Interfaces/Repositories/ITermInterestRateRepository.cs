using Core.DTOs;
using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface ITermInterestRateRepository
{
    Task<TermInterestRate> GetTermByMonth(int TermInMonths);
}
