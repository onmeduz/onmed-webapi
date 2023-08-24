using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnMed.WebApi.Controllers.Head
{
    [ApiController]
    [Authorize(Roles = "Head")]
    public class HeadBaseController : ControllerBase
    { }
}
