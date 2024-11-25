using Core.DTOs.InstallmentSimulator;

namespace Core.Interfaces.Services;

public interface ISimulatorService
{
    Task<InstallmentSimResponseDTO> SimulateInstallment(InstallmentSimDTO installmentSimDTO);
}
