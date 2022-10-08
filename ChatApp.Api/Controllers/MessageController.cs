using ChatApp.Service.Contracts;
using ChatApp.Shared.DataTransferObjects.ChatMessages;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IServiceManager _service;

        public MessageController(IServiceManager service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateChatMessage(int conversationId, CreateChatMessageDto chatMessage)
        {
            try
            {
                await _service.ChatMessageService.CreateChatMessage(conversationId, chatMessage, trackChanges: true);
                return StatusCode(201);
            }
            catch (Exception e)
            {
                return BadRequest($"Something went wrong when sending message: {e.Message}");
            }
        }
    }
}
