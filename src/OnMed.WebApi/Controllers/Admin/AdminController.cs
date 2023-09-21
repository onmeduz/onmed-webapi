using Microsoft.AspNetCore.Mvc;
using OnMed.Persistance.Dtos.Administrators;
using OnMed.Persistance.Validators.Dtos.Administrators;
using OnMed.Service.Interfaces.Administrators;

namespace OnMed.WebApi.Controllers.Admin
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : AdminBaseController
    {
        private readonly IAdministratorsService _adminService;

        public AdminController(IAdministratorsService administratorsService)
        {
            this._adminService = administratorsService;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(long adminId, [FromForm] AdministratorUpdateDto dto)
        {
            var updateValidator = new AdministratorUpdateValidator();
            var validationResult = updateValidator.Validate(dto);
            if (validationResult.IsValid) return Ok(await _adminService.UpdateAsync(adminId, dto));
            else return BadRequest(validationResult.Errors);
        }
    }
}
