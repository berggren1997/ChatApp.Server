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

        [HttpPost("{recipientUsername}"), Authorize]
        public async Task<IActionResult> CreateConversation(string recipientUsername)
        {
            //TODO: Lägg till så att jag kan använda createdatroute, så status code blir 201 ist för 200
            var conversationId = await _service.ConversationService.CreateConversation(recipientUsername);
            return Ok(conversationId);
        }
        [HttpGet("userConversations"), Authorize]
        public async Task<ActionResult<List<ConversationDto>>> GetAllUserConversations()
        {
            var conversations = await _service.ConversationService.GetAllUserConversations(trackChanges: true);

            return conversations != null ? Ok(conversations) : NotFound("No conversations found!");
        }

        [HttpGet("{conversationId}"), Authorize/*(Policy = "ConversationMessageRequirements")*/]
        public async Task<IActionResult> GetConversation(int conversationId)
        {
            var conversation = await _service.ConversationService
                .GetConversation(conversationId, trackChanges: false);

            return conversation != null ? Ok(conversation) : NotFound("No conversation found.");
        }
    }
}
