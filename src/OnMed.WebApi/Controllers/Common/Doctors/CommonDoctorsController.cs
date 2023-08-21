using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnMed.Application.Utils;
using OnMed.Persistance.Dtos.Auth;
using OnMed.Persistance.Validators.Dtos.Auth;
using OnMed.Service.Interfaces.Doctors;

namespace OnMed.WebApi.Controllers.Common.Doctors;

[Route("api/common/doctors")]
[ApiController]
public class CommonDoctorsController : CommonBaseController
{
    private readonly IDoctorService _doctorService;
    private readonly int maxPageSize = 20;

    public CommonDoctorsController(IDoctorService doctorService)
    {
        this._doctorService = doctorService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _doctorService.GetAllAsync(new PaginationParams(page, maxPageSize)));


    [HttpGet("{doctorId}")]
    public async Task<IActionResult> GetByIdAsync(long doctorId)
        => Ok(await _doctorService.GetByIdAsync(doctorId));

    [HttpGet("count")]
    public async Task<IActionResult> CountAsync()
        => Ok(await _doctorService.CountAsync());


}
