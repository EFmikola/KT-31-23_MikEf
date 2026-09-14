using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectPractice.Filters;
using ProjectPractice.Models;
using ProjectPractice.Services;

namespace ProjectPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpPost("search")]
        public async Task<ActionResult<List<Group>>> GetGroupsAsync([FromBody] GroupFilter filter)
        {
            var groups = await _groupService.GetGroupsAsync(filter);
            return Ok(groups);
        }
    }
}
