using Core.DTOs.InstallmentSimulator;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoanManagement.Controllers
{
    public class SimulatorController : BaseApiController
    {
        private readonly ISimulatorService _simulatorService;

        public SimulatorController(ISimulatorService simulatorService)
        {
            _simulatorService = simulatorService;
        }

        [HttpPost("installment-simulator")]
        public async Task<IActionResult> SimulateInstallment([FromBody] InstallmentSimDTO installmentSimDTO)
        {
            var result = await _simulatorService.SimulateInstallment(installmentSimDTO);
            return Ok(result);
        }
    }
}
