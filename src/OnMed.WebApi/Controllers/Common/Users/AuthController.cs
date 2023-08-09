using Microsoft.AspNetCore.Mvc;
using OnMed.Persistance.Dtos.Auth;

namespace OnMed.WebApi.Controllers.Common.Users
{
    [Route("api/common/auth")]
    [ApiController]
    public class AuthController : CommonBaseController
    {
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromForm] RegisterDto registerDto)
        {
        }
    }
}
