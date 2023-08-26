using Microsoft.AspNetCore.Mvc;
using OnMed.Application.Utils;
using OnMed.Service.Interfaces.Doctors;
using OnMed.Service.Interfaces.Users;

namespace OnMed.WebApi.Controllers.Common.Doctors;

[Route("api/common/doctors")]
[ApiController]
public class CommonDoctorsController : CommonBaseController
{
    private readonly IDoctorService _doctorService;
    private readonly IUserAppointmentService _userAppointmentService;
    private readonly int maxPageSize = 20;

    public CommonDoctorsController(IDoctorService doctorService,
        IUserAppointmentService userAppointmentService)
    {
        this._doctorService = doctorService;
        this._userAppointmentService = userAppointmentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _doctorService.GetAllAsync(new PaginationParams(page, maxPageSize)));

    [HttpGet("{doctorId}/{date}")]
    public async Task<IActionResult> GetByDateAndDoctorIdAsync(long doctorId, DateOnly date)
        => Ok(await _userAppointmentService.GetByDateAndDoctorIdAsync(doctorId, date));

    [HttpGet("{doctorId}")]
    public async Task<IActionResult> GetByIdAsync(long doctorId)
        => Ok(await _doctorService.GetByIdAsync(doctorId));

    [HttpGet("count")]
    public async Task<IActionResult> CountAsync()
        => Ok(await _doctorService.CountAsync());

    [HttpGet("search")]
    public async Task<IActionResult> SearchAsync([FromQuery]string search)
        => Ok(await _doctorService.SearchAsync(search));
}
