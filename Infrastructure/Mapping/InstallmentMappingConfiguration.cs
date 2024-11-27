using Core.DTOs.InstallmentPayment;
using Core.DTOs.InstallmentSimulator;
using Core.Entities;
using Mapster;

namespace Infrastructure.Mapping;

public class InstallmentMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<InstallmentSimDTO, InstallmentSimResponseDTO>();

        config.NewConfig<Payment, PaymentDTO>();
    }
}
