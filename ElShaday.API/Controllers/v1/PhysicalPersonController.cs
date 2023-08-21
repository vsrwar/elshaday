using System.Net;
using ElShaday.Application.DTOs.Requests;
using ElShaday.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElShaday.API.Controllers.v1;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
[AllowAnonymous]
public class PhysicalPersonController : ControllerBase
{
    private readonly IPhysicalPersonService _service;

    public PhysicalPersonController(IPhysicalPersonService service)
    {
        _service = service;
    }

    /// <summary>
    /// Creates a new Physical Person
    /// </summary>
    /// <param name="physicalPersonRequestDto">Name and responsableId</param>
    /// <returns>201 (created) Status code</returns>
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] PhysicalPersonRequestDto physicalPersonRequestDto)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            var created = await _service.CreateAsync(physicalPersonRequestDto);
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
    /// Gets an Physical Person by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>a Physical Person with id</returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        try
        {
            var department = await _service.GetByIdAsync(id);
            if (department is null)
                return NotFound($"PhysicalPerson id: {id} not found");
            return Ok(department);
        }
        catch (Exception e)
        {
            return Problem(e.Message, nameof(GetByIdAsync), (int)HttpStatusCode.InternalServerError);
        }
    }
    
    /// <summary>
    /// Gets paginated Physical Persons (default page: 1, default pageSize: 25)
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns>A list of Physical Persons paginated</returns>
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
    /// Updates an Physical Person
    /// </summary>
    /// <param name="physicalPersonRequestDto"></param>
    /// <returns>Updated Physical Person</returns>
    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] PhysicalPersonRequestDto physicalPersonRequestDto)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            var updated = await _service.UpdateAsync(physicalPersonRequestDto);
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
    /// Deletes (softly) an Physical Person by Id
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
    /// returns a list of persons available to be assigned to a department
    /// </summary>
    /// <returns>A list of PhysicalPersonResponseDto</returns>
    [HttpGet("available-for-department")]
    public async Task<IActionResult> GetAvailableForDepartmentAsync()
    {
        try
        {
            var available = await _service.GetAvailableForDepartmentAsync();
            return Ok(available);
        }
        catch (Exception e)
        {
            return Problem(e.Message, nameof(GetAvailableForDepartmentAsync), (int)HttpStatusCode.InternalServerError);
        }
    }

    /// <summary>
    /// Returns the count of active Physical People
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
}