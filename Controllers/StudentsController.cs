using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectPractice.Database;
using ProjectPractice.Filters;
using ProjectPractice.Models;
using ProjectPractice.Services;

namespace ProjectPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ILogger<StudentsController> _logger;
        private readonly IStudentService _StudentService;

        public StudentsController(ILogger<StudentsController> logger, IStudentService StudentService)
        {
            _logger = logger;
            _StudentService = StudentService;
        }

        [HttpPost("search")]
        public async Task<ActionResult<List<Student>>> GetStudentsAsync([FromBody] StudentFilter filter)
        {
            //_logger.LogInformation("Запрос списка студентов: GroupId={GroupId}, IsDeleted={IsDeleted}",filter.GroupId,filter.IsDeleted);
            //_logger.LogError("Запрос списка студентов: GroupId={GroupId}, IsDeleted={IsDeleted}", filter.GroupId, filter.IsDeleted);
            var Students = await _StudentService.GetStudentsAsync(filter);
            return Ok(Students);
        }
    }
}
