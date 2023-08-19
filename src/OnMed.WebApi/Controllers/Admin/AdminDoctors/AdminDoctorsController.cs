using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnMed.Application.Utils;
using OnMed.Persistance.Dtos.Doctors;
using OnMed.Persistance.Validators.Dtos.Doctors;
using OnMed.Service.Interfaces.Doctors;

namespace OnMed.WebApi.Controllers.Admin.AdminDoctors;

[Route("api/[controller]")]
[ApiController]
public class AdminDoctorsController : AdminBaseController
{
    private readonly IDoctorService _service;
    private readonly int maxPageSize = 20;

    public AdminDoctorsController(IDoctorService doctorService)
    {
        this._service = doctorService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(long hospitalId, [FromQuery] int page = 1)
    => Ok(await _service.GetAllByHospitalAsync(hospitalId, new PaginationParams(page, maxPageSize)));

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] DoctorCreateDto dto)
    {
        var createValidator = new DoctorCreateValidator();
        var result = createValidator.Validate(dto);
        if (result.IsValid) return Ok(await _service.CreateAsync(dto));
        else return BadRequest(result.Errors);
    }

    [HttpGet("count")]
    [AllowAnonymous]
    public async Task<IActionResult> CountAsync([FromQuery] long hospitalId)
        => Ok(await _service.CountByHospitalAsync(hospitalId));

}
