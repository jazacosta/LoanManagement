using Core.DTOs.LoanManagement;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoanManagement.Controllers;

public class LoanController : BaseApiController
{
    private readonly ILoanService _loanService;

    public LoanController(ILoanService loanService)
    {
        _loanService = loanService;
    }

    [HttpPost("approve")]
    //[Authorize(Roles = "admin")]
    public async Task<IActionResult> ApproveLoan([FromBody] ApprovedLoanDTO loanApproval, CancellationToken cancellationToken)
    {
        
            await _loanService.ApproveLoan(loanApproval, cancellationToken);
            return Ok("Loan successfully approved.");
        
    }

    [HttpPost("reject")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> RejectLoan([FromBody] RejectedLoanDTO loanRejection)
    {
        try
        {
            await _loanService.RejectLoan(loanRejection);
            return Ok("Loan successfully rejected.");
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("{id}/get-loan-details")]
    public async Task<IActionResult> GetLoanDetails(int id, CancellationToken cancellationToken)
    {
        try
        {
            var loanDetails = await _loanService.GetLoanDetails(id, cancellationToken);
            return Ok(loanDetails);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}
