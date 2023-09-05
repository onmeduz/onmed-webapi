using Microsoft.AspNetCore.Mvc;
using OnMed.Persistance.Dtos.Auth;
using OnMed.Persistance.Dtos.Heads;
using OnMed.Persistance.Dtos.Users;
using OnMed.Persistance.Validators.Dtos.Auth;
using OnMed.Persistance.Validators.Dtos.Users;
using OnMed.Service.Interfaces.Heads;

namespace OnMed.WebApi.Controllers.Head;

[Route("api/head/profile")]
[ApiController]
public class HeadProfileController : HeadBaseController
{
    private readonly IHeadProfileService _headService;

    public HeadProfileController(IHeadProfileService headService)
    {
        this._headService = headService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUserInfoAsync()
        => Ok(await _headService.GetProfileInfoAsync());

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] HeadUpdateDto dto)
    {
        var updateValidator = new HeadUpdateValidator();
        var result = updateValidator.Validate(dto);
        if (result.IsValid) return Ok(await _headService.UpdateAsync(dto));
        else return BadRequest(result.Errors);
    }

    [HttpPut("reset/password")]
    public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordDto dto)
    {
        var validator = new ResetPasswordValidator();
        var valResult = validator.Validate(dto);
        if (valResult.IsValid == false) return BadRequest(valResult.Errors);

        return Ok(await _headService.ResetPasswordAsync(dto));
    }

    [HttpDelete("delete/image")]
    public async Task<IActionResult> DeleteImageAsync()
        => Ok(await _headService.DeleteImageAsync());

    [HttpPut("upload/image")]
    public async Task<IActionResult> UploadImageAsync([FromForm] UploadImageDto file)
    {
        var validator = new UploadImageValidator();
        var valResult = validator.Validate(file);
        if (valResult.IsValid == false) return BadRequest(valResult.Errors);

        return Ok(await _headService.UploadImageAsync(file));
    }
}
