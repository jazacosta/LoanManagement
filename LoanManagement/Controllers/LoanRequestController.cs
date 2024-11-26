using Core.DTOs.Request;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoanManagement.Controllers;

public class LoanRequestController : BaseApiController
{
    private readonly ILoanRequestService _loanRequestService;

    public LoanRequestController(ILoanRequestService loanRequestService)
    {
        _loanRequestService = loanRequestService;
    }

    [HttpPost("create-loan-request")]
    public async Task<IActionResult> CreateLoanRequest([FromBody] LoanRequestDTO loanRequestDTO)
    {
        try
        {
            var result = await _loanRequestService.CreateLoanRequest(loanRequestDTO);
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
