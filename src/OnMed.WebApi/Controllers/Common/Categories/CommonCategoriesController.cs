using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnMed.WebApi.Controllers.Common.Categories
{
    [Route("api/common/categories")]
    [ApiController]
    public class CommonCategoriesController : CommonBaseController
    {
        [HttpGet]
        public IActionResult Get() => Ok();

        [HttpGet("{id}")]
        public IActionResult Get(long id) => Ok(id);

    }
}
