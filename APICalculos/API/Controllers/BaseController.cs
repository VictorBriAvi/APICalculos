using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace APICalculos.API.Controllers
{
    [Authorize] // Opcional: si todos los controllers que hereden requieren token
    public abstract class BaseController : ControllerBase
    {
        protected int GetStoreIdFromToken()
        {
            var storeIdClaim = User.FindFirst("storeId")?.Value;

            if (string.IsNullOrEmpty(storeIdClaim) || !int.TryParse(storeIdClaim, out var storeId))
                throw new UnauthorizedAccessException("StoreId inválido en token");

            return storeId;
        }

        protected int GetUserIdFromToken()
        {
            var userIdClaim = User.FindFirst("userId")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                throw new UnauthorizedAccessException("UserId inválido en token");

            return userId;
        }
    }
}
