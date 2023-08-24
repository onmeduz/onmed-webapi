using Microsoft.AspNetCore.Mvc;
using OnMed.Persistance.Dtos.Auth;
using OnMed.Persistance.Validators.Dtos.Auth;
using OnMed.Service.Interfaces.Auth;
using OnMed.WebApi.Controllers.Common;

namespace OnMed.WebApi.Controllers.Head.Auth
{
    [Route("api/head/auth")]
    [ApiController]
    public class HeadAuthController : CommonBaseController
    {
        private readonly IHeadAuthService _headAuthService;

        public HeadAuthController(IHeadAuthService headAuthService)
        {
            this._headAuthService = headAuthService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto loginDto)
        {
            var validator = new LoginValidator();
            var valResult = validator.Validate(loginDto);
            if (valResult.IsValid == false) return BadRequest(valResult.Errors);
            var serviceResult = await _headAuthService.LoginAsync(loginDto);

            return Ok(new { serviceResult.Result, serviceResult.Token });
        }
    }
}
