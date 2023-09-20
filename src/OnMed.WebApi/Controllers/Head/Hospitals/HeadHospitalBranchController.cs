using Microsoft.AspNetCore.Mvc;
using OnMed.Application.Utils;
using OnMed.Persistance.Dtos.Hospitals;
using OnMed.Persistance.Validators.Dtos.Hospitals;
using OnMed.Service.Interfaces.Hospitals;

namespace OnMed.WebApi.Controllers.Head.Hospitals;

[Route("api/head/hospital-branch")]
[ApiController]
public class HeadHospitalBranchController : HeadBaseController
{
    private readonly IHospitalBranchService _hospitalBranchService;
    private readonly int maxPageSize = 30;

    public HeadHospitalBranchController(IHospitalBranchService hospitalBranchService)
    {
        this._hospitalBranchService = hospitalBranchService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] HospitalBranchCreateDto dto)
    {
        var createValidator = new HospitalBranchCreateValidator();
        var result = createValidator.Validate(dto);
        if (result.IsValid) return Ok(await _hospitalBranchService.CreateAsync(dto));
        else return BadRequest(result.Errors);
    }

    [HttpPut("{hospitalBranchId}")]
    public async Task<IActionResult> UpdateAsync(long hospitalBranchId, [FromForm] HospitalBranchUpdateDto dto)
    {
        var updateValidator = new HospitalBranchUpdateValidator();
        var validationResult = updateValidator.Validate(dto);
        if (validationResult.IsValid) return Ok(await _hospitalBranchService.UpdateAsync(hospitalBranchId, dto));
        else return BadRequest(validationResult.Errors);
    }

    [HttpDelete("{hospitalBranchId}")]
    public async Task<IActionResult> DeleteAsync(long hospitalBranchId)
        => Ok(await _hospitalBranchService.DeleteAsync(hospitalBranchId));

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _hospitalBranchService.GetAllAsync(new PaginationParams(page, maxPageSize)));

    [HttpGet("{search}")]
    public async Task<IActionResult> SearchAsync(string search)
        => Ok(await _hospitalBranchService.SearchAsync(search));

    [HttpGet("hospital-branch")]
    public async Task<IActionResult> GetByHospitalIdAsync(long hospitalId)
    {
        return Ok(await _hospitalBranchService.GetByHospitalIdAsync(hospitalId));
    }
}
