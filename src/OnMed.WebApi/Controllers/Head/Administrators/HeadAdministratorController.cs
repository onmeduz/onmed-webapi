﻿using Microsoft.AspNetCore.Mvc;
using OnMed.Application.Utils;
using OnMed.Persistance.Dtos.Administrators;
using OnMed.Persistance.Validators.Dtos.Administrators;
using OnMed.Service.Interfaces.Administrators;

namespace OnMed.WebApi.Controllers.Head.Administrators;

[Route("api/head/administrators")]
[ApiController]
public class HeadAdministratorController : HeadBaseController
{
    private readonly IAdministratorsService _administratorService;
    private readonly int maxPageSize = 30;

    public HeadAdministratorController(IAdministratorsService administratorsService)
    {
        _administratorService = administratorsService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] AdministratorCreateDto dto)
    {
        var createValidator = new AdministratorCreateValidator();
        var result = createValidator.Validate(dto);
        if (result.IsValid) return Ok(await _administratorService.CreateAsync(dto));
        else return BadRequest(result.Errors);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _administratorService.GetAllAsync(new PaginationParams(page, maxPageSize)));

    [HttpGet("search")]
    public async Task<IActionResult> SearchAsync([FromQuery] string search)
        => Ok(await _administratorService.SearchAsync(search));

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(long administratorId, [FromForm] AdministratorUpdateDto dto)
    {
        var updateValidator = new AdministratorUpdateValidator();
        var validationResult = updateValidator.Validate(dto);
        if (validationResult.IsValid) return Ok(await _administratorService.UpdateAsync(administratorId, dto));
        else return BadRequest(validationResult.Errors);
    }

    [HttpDelete("{administratorId}")]
    public async Task<IActionResult> DeleteAsync(long administratorId)
        => Ok(await _administratorService.DeleteAsync(administratorId));
}
