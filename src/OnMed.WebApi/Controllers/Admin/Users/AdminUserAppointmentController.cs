using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnMed.Service.Interfaces.Users;

namespace OnMed.WebApi.Controllers.Admin.Users
{
    [Route("api/admin/user")]
    [ApiController]
    public class AdminUserAppointmentController : ControllerBase
    {
        private readonly IUserAppointmentService _userAppointmentService;

        public AdminUserAppointmentController(IUserAppointmentService userAppointmentService)
        {
            this._userAppointmentService = userAppointmentService;
        }

        [HttpGet("patient")]
        public async Task<IActionResult> GetAllByMomentAsync(int moment)
            => Ok(await _userAppointmentService.GetAllByMomentAsync(moment));
    }
}
