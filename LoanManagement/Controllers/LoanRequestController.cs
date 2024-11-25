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

    [HttpPost("loan-request")]
    public async Task<IActionResult> CreateLoanRequest([FromBody] LoanRequestDTO loanRequestDTO)
    {
        var result = await _loanRequestService.CreateLoanRequest(loanRequestDTO); //
        return Ok(result);
    }
}
