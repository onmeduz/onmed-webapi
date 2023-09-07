using Microsoft.AspNetCore.Mvc;
using OnMed.Persistance.Dtos.Administrators;
using OnMed.Persistance.Dtos.Users;
using OnMed.Persistance.Validators.Dtos.Administrators;
using OnMed.Persistance.Validators.Dtos.Users;
using OnMed.Service.Interfaces.Administrators;
using OnMed.Service.Services.Administrators;

namespace OnMed.WebApi.Controllers.Admin;

[Route("api/admin/profile")]
[ApiController]
public class AdminProfileController : AdminBaseController
{
    private readonly IAdministratorsService _adminService;

    public AdminProfileController(IAdministratorsService administratorsService)
    {
        this._adminService = administratorsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUserInfoAsync()
    => Ok(await _adminService.GetProfileInfoAsync());

    [HttpPut("upload/image")]
    public async Task<IActionResult> UploadImageAsync([FromForm] UploadImageDto file)
    {
        var validator = new UploadImageValidator();
        var valResult = validator.Validate(file);
        if (valResult.IsValid == false) return BadRequest(valResult.Errors);

        return Ok(await _adminService.UpdateImageAsync(file));
    }
}
