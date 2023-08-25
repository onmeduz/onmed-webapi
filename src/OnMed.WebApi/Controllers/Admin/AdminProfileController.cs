using Microsoft.AspNetCore.Mvc;
using OnMed.Service.Interfaces.Administrators;

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
}
