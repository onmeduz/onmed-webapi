using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnMed.WebApi.Controllers.Common.Doctors;

[Route("api/common/doctors")]
[ApiController]
public class CommonDoctorsController : CommonBaseController
{
    [HttpGet]
    public IActionResult Get() => Ok();

    [HttpGet("{id}")]
    public IActionResult Get(long id) => Ok(id);

}
