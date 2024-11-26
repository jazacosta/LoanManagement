using Core.DTOs;
using Core.Interfaces.Repositories;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TermInterestRateRepository : ITermInterestRateRepository
{
    private readonly ApplicationDbContext _context;

    public TermInterestRateRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TermInterestRateResponseDTO> GetInterestRateByTerm(int TermInMonths)
    {
        var termInterestRate = await _context.TermInterestRates
            .FirstOrDefaultAsync(x => x.TermInMonths == TermInMonths);

        //var response = new TermInterestRateResponseDTO
        //{
        //    Id = termInterestRate.Id,
        //    TermInMonths = termInterestRate.TermInMonths,
        //    InterestRate = termInterestRate.InterestRate
        //};


        if (termInterestRate == null)
            return null;

        return new TermInterestRateResponseDTO //mappear!!
        {
            Id = termInterestRate.Id,
            TermInMonths = termInterestRate.TermInMonths,
            InterestRate = termInterestRate.InterestRate
        };
    }
}
