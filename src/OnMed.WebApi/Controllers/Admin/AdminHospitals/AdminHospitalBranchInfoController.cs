using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnMed.Service.Interfaces.Hospitals;

namespace OnMed.WebApi.Controllers.Admin.AdminHospitals;

[Route("api/admin/hospital-branch")]
[ApiController]
public class AdminHospitalBranchInfoController : AdminBaseController
{
    private readonly IHospitalBranchService _service;

    public AdminHospitalBranchInfoController(IHospitalBranchService hospitalBranchService)
    {
        this._service = hospitalBranchService;
    }

    [HttpGet("appointments/last-week/{hospitalBranchId}")]
    public async Task<IActionResult> GetHospitalAppointmentFromLastWeek(long hospitalBranchId)
    {
        return Ok(await _service.GetLastWeekAppointmentCount(hospitalBranchId));
    }
}
