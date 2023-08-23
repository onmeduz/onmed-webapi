using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnMed.Application.Utils;
using OnMed.Service.Interfaces.Doctors;
using OnMed.Service.Interfaces.Hospitals;

namespace OnMed.WebApi.Controllers.Common.Hospital;

[Route("api/common/hospital/branch")]
[ApiController]
public class CommonHospitalBranchController : CommonBaseController
{
    private readonly IDoctorService _service;
    private readonly IHospitalBranchService _hospitalBranchService;
    private readonly int maxPageSize = 20;

    public CommonHospitalBranchController(IDoctorService doctorService,
        IHospitalBranchService hospitalBranchService)
    {
        this._service = doctorService;
        this._hospitalBranchService = hospitalBranchService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllForCommonAsync([FromQuery] int page = 1)
        => Ok(await _hospitalBranchService.GetAllForCommonAsync(new PaginationParams(page, maxPageSize)));

    [HttpGet("doctors/{hospitalId}")]
    public async Task<IActionResult> GetAllAsync(long hospitalId, [FromQuery]long? categoryId = null, [FromQuery] int page = 1)
        => Ok(await _service.GetAllByHospitalAsync(hospitalId,categoryId, new PaginationParams(page, maxPageSize)));

    [HttpGet("doctors/count")]
    [AllowAnonymous]
    public async Task<IActionResult> CountDoctorAsync([FromQuery] long hospitalId)
        => Ok(await _service.CountByHospitalAsync(hospitalId));
}
