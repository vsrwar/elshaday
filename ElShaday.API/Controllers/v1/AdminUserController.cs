using System.Net;
using ElShaday.Application.DTOs.Requests;
using ElShaday.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApplicationException = ElShaday.Application.Configuration.ApplicationException;

namespace ElShaday.API.Controllers.v1;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
[AllowAnonymous]
public class AdminUserController : ControllerBase
{
    private readonly IAdminUserService _service;

    public AdminUserController(IAdminUserService service)
    {
        _service = service;
    }

    /// <summary>
    /// Creates a new Admin User
    /// </summary>
    /// <param name="adminUserRequestDto">Email and Nickname</param>
    /// <returns>201 (created) Status code</returns>
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] AdminUserRequestDto adminUserRequestDto)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            var created = await _service.CreateAsync(adminUserRequestDto);
            return Created(created.Id.ToString(), created);
        }
        catch (ApplicationException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message, nameof(CreateAsync), (int)HttpStatusCode.InternalServerError);
        }
    }
    
    /// <summary>
    /// Gets an Admin User by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Admin user</returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        try
        {
            var adminUser = await _service.GetByIdAsync(id);
            if (adminUser is null)
                return NotFound($"Admin User id: {id} not found");
            return Ok(adminUser);
        }
        catch (Exception e)
        {
            return Problem(e.Message, nameof(GetByIdAsync), (int)HttpStatusCode.InternalServerError);
        }
    }
    
    /// <summary>
    /// Gets paginated Admin Users (default page: 1, default pageSize: 25)
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns>A list of Admin User paginated</returns>
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
    /// Updated an Admin User
    /// </summary>
    /// <param name="adminUserRequestDto"></param>
    /// <returns>Updated Admin User</returns>
    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] AdminUserRequestDto adminUserRequestDto)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            var updated = await _service.UpdateAsync(adminUserRequestDto);
            return Ok(updated);
        }
        catch (ApplicationException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message, nameof(UpdateAsync), (int)HttpStatusCode.InternalServerError);
        }
    }
    
    /// <summary>
    /// Deletes (softly) an Admin User by Id
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
    /// Deactivates an Admin User by Id
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
        catch (ApplicationException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message, nameof(DeactivateAsync), (int)HttpStatusCode.InternalServerError);
        }
    }

    /// <summary>
    /// Activates an Admin User by Id
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
        catch (ApplicationException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message, nameof(DeactivateAsync), (int)HttpStatusCode.InternalServerError);
        }
    }
}