using Microsoft.AspNetCore.Mvc;
using ProjectPractice.Dtos;
using ProjectPractice.Filters;
using ProjectPractice.Requests;
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




    [HttpPost]
    public async Task<ActionResult<int>> CreateGradeAsync([FromBody] CreateGradeRequest request)
    {
        var gradeId = await _gradeService.CreateGradeAsync(request);

        if (gradeId is null)
        {
            return BadRequest("Студент или дисциплина не найдена.");
        }

        return Ok(gradeId.Value);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateGradeAsync([FromBody] UpdateGradeRequest request)
    {
        var updated = await _gradeService.UpdateGradeAsync(request);

        return updated ? NoContent() : NotFound();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteGradeAsync([FromBody] DeleteGradeRequest request)
    {
        var deleted = await _gradeService.DeleteGradeAsync(request);

        return deleted ? NoContent() : NotFound();
    }


    [HttpGet("debts")]
    public async Task<ActionResult<List<StudentDebtDto>>> GetStudentDebtsAsync([FromQuery] StudentDebtFilter filter)
    {
        var debts = await _gradeService.GetStudentDebtsAsync(filter);

        return Ok(debts);
    }
}