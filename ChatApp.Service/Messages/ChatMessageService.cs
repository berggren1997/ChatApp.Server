using ChatApp.Contracts.Repositories;
using ChatApp.Entities.Models;
using ChatApp.Service.Contracts.Authentication;
using ChatApp.Service.Contracts.Message;
using ChatApp.Shared.DataTransferObjects.ChatMessages;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Service.Messages
{
    public class ChatMessageService : IChatMessageService
    {
        private readonly IRepositoryManager _repository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserAccessor _userAccessor;

        public ChatMessageService(IRepositoryManager repository, UserManager<AppUser> userManager, 
            IUserAccessor userAccessor)
        {
            _repository = repository;
            _userManager = userManager;
            _userAccessor = userAccessor;
        }

        public async Task CreateChatMessage(int conversationId, CreateChatMessageDto chatMessage, 
            bool trackChanges)
        {
            // Get current conversation
            var conversation = await _repository.ConversationRepository
                .GetConversation(conversationId, trackChanges);

            if (conversation == null) 
                throw new Exception($"No conversation with id {conversationId} was found");

            //Get user sending request, to get the id.
            var userId = _userAccessor.GetCurrentUserId();
            
            if(userId == -1) 
            {
                throw new Exception("fml this method is so long");
            }

            var userSender = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);

            // Map new message to entity model
            var chatMessageEntity = new Message
            {
                ChatMessage = chatMessage.Message,
                CreatedAt = DateTime.Now,
                ConversationId = conversation.Id,
                Sender = userSender,
                SenderId = userSender.Id
            };

            conversation.Messages.Add(chatMessageEntity);

            await _repository.SaveAsync();
        }
    }
}
