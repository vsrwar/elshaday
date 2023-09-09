using System.Net;
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
[Authorize]
public class LegalPersonController : ControllerBase
{
    private readonly ILegalPersonService _service;

    public LegalPersonController(ILegalPersonService service)
    {
        _service = service;
    }

    /// <summary>
    /// Creates a new Legal Person
    /// </summary>
    /// <param name="departmentRequestDto"></param>
    /// <returns>201 (created) Status code</returns>
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] LegalPersonRequestDto departmentRequestDto)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            var created = await _service.CreateAsync(departmentRequestDto);
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
    /// Gets an Legal Person by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>a Legal Person with id</returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        try
        {
            var department = await _service.GetByIdAsync(id);
            if (department is null)
                return NotFound($"LegalPerson id: {id} not found");
            return Ok(department);
        }
        catch (Exception e)
        {
            return Problem(e.Message, nameof(GetByIdAsync), (int)HttpStatusCode.InternalServerError);
        }
    }
    
    /// <summary>
    /// Gets paginated Legal Persons (default page: 1, default pageSize: 25)
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns>A list of LegalPersons paginated</returns>
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
    /// Updates an Legal Person
    /// </summary>
    /// <param name="departmentRequestDto"></param>
    /// <returns>Updated Legal Person</returns>
    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] LegalPersonRequestDto departmentRequestDto)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            var updated = await _service.UpdateAsync(departmentRequestDto);
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
    /// Deletes (softly) an Legal Person by Id
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
        catch (BusinessException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message, nameof(DeleteAsync), (int)HttpStatusCode.InternalServerError);
        }
    }
    
    /// <summary>
    /// returns a list of persons available to be assigned to a department
    /// </summary>
    /// <returns>A list of LegalPersonResponseDto</returns>
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
    /// Returns the count of active Legal People
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