using Microsoft.AspNetCore.Mvc;
using OnMed.Application.Utils;
using OnMed.Persistance.Dtos.Users;
using OnMed.Persistance.Validators.Dtos.Users;
using OnMed.Service.Interfaces.Users;

namespace OnMed.WebApi.Controllers.Head.Users;

[Route("api/head/users")]
[ApiController]
public class HeadUserController : HeadBaseController
{
    private readonly IUserService _userService;
    private readonly int maxPageSize = 30;

    public HeadUserController(IUserService userService)
    {
        this._userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _userService.GetAllAsync(new PaginationParams(page, maxPageSize)));

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(long userId, [FromForm] UserUpdateDto dto)
    {
        var updateValidator = new UserUpdateValidator();
        var validationResult = updateValidator.Validate(dto);
        if (validationResult.IsValid) return Ok(await _userService.UpdateAsync(userId, dto));
        else return BadRequest(validationResult.Errors);
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteAsync(long userId)
        => Ok(await _userService.DeleteAsync(userId));
}
