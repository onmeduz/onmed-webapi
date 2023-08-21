using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnMed.Application.Utils;
using OnMed.Service.Interfaces.Categories;
using System.ComponentModel;

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
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1,int PerPage = 10)
        => Ok(await _categoryService.GetAllAsync(new PaginationParams(page, PerPage)));

        [HttpGet("{id}")]
        public IActionResult Get(long id) => Ok(id);

    }
}
