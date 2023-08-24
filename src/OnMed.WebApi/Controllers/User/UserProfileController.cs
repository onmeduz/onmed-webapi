using Microsoft.AspNetCore.Mvc;
using OnMed.Persistance.Dtos.Auth;
using OnMed.Persistance.Dtos.Users;
using OnMed.Persistance.Validators.Dtos.Auth;
using OnMed.Persistance.Validators.Dtos.Users;
using OnMed.Service.Interfaces.Users;

namespace OnMed.WebApi.Controllers.User;

[Route("api/user/profile")]
[ApiController]
public class UserProfileController : UserBaseController
{
    private readonly IUserProfileService _userService;

    public UserProfileController(IUserProfileService userProfileService)
    {
        this._userService = userProfileService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUserInfoAsync()
        => Ok(await _userService.GetProfileInfoAsync());


    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] UserUpdateDto dto)
    {
        var updateValidator = new UserUpdateValidator();
        var result = updateValidator.Validate(dto);
        if (result.IsValid) return Ok(await _userService.UpdateAsync(dto));
        else return BadRequest(result.Errors);
    }

    [HttpPut("reset/password")]
    public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordDto dto)
    {
        var validator = new ResetPasswordValidator();
        var valResult = validator.Validate(dto);
        if (valResult.IsValid == false) return BadRequest(valResult.Errors);

        return Ok(await _userService.ResetPasswordAsync(dto));
    }

    [HttpDelete("delete/image")]
    public async Task<IActionResult> DeleteImageAsync()
        => Ok(await _userService.DeleteImageAsync());

    [HttpPut("upload/image")]
    public async Task<IActionResult> UploadImageAsync([FromForm] UploadImageDto file)
    {
        var validator = new UploadImageValidator();
        var valResult = validator.Validate(file);
        if (valResult.IsValid == false) return BadRequest(valResult.Errors);

        return Ok(await _userService.UploadImageAsync(file));
    }
}
