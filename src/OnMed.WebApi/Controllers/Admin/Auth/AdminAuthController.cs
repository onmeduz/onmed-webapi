using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnMed.Persistance.Dtos.Auth;
using OnMed.Persistance.Validators.Dtos.Auth;

namespace OnMed.WebApi.Controllers.Admin.Auth;

[Route("api/admin/auth")]
[ApiController]
public class AdminAuthController : ControllerBase
{
    public AdminAuthController()
    {
        
    }
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginDto loginDto)
    {
        var validator = new LoginValidator();
        var valResult = validator.Validate(loginDto);
        if (valResult.IsValid == false) return BadRequest(valResult.Errors);
        var serviceResult = await _adminService.LoginAsync(loginDto);

        return Ok(new { serviceResult.Result, serviceResult.Token });
    }
}
