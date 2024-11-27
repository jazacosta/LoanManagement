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
        config.NewConfig<LoanRequest, ApprovedLoan>()
            .Map(dest => dest.RequestAmount, src => src.Amount)
            .Map(dest => dest.InterestRate, src => src.TermInterestRate.InterestRate)
            .Map(dest => dest.ApprovalDate, src => DateTime.UtcNow)
            .Map(dest => dest.LoanRequestId, src => src.Id);

        config.NewConfig<ApprovedLoan, DetailedLoanDTO>();
        
    }
}
