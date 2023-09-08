using Microsoft.AspNetCore.Mvc;
using OnMed.Persistance.Dtos.Hospitals;
using OnMed.Service.Interfaces.Hospitals;

namespace OnMed.WebApi.Controllers.Admin.AdminHospitals;



[Route("api/hospital-schedule")]
[ApiController]
public class AdminHospitalScheduleController : ControllerBase
{
    private readonly IHospitalScheduleService _hospitalScheduleService;

    public AdminHospitalScheduleController(IHospitalScheduleService hospitalSchedule)
    {
        this._hospitalScheduleService = hospitalSchedule;
    }

    [HttpPost]
    public async Task<IActionResult> HospitalScheduleCreateAsync([FromForm] HospitalScheduleCreateDto dto)
    {
        return Ok(await _hospitalScheduleService.CreateAsync(dto));
    }
}
