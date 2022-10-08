using ChatApp.Service.Contracts;
using ChatApp.Shared.DataTransferObjects.Conversations;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConversationController : ControllerBase
    {
        private readonly IServiceManager _service;

        public ConversationController(IServiceManager service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateConversation(CreateConversationDto conversation)
        {
            try
            {
                await _service.ConversationService.CreateConversation(conversation);
                return StatusCode(201);
            }
            catch (Exception e)
            {
                return BadRequest($"Problem creating conversation, {e.Message}");
            }
        }

        [HttpGet("{conversationId}")]
        public async Task<IActionResult> GetConversation(int conversationId)
        {
            var conversation = await _service.ConversationService.GetConversation(conversationId, trackChanges: false);

            return conversation != null ? Ok(conversation) : NotFound("No conversation found.");
        }
    }
}
