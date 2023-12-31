﻿using System.Net;
using ElShaday.API.Configuration;
using ElShaday.Application.DTOs.Requests;
using ElShaday.Application.Interfaces;
using ElShaday.Domain.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElShaday.API.Controllers.v1;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
[Authorize(Policy.Administrator)]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    /// <summary>
    /// Creates a new User
    /// </summary>
    /// <param name="userRequestDto">Email and Nickname</param>
    /// <returns>201 (created) Status code</returns>
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] UserRequestDto userRequestDto)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            var created = await _service.CreateAsync(userRequestDto);
            return Created(created.Id.ToString(), created);
        }
        catch (BusinessException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message, nameof(CreateAsync), (int)HttpStatusCode.InternalServerError);
        }
    }
    
    /// <summary>
    /// Gets an User by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>a User with id</returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        try
        {
            var user = await _service.GetByIdAsync(id);
            if (user is null)
                return NotFound($"User id: {id} not found");
            return Ok(user);
        }
        catch (Exception e)
        {
            return Problem(e.Message, nameof(GetByIdAsync), (int)HttpStatusCode.InternalServerError);
        }
    }
    
    /// <summary>
    /// Gets paginated Users (default page: 1, default pageSize: 25)
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns>A list of Users paginated</returns>
    [HttpGet]
    public async Task<IActionResult> GetAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 25)
    {
        try
        {
            var paged = await _service.GetAsync(page, pageSize);
            return Ok(paged);
        }
        catch (Exception e)
        {
            return Problem(e.Message, nameof(GetAsync), (int)HttpStatusCode.InternalServerError);
        }
    }
    
    /// <summary>
    /// Updates an User
    /// </summary>
    /// <param name="userRequestDto"></param>
    /// <returns>Updated User</returns>
    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] UserEditRequestDto userRequestDto)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            var updated = await _service.UpdateAsync(userRequestDto);
            return Ok(updated);
        }
        catch (BusinessException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message, nameof(UpdateAsync), (int)HttpStatusCode.InternalServerError);
        }
    }
    
    /// <summary>
    /// Deletes (softly) an User by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>204 - No Content</returns>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception e)
        {
            return Problem(e.Message, nameof(DeleteAsync), (int)HttpStatusCode.InternalServerError);
        }
    }

    /// <summary>
    /// Deactivates an User by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>204 - No Content</returns>
    [HttpPost("deactivate/{id:int}")]
    public async Task<IActionResult> DeactivateAsync(int id)
    {
        try
        {
            await _service.DeactivateAsync(id);
            return NoContent();
        }
        catch (BusinessException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message, nameof(DeactivateAsync), (int)HttpStatusCode.InternalServerError);
        }
    }

    /// <summary>
    /// Activates an User by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>204 - No Content</returns>
    [HttpPost("activate/{id:int}")]
    public async Task<IActionResult> ActivateAsync(int id)
    {
        try
        {
            await _service.ActivateAsync(id);
            return NoContent();
        }
        catch (BusinessException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message, nameof(DeactivateAsync), (int)HttpStatusCode.InternalServerError);
        }
    }

        
    /// <summary>
    /// Returns the count of active Users
    /// </summary>
    /// <returns></returns>
    [HttpGet("count-actives")]
    public async Task<IActionResult> CountActivesAsync()
    {
        try
        {
            int count = await _service.CountActivesAsync();
            return Ok(count);
        }
        catch (Exception e)
        {
            return Problem(e.Message, nameof(CountActivesAsync), (int)HttpStatusCode.InternalServerError);
        }
    }

    /// <summary>
    /// Changes a user's password 
    /// </summary>
    /// <returns></returns>
    [HttpPost("change-password")]
    [AllowAnonymous]
    public async Task<IActionResult> ChangePassword([FromBody] ChangeUserPasswordDto changeUserPasswordDto)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            bool changed = await _service.ChangePasswordAsync(changeUserPasswordDto);
            return Ok(changed);
        }
        catch (BusinessException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message, nameof(ChangePassword), (int)HttpStatusCode.InternalServerError);
        }
    }
}