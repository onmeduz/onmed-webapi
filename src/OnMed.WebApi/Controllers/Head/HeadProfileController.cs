using Microsoft.AspNetCore.Mvc;
using OnMed.Service.Interfaces.Heads;

namespace OnMed.WebApi.Controllers.Head;

[Route("api/[controller]")]
[ApiController]
public class HeadProfileController : HeadBaseController
{
    private readonly IHeadService _headService;

    public HeadProfileController(IHeadService headService)
    {
        this._headService = headService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUserInfoAsync()
        => Ok(await _headService.GetProfileInfoAsync());

}
