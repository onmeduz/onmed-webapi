using Microsoft.AspNetCore.Mvc;
using OnMed.Application.Utils;
using OnMed.Persistance.Dtos.Doctors;
using OnMed.Persistance.Validators.Dtos.Doctors;
using OnMed.Service.Interfaces.Doctors;

namespace OnMed.WebApi.Controllers.Admin.AdminDoctors;

[Route("api/admin/doctor")]
[ApiController]
public class AdminDoctorsController : AdminBaseController
{
    private readonly IDoctorService _service;
    private readonly int maxPageSize = 20;

    public AdminDoctorsController(IDoctorService doctorService)
    {
        this._service = doctorService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] DoctorCreateDto dto)
    {
        var createValidator = new DoctorCreateValidator();
        var result = createValidator.Validate(dto);
        if (result.IsValid) return Ok(await _service.CreateAsync(dto));
        else return BadRequest(result.Errors);
    }
}
