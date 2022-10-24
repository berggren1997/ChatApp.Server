using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Api.SignalR
{
    public class ChatHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await SendMessageAsync($"Connection Id: {Context.ConnectionId} has connected");
        }

        public async Task SendMessageAsync(string message)
        {
            if (message.ToLower() == "ping")
            {
                await Clients.All.SendAsync("ReceiveMessage", "PONG!");
            }
        }

    }
}
