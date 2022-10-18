using ChatApp.Service.Contracts;
using ChatApp.Shared.DataTransferObjects.Conversations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ConversationController : ControllerBase
    {
        private readonly IServiceManager _service;

        public ConversationController(IServiceManager service)
        {
            _service = service;
        }

        [HttpPost("{recipientUsername}")]
        public async Task<IActionResult> CreateConversation(string recipientUsername)
        {

            await _service.ConversationService.CreateConversation(recipientUsername);
            return StatusCode(201);

        }
        [HttpGet("userConversations"), Authorize]
        public async Task<ActionResult<List<ConversationDto>>> GetAllUserConversations()
        {
            var conversations = await _service.ConversationService.GetAllUserConversations(trackChanges: true);

            return conversations != null ? Ok(conversations) : NotFound("No conversations found!");
        }

        [HttpGet("{conversationId}"), Authorize(Policy = "ConversationMessageRequirements")]
        public async Task<IActionResult> GetConversation(int conversationId)
        {
            var conversation = await _service.ConversationService
                .GetConversation(conversationId, trackChanges: false);

            return conversation != null ? Ok(conversation) : NotFound("No conversation found.");
        }
    }
}
