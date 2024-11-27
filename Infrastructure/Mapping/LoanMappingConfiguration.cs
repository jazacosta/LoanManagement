using Core.DTOs;
using Core.DTOs.LoanManagement;
using Core.DTOs.Request;
using Core.Entities;
using Mapster;

namespace Infrastructure.Mapping;

public class LoanMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TermInterestRate, TermInterestRateResponseDTO>();

        config.NewConfig<LoanRequest, LoanRequestDTO>();
        config.NewConfig<LoanRequest, LoanRequestResponseDTO>();

        config.NewConfig<LoanRequest, ApprovedLoanDTO>();
        config.NewConfig<LoanRequest, RejectedLoanDTO>();

        config.NewConfig<ApprovedLoan, DetailedLoanDTO>();
        
    }
}
