using Core.Interfaces.Services;
using Core.Models;
using LoanManagement.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace LoanManagement.Authentication;

public class AuthController : BaseApiController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("api/generate-token")]
    public IActionResult GenerateToken([FromBody] User user)
    {
        return Ok(_authService.CreateToken(user));
    }

}
