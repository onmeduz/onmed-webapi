using Microsoft.AspNetCore.Mvc;
using OnMed.Persistance.Dtos.Appointments;
using OnMed.Persistance.Validators.Dtos.Appointments;
using OnMed.Service.Interfaces.Users;

namespace OnMed.WebApi.Controllers.User.Appointments;

[Route("api/user/appointments")]
[ApiController]
public class UserAppointmentsController : UserBaseController
{
    private readonly IUserAppointmentService _userAppointmentService;

    public UserAppointmentsController(IUserAppointmentService userAppointmentService)
    {
        this._userAppointmentService = userAppointmentService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] AppointmentCreateDto dto)
    {
        var createValidator = new AppointmentCreateValidator();
        var result = createValidator.Validate(dto);
        if (result.IsValid) return Ok(await _userAppointmentService.CreateAsync(dto));
        else return BadRequest(result.Errors);
    }

    /* [HttpGet("{doctorId}/{bookingDate}")]
     public async Task<IActionResult> GetByIdDateAsync(long doctorId, string bookingDate)
     => Ok(await _bookingService.GetByIdDateAsync(doctorId, bookingDate));

     [HttpGet("{doctorId}/{time}/{date}")]
     public async Task<IActionResult> GetUserViewByDoctorIdDateTime(long doctorId, string time, string date)
         => Ok(await _bookingService.GetUserViewByDoctorIdDateTime(doctorId, time, date));*/
}
