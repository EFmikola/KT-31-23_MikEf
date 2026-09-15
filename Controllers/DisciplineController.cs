using Microsoft.AspNetCore.Mvc;
using ProjectPractice.Filters;
using ProjectPractice.Models;
using ProjectPractice.Services;

namespace ProjectPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisciplinesController : ControllerBase
    {
        private readonly ILogger<DisciplinesController> _logger;
        private readonly IDisciplineService _DisciplineService;

        public DisciplinesController(ILogger<DisciplinesController> logger, IDisciplineService DisciplineService)
        {
            _logger = logger;
            _DisciplineService = DisciplineService;
        }

        [HttpPost("search")]
        public async Task<ActionResult<List<Discipline>>> GetDisciplinesAsync([FromBody] DisciplineFilter filter)
        {
            var Disciplines = await _DisciplineService.GetDisciplinesAsync(filter);
            return Ok(Disciplines);
        }
    }
}
