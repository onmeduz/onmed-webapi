﻿using Microsoft.AspNetCore.Mvc;
using OnMed.Persistance.Dtos.Categories;
using OnMed.Persistance.Validators.Dtos.Categories;
using OnMed.Service.Interfaces.Categories;

namespace OnMed.WebApi.Controllers.Head.Categories;

[Route("api/head/categories")]
[ApiController]
public class HeadCategoryController : HeadBaseController
{
    private readonly ICategoryService _categoryService;

    public HeadCategoryController(ICategoryService categoryService)
    {
        this._categoryService = categoryService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] CategoryCreateDto dto)
    {
        var createValidator = new CategoryCreateValidator();
        var result = createValidator.Validate(dto);
        if (result.IsValid) return Ok(await _categoryService.CreateAsync(dto));
        else return BadRequest(result.Errors);
    }

    [HttpPut("{categoryId}")]
    public async Task<IActionResult> UpdateAsync(long categoryId, [FromForm] CategoryUpdateDto dto)
    {
        var updateValidator = new CategoryUpdateValidator();
        var validationResult = updateValidator.Validate(dto);
        if (validationResult.IsValid) return Ok(await _categoryService.UpdateAsync(categoryId, dto));
        else return BadRequest(validationResult.Errors);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(long categoryId)
        => Ok(await _categoryService.DeleteAsync(categoryId));

    [HttpGet("count")]
    public async Task<IActionResult> CountAsync()
    => Ok(await _categoryService.CountAsync());

    // Get All Common controllerda.
    // Search Common controllerda.
}
