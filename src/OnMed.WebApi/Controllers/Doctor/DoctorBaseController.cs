using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnMed.WebApi.Controllers.Doctor
{
    [ApiController]
    [Authorize(Roles = "Doctor")]
    public class DoctorBaseController : ControllerBase
    {}
}
