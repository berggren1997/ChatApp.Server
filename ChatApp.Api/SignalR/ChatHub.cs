using ChatApp.Service.Contracts;
using ChatApp.Shared.DataTransferObjects.ChatMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace ChatApp.Api.SignalR
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IServiceManager _serviceManager;

        public ChatHub(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("OnConnected", "User connected");
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessageAsync(string message, int conversationId)
        {
            var user = Context.User.FindFirst(ClaimTypes.Name).Value;
            
            if (user == null) return;

            var newMessageDto = new CreateChatMessageDto
            {
                Message = message,
                CreatedAt = DateTime.Now
            };

            var messageToReturn = await _serviceManager.MessageService
                .CreateChatMessage(conversationId, newMessageDto,
                trackChanges: true);
            await Clients.All.SendAsync("ReceiveMessage", messageToReturn);
            
        }

    }
}
