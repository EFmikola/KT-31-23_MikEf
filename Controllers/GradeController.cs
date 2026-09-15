using Microsoft.AspNetCore.Mvc;
using ProjectPractice.Dtos;
using ProjectPractice.Filters;
using ProjectPractice.Services;

namespace ProjectPractice.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GradeController : ControllerBase
{
    private readonly IGradeService _gradeService;

    public GradeController(IGradeService gradeService)
    {
        _gradeService = gradeService;
    }

    [HttpPost("student")]
    public async Task<ActionResult<List<AverageGradeDto>>> GetStudentGradesAsync(
        [FromBody] StudentGradesFilter filter)
    {
        var grades = await _gradeService.GetStudentGradesAsync(filter);

        return Ok(grades);
    }

    [HttpPost("group-discipline")]
    public async Task<ActionResult<AverageGradeDto>> GetGroupDisciplineAsync(
        [FromBody] GroupDisciplineFilter filter)
    {
        var result = await _gradeService.GetGroupDisciplineAsync(filter);

        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost("course")]
    public async Task<ActionResult<List<AverageGradeDto>>> GetCourseAverageAsync(
        [FromBody] CourseAverageFilter filter)
    {
        var results = await _gradeService.GetCourseAverageAsync(filter);

        return Ok(results);
    }
}