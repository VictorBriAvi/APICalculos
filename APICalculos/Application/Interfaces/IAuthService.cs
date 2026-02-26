using APICalculos.Application.DTOs.Login;

namespace APICalculos.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(LoginDTO dto);
    }
}
