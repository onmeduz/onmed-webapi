using Microsoft.AspNetCore.Mvc;
using OnMed.Service.Interfaces.Users;

namespace OnMed.WebApi.Controllers.Admin.Users;

[Route("api/admin/user")]
[ApiController]
public class AdminUserController : AdminBaseController
{
    private readonly IUserAppointmentService _userAppointmentService;

    public AdminUserController(IUserAppointmentService userAppointmentService)
    {
        this._userAppointmentService = userAppointmentService;
    }

    [HttpGet("patient/count/{hospitalBranchId}")]
    public async Task<IActionResult> CountByHospitalIdAsync(long hospitalBranchId)
    => Ok(await _userAppointmentService.CountByHospitalIdAsync(hospitalBranchId));
}
