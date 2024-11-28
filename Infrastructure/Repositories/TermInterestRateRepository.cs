using Core.DTOs;
using Core.Entities;
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

    public async Task<TermInterestRate> GetTermByMonth(int termInMonths)
    {
        var term = await _context.TermInterestRates
            .FirstOrDefaultAsync(x => x.TermInMonths == termInMonths);

        if (term == null)
            return null;

        return term;
    }
}
