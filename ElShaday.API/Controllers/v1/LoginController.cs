﻿using System.Net;
using ElShaday.Application.DTOs.Requests;
using ElShaday.Application.DTOs.Responses;
using ElShaday.Application.Interfaces;
using ElShaday.Domain.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElShaday.API.Controllers.v1;
[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
[AllowAnonymous]
public class LoginController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;
    public LoginController(IUserService userService, ITokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    /// <summary>
    /// Logs in a User
    /// </summary>
    /// <param name="loginRequestDto">Email and Password</param>
    /// <returns>200 - Authenticated</returns>
    /// <returns>403 - Forbidden</returns>
    [HttpPost]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDto loginRequestDto)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            var user = await _userService.VerifyLoginAsync(loginRequestDto);
            var token = _tokenService.GenerateTokenAsync(user);
            return Ok(new LoginResponseDto(user, token));
        }
        catch (BusinessException e)
        {
            return Unauthorized(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message, nameof(LoginAsync), (int)HttpStatusCode.InternalServerError);
        }
    }

    /// <summary>
    /// Checks if the User is available for Password Recovery
    /// </summary>
    /// <param name="nickName">users' nickname</param>
    /// <returns></returns>
    [HttpGet("CheckNickNameForPasswordRecovery/{nickName}")]
    public async Task<IActionResult> CheckNickNameForPasswordRecovery([FromRoute] string nickName)
    {
        try
        {
            var canChange = await _userService.CanChangePasswordAsync(nickName);
            return Ok(canChange);
        }
        catch (BusinessException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message, nameof(CheckNickNameForPasswordRecovery), (int)HttpStatusCode.InternalServerError);
        }
    }
}