using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnMed.Application.Utils;
using OnMed.Persistance.Dtos.Hospitals;
using OnMed.Persistance.Validators.Dtos.Hospitals;
using OnMed.Service.Interfaces.Hospitals;
using System.Data;

namespace OnMed.WebApi.Controllers.Head.Hospitals;

[Route("api/head/hospitals")]
[ApiController]
public class HeadHospitalController : HeadBaseController
{
    private readonly IHospitalService _hospitalService;
    private readonly int maxPageSize = 30;

    public HeadHospitalController(IHospitalService hospitalService)
    {
        this._hospitalService = hospitalService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] HospitalCreateDto dto)
    {
        var createValidator = new HospitalCreateValidator();
        var result = createValidator.Validate(dto);
        if (result.IsValid) return Ok(await _hospitalService.CreateAsync(dto));
        else return BadRequest(result.Errors);
    }

    [HttpPut("{hospitalId}")]
    public async Task<IActionResult> UpdateAsync(long hospitalId, [FromForm] HospitalUpdateDto dto)
    {
        var updateValidator = new HospitalUpdateValidator();
        var validationResult = updateValidator.Validate(dto);
        if (validationResult.IsValid) return Ok(await _hospitalService.UpdateAsync(hospitalId, dto));
        else return BadRequest(validationResult.Errors);
    }

    [HttpGet("count")]
    public async Task<IActionResult> CountAsync()
    => Ok(await _hospitalService.CountAsync());

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _hospitalService.GetAllAsync(new PaginationParams(page, maxPageSize)));

    [HttpDelete("{categoryId}")]
    public async Task<IActionResult> DeleteAsync(long categoryId)
        => Ok(await _hospitalService.DeleteAsync(categoryId));

}
