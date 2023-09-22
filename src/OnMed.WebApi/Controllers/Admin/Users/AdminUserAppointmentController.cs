using Microsoft.AspNetCore.Mvc;
using OnMed.Service.Interfaces.Users;

namespace OnMed.WebApi.Controllers.Admin.Users;

[Route("api/admin/users")]
[ApiController]
public class AdminUserAppointmentController : ControllerBase
{
    private readonly IUserAppointmentService _userAppointmentService;

    public AdminUserAppointmentController(IUserAppointmentService userAppointmentService)
    {
        this._userAppointmentService = userAppointmentService;
    }

    [HttpGet("patients")]
    public async Task<IActionResult> GetAllByMomentAsync(int moment)
        => Ok(await _userAppointmentService.GetAllByMomentAsync(moment));

    [HttpGet("search")]
    public async Task<IActionResult> SearchAsync(long branchId, [FromQuery] string search)
    => Ok(await _userAppointmentService.SearchAsync(branchId, search));
}
