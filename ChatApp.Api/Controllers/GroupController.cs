using ChatApp.Service.Contracts;
using ChatApp.Shared.DataTransferObjects.Groups;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly IServiceManager _service;

        public GroupController(IServiceManager service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetGroups()
        {
            var groups = await _service.GroupService.GetGroups(trackChanges: false);
            return groups != null ? Ok(groups) : NotFound();
        }

        [HttpGet("{groupId}")]
        public async Task<IActionResult> GetGroup(int groupId)
        {
            var group = await _service.GroupService.GetGroup(groupId, trackChanges: false);
            return group != null ? Ok(group) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroup(CreateGroupDto createGroupDto)
        {
            await _service.GroupService.CreateGroup(createGroupDto);
            return StatusCode(201);
        }
    }
}
