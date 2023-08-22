using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnMed.WebApi.Controllers.User
{
    [ApiController]
    //[Authorize(Roles = "User")]
    public abstract class UserBaseController : ControllerBase
    {}
}
