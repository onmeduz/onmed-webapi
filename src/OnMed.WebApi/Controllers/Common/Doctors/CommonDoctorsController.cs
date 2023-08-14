using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnMed.Persistance.Dtos.Auth;
using OnMed.Persistance.Validators.Dtos.Auth;
using OnMed.Service.Interfaces.Doctors;

namespace OnMed.WebApi.Controllers.Common.Doctors;

[Route("api/common/doctors")]
[ApiController]
public class CommonDoctorsController : CommonBaseController
{
    private readonly IDoctorService _doctorService;

    public CommonDoctorsController(IDoctorService doctorService)
    {
        this._doctorService = doctorService;
    }

    [HttpGet]
    public IActionResult Get() => Ok();

    [HttpGet("{id}")]
    public IActionResult Get(long id) => Ok(id);

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginDto loginDto)
    {
        var validator = new LoginValidator();
        var valResult = validator.Validate(loginDto);
        if (valResult.IsValid == false) return BadRequest(valResult.Errors);

        var serviceResult = await _doctorService.LoginAsync(loginDto);
        return Ok(new { serviceResult.Result, serviceResult.Token });
    }

}
