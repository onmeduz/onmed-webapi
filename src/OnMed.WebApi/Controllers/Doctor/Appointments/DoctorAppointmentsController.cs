using Microsoft.AspNetCore.Mvc;

namespace OnMed.WebApi.Controllers.Doctor.Appointments
{
    [Route("api/doctor/appointments")]
    [ApiController]
    public class DoctorAppointmentsController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get() => Ok();

        [HttpGet("{id}")]
        public IActionResult Get(long id) => Ok(id);

    }
}
