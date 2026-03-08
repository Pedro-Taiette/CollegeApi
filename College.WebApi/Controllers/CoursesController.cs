using College.Application.ViewModels;
using College.Application.Services;
using College.Application.Exceptions;
using College.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly CourseService _courseService;
    private readonly EnrollmentService _enrollmentService;

    public CoursesController(CourseService courseService, EnrollmentService enrollmentService)
    {
        _courseService = courseService;
        _enrollmentService = enrollmentService;
    }

    /// <summary>
    /// Creates a new course
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Create([FromBody] CreateCourseViewModel viewModel)
    {
        try
        {
            var course = await _courseService.CreateAsync(viewModel);
            return Ok(course);
        }
        catch (DomainException ex)
        {
            return UnprocessableEntity(new { error = ex.Message });
        }
    }

    /// <summary>
    /// Enrolls a student in a course
    /// </summary>
    [HttpPost("{courseId}/enroll/{studentId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Enroll(Guid courseId, Guid studentId)
    {
        try
        {
            await _enrollmentService.EnrollStudentAsync(studentId, courseId);
            return NoContent();
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
}