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

        config.NewConfig<LoanRequestDTO, LoanRequest>()
            .Map(dest => dest.RequestDate, src => DateTime.UtcNow)
            .Map(dest => dest.Status, src => "Pending Approval");

        config.NewConfig<LoanRequest, LoanRequestResponseDTO>();

        config.NewConfig<LoanRequest, ApprovedLoan>()
            .Map(dest => dest.RequestAmount, src => src.Amount)
            .Map(dest => dest.ApprovalDate, src => DateTime.UtcNow)
            .Map(dest => dest.LoanRequestId, src => src.Id);

        config.NewConfig<ApprovedLoan, DetailedLoanDTO>()
            .Map(dest => dest.CustomerName, src => $"{src.Customer.FirstName} {src.Customer.LastName}")
            .Map(dest => dest.RequestedAmount, src => src.RequestAmount);
       
    }
}
