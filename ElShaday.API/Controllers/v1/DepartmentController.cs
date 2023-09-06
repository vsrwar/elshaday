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
[Authorize]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentService _service;

    public DepartmentController(IDepartmentService service)
    {
        _service = service;
    }

    /// <summary>
    /// Creates a new Department
    /// </summary>
    /// <param name="departmentForLegalPersonRequestDto">Name and responsableId</param>
    /// <returns>201 (created) Status code</returns>
    [HttpPost("for-legal-person")]
    public async Task<IActionResult> CreateForLegalPersonAsync([FromBody] DepartmentForLegalPersonRequestDto departmentForLegalPersonRequestDto)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            var created = await _service.CreateAsync(departmentForLegalPersonRequestDto);
            return Created(created.Id.ToString(), created);
        }
        catch (ApplicationException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message, nameof(CreateForLegalPersonAsync), (int)HttpStatusCode.InternalServerError);
        }
    }

    /// <summary>
    /// Creates a new Department
    /// </summary>
    /// <param name="departmentForPhysicalPersonRequestDto">Name and responsableId</param>
    /// <returns>201 (created) Status code</returns>
    [HttpPost("for-physical-person")]
    public async Task<IActionResult> CreateForPhysicalAsync([FromBody] DepartmentForPhysicalPersonRequestDto departmentForPhysicalPersonRequestDto)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            var created = await _service.CreateAsync(departmentForPhysicalPersonRequestDto);
            return Created(created.Id.ToString(), created);
        }
        catch (ApplicationException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message, nameof(CreateForPhysicalAsync), (int)HttpStatusCode.InternalServerError);
        }
    }
    
    /// <summary>
    /// Gets an Department by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>a Department with id</returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        try
        {
            var department = await _service.GetByIdAsync(id);
            if (department is null)
                return NotFound($"Department id: {id} not found");
            return Ok(department);
        }
        catch (Exception e)
        {
            return Problem(e.Message, nameof(GetByIdAsync), (int)HttpStatusCode.InternalServerError);
        }
    }
    
    /// <summary>
    /// Gets paginated Departments (default page: 1, default pageSize: 25)
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns>A list of Departments paginated</returns>
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
    /// Updates an Department
    /// </summary>
    /// <param name="departmentForLegalPersonRequestDto"></param>
    /// <returns>Updated Department</returns>
    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] DepartmentForLegalPersonRequestDto departmentForLegalPersonRequestDto)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            var updated = await _service.UpdateAsync(departmentForLegalPersonRequestDto);
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
    /// Deletes (softly) an Department by Id
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
    /// Returns the count of active Departments
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