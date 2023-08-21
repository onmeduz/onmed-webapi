using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnMed.Application.Utils;
using OnMed.Service.Interfaces.Doctors;

namespace OnMed.WebApi.Controllers.Common.Hospital;

[Route("api/common/hospital/branch")]
[ApiController]
public class CommonHospitalBranchController : CommonBaseController
{
    private readonly IDoctorService _service;
    private readonly int maxPageSize = 20;

    public CommonHospitalBranchController(IDoctorService doctorService)
    {
        this._service = doctorService;
    }

    [HttpGet("doctors")]
    public async Task<IActionResult> GetAllAsync(long hospitalId, [FromQuery] int page = 1)
        => Ok(await _service.GetAllByHospitalAsync(hospitalId, new PaginationParams(page, maxPageSize)));

    [HttpGet("doctors/count")]
    [AllowAnonymous]
    public async Task<IActionResult> CountAsync([FromQuery] long hospitalId)
        => Ok(await _service.CountByHospitalAsync(hospitalId));
}
