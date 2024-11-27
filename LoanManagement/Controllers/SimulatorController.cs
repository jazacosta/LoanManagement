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
            try
            {
                var result = await _simulatorService.SimulateInstallment(installmentSimDTO);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }
    }
}
