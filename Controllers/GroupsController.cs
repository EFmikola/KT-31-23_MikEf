using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectPractice.Filters;
using ProjectPractice.Models;
using ProjectPractice.Services;
using ProjectPractice.Requests;

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

        [HttpPost]
        public async Task<ActionResult<int>> CreateGroupAsync(
    [FromBody] CreateGroupRequest request)
        {
            var groupId = await _groupService.CreateGroupAsync(request);

            if (groupId is null)
            {
                return BadRequest("Специальность не найдена.");
            }

            return Ok(groupId.Value);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGroupAsync(
    [FromBody] UpdateGroupRequest request)
        {
            var updated = await _groupService.UpdateGroupAsync(request);

            return updated ? NoContent() : NotFound();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGroupAsync(
    [FromBody] DeleteGroupRequest request)
        {
            var deleted = await _groupService.DeleteGroupAsync(request);

            return deleted ? NoContent() : NotFound();
        }
    }
}
