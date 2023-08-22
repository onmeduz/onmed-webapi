using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnMed.WebApi.Controllers.Admin;

[ApiController]
[Authorize(Roles = "Admin")]
public abstract class AdminBaseController : ControllerBase
{}
