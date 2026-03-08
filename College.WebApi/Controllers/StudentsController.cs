using College.Application.ViewModels;
using College.Application.Services;
using College.Application.Exceptions;
using College.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace College.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly StudentService _studentService;

    public StudentsController(StudentService studentService)
    {
        _studentService = studentService;
    }

    /// <summary>
    /// Enrolls a new student.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Enroll([FromBody] CreateStudentViewModel viewModel)
    {
        try
        {
            await _studentService.EnrollAsync(viewModel);
            return CreatedAtAction(nameof(GetAll), new { message = "Student successfully enrolled." });
        }
        catch (DomainException ex)
        {
            return UnprocessableEntity(new { error = ex.Message });
        }
        catch (BusinessException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    /// <summary>
    /// Retrieves all active students.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var students = await _studentService.GetAllAsync();
        return Ok(students);
    }

    /// <summary>
    /// Deactivates a student by ID (soft delete).
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Deactivate(Guid id)
    {
        try
        {
            await _studentService.DeactivateAsync(id);
            return NoContent();
        }
        catch (BusinessException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}