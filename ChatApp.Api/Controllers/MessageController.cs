using ChatApp.Service.Contracts;
using ChatApp.Shared.DataTransferObjects.ChatMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IServiceManager _service;

        public MessageController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet("{conversationId}"), Authorize]
        public async Task<IActionResult> GetMessagesInConversation(int conversationId)
        {
            var messages = await _service.MessageService
                .GetChatMessagesInConversation(conversationId, trackChanges: false);

            return messages != null ? Ok(messages) : NotFound("No messages found");
        }

        [HttpPost("{conversationId}"), Authorize(Policy = "ConversationMessageRequirements")]
        public async Task<IActionResult> CreateChatMessage(int conversationId, CreateChatMessageDto chatMessage)
        {
            try
            {
                await _service.MessageService.CreateChatMessage(conversationId, chatMessage, 
                    trackChanges: true);
                return StatusCode(201);
            }
            catch (Exception e)
            {
                return BadRequest($"Something went wrong when sending message: {e.Message}");
            }
        }
    }
}
