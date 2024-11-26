﻿using Core.DTOs.LoanManagement;
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
    public async Task<IActionResult> ApproveLoan([FromBody] ApprovedLoanDTO loanApproval)
    {
        try
        {
            await _loanService.ApproveLoan(loanApproval);
            return Ok("Loan successfully approved.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("reject")]
    //[Authorize(Roles = "admin")]
    public async Task<IActionResult> RejectLoan([FromBody] RejectedLoanDTO loanRejection)
    {
        try
        {
            await _loanService.RejectLoan(loanRejection);
            return Ok("Loan successfully rejected.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
