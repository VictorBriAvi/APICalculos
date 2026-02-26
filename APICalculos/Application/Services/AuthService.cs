using APICalculos.Application.DTOs.Login;
using APICalculos.Application.Interfaces;
using APICalculos.Domain.Entidades;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public AuthService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<string?> LoginAsync(LoginDTO dto)
    {
        var user = await _userRepository.GetByUsernameAsync(dto.Username);
        if (user == null) return null;

        // ⚡ Verificar contraseña con BCrypt
        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return null;

        if (!user.IsActive) return null;

        return GenerateJwt(user);
    }

    private string GenerateJwt(User user)
    {
        var claims = new List<Claim>
        {
            new Claim("userId", user.Id.ToString()),
            new Claim("storeId", user.StoreId.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(4),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
