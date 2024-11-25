using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

namespace Infrastructure.Services;

public class InstallmentService : IInstallmentService
{
    private readonly IInstallmentRepository _installmentRepository;
    private readonly IMapper _mapper;

    public InstallmentService(IInstallmentRepository installmentRepository, IMapper mapper)
    {
        _installmentRepository = installmentRepository;
        _mapper = mapper;
    }

    public Task<List<InstallmentDTO>> GenerateInstallment(ApprovedLoan approvedLoan)
    {
        throw new NotImplementedException();
    }
}
