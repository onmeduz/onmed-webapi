﻿using Microsoft.AspNetCore.Mvc;
using OnMed.Application.Utils;
using OnMed.Service.Interfaces.Categories;

namespace OnMed.WebApi.Controllers.Common.Categories
{
    [Route("api/common/categories")]
    [ApiController]
    public class CommonCategoriesController : CommonBaseController
    {
        private readonly ICategoryService _categoryService;

        public CommonCategoriesController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1, int PerPage = 10)
        => Ok(await _categoryService.GetAllAsync(new PaginationParams(page, PerPage)));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(long id)
            => Ok(await _categoryService.GetByIdAsync(id));

        [HttpGet("search")]
        public async Task<IActionResult> SearchAsync([FromQuery] string search)
            => Ok(await _categoryService.SearchAsync(search));
    }
}
