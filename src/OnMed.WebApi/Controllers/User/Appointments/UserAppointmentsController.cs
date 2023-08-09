using Microsoft.AspNetCore.Mvc;

namespace OnMed.WebApi.Controllers.User.Appointments
{
    [Route("api/user/appointments")]
    [ApiController]
    public class UserAppointmentsController : UserBaseController
    {
        // post appointment

        [HttpGet]
        public IActionResult Get() => Ok();

        [HttpGet("{id}")]
        public IActionResult Get(long id) => Ok(id);


    }
}
