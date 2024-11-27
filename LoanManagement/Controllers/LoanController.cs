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

    [HttpPost("{Id}/approve")]
    //[Authorize(Roles = "admin")]
    public async Task<IActionResult> ApproveLoan([FromRoute] int Id)
    {
        
            await _loanService.ApproveLoan(Id);
            return Ok("Loan successfully approved.");
        
    }

    [HttpPost("{Id}/reject")]
    //[Authorize(Roles = "admin")]
    public async Task<IActionResult> RejectLoan([FromRoute] int Id, [FromQuery] string RejectionReason)
    {
        try
        {
            await _loanService.RejectLoan(Id, RejectionReason);
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
