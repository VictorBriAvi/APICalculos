using APICalculos.Application.DTOs.Login;
using APICalculos.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDTO dto)
    {
        var token = await _authService.LoginAsync(dto);

        if (token == null)
            return Unauthorized("Credenciales inválidas");

        return Ok(new { token });
    }
}
