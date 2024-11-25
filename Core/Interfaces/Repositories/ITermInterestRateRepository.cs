using Core.DTOs;

namespace Core.Interfaces.Repositories;

public interface ITermInterestRateRepository
{
    Task<TermInterestRateResponseDTO> GetInterestRateByTerm(int TermInMonths);
}
