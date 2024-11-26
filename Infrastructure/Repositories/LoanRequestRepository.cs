using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Contexts;
using System;

namespace Infrastructure.Repositories;

internal class LoanRequestRepository : ILoanRequestRepository
{
    private readonly ApplicationDbContext _context;

    public LoanRequestRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<LoanRequest> AddLoanRequest(LoanRequest request)
    {
        _context.LoanRequests.Add(request);
        await _context.SaveChangesAsync();
        return request;
    }
}
