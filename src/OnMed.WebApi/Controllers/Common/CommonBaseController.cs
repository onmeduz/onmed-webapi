using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnMed.WebApi.Controllers.Common
{
    [ApiController]
    [AllowAnonymous]
    public abstract class CommonBaseController : ControllerBase
    {}
}
