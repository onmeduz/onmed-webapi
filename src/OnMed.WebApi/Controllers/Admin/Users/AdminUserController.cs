using Microsoft.AspNetCore.Mvc;
using OnMed.Service.Interfaces.Hospitals;
using OnMed.Service.Interfaces.Users;

namespace OnMed.WebApi.Controllers.Admin.Users;

[Route("api/admin/users")]
[ApiController]
public class AdminUserController : AdminBaseController
{
    private readonly IUserAppointmentService _userAppointmentService;
    private readonly IHospitalBranchService _hospitalBranchService;

    public AdminUserController(IUserAppointmentService userAppointmentService,
        IHospitalBranchService hospitalBranchService)
    {
        this._userAppointmentService = userAppointmentService;
        this._hospitalBranchService = hospitalBranchService;
    }

    [HttpGet("patient/count/{hospitalBranchId}")]
    public async Task<IActionResult> CountByHospitalIdAsync(long hospitalBranchId)
    => Ok(await _userAppointmentService.CountByHospitalIdAsync(hospitalBranchId));

    [HttpGet("patient/last-week/{hospitalBranchId}")]
    public async Task<IActionResult> GetHospitalAppointmentFromLastWeek(long hospitalBranchId)
    {
        return Ok(await _hospitalBranchService.GetLastWeekAppointmentCount(hospitalBranchId));
    }
}
