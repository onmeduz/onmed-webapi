﻿using Microsoft.AspNetCore.Mvc;
using OnMed.Persistance.Dtos.Auth;
using OnMed.Persistance.Validators;
using OnMed.Persistance.Validators.Dtos.Auth;
using OnMed.Service.Interfaces.Auth;

namespace OnMed.WebApi.Controllers.Common.Doctors;

[Route("api/doctor-auth")]
[ApiController]
public class DoctorAuthController : ControllerBase
{
    private readonly IDoctorAuthService _service;

    public DoctorAuthController(IDoctorAuthService doctorService)
    {
        this._service = doctorService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginDto loginDto)
    {
        var validator = new LoginValidator();
        var valResult = validator.Validate(loginDto);
        if (valResult.IsValid == false) return BadRequest(valResult.Errors);
        var serviceResult = await _service.LoginAsync(loginDto);

        return Ok(new { serviceResult.Result, serviceResult.Token });
    }

    [HttpPost("reset/send-code")]
    public async Task<IActionResult> SentCodeResetPasswordAsync(string phone)
    {
        var result = PhoneNumberValidator.IsValid(phone);
        if (result == false) return BadRequest("Phone number is invalid!");
        var serviceResult = await _service.SendCodeForResetPasswordAsync(phone);

        return Ok(new { serviceResult.Result, serviceResult.CachedVerificationMinutes });
    }

    [HttpPost("reset/verify")]
    public async Task<IActionResult> VerifyResetPasswordAsync([FromBody] VerifyRegisterDto verifyRegisterDto)
    {
        var serviceResult = await _service.VerifyResetPasswordAsync(verifyRegisterDto.PhoneNumber, verifyRegisterDto.Code);

        return Ok(new { serviceResult.Result, serviceResult.Token });
    }

    [HttpPut("reset/update")]
    //[Authorize(Roles = "Doctor")]
    public async Task<IActionResult> ResetPasswordAsync([FromForm] ResetPasswordDto dto)
    {
        var validator = new ResetPasswordValidator();
        var valResult = validator.Validate(dto);
        if (valResult.IsValid == false) return BadRequest(valResult.Errors);

        return Ok(await _service.ResetPasswordAsync(dto));
    }
}