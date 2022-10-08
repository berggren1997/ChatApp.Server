using ChatApp.Contracts.Repositories;
using ChatApp.Entities.Models;
using ChatApp.Service.Contracts.Authentication;
using ChatApp.Service.Contracts.Conversations;
using ChatApp.Shared.DataTransferObjects.ChatMessages;
using ChatApp.Shared.DataTransferObjects.Conversations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Service.Conversations
{
    public class ConversationService : IConversationService
    {
        private readonly IRepositoryManager _repository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserAccessor _userAccessor;

        public ConversationService(IRepositoryManager repositoy, IUserAccessor userAccessor, 
            UserManager<AppUser> userManager)
        {
            _repository = repositoy;
            _userAccessor = userAccessor;
            _userManager = userManager;
        }

        public async Task CreateConversation(CreateConversationDto conversation)
        {
            var userId = _userAccessor.GetCurrentUserId();
            
            var conversationEntity = new Conversation
            {
                RoomName = conversation.RoomName,
                CreatedAt = DateTime.Now,
                CreatedByAppUserId = userId
            };
            _repository.ConversationRepository.CreateConversation(conversationEntity);
            await _repository.SaveAsync();

            // Create Conversation + Save
        }

        public async Task<ConversationDto> GetConversation(int id, bool trackChanges)
        {
            var conversation = await _repository.ConversationRepository.GetConversation(id, trackChanges);
            
            if (conversation == null) 
                throw new Exception("Something went wrong when getting conversation");

            var messages = await _repository.ChatMessageRepository.GetMessages(id, trackChanges: false);
            
            var conversationDto = new ConversationDto
            {
                Id = conversation.Id,
                RoomName = conversation.RoomName,
                CreatedAt = conversation.CreatedAt,
                CreatedByAppUserName = conversation.CreatedByAppUser?.UserName!,
                ChatMessages = messages.Select(x => new MessageDto
                {
                    Id = x.Id,
                    CreatedAt = x.CreatedAt,
                    FromUsername = x.Sender!.UserName, //TODO: kolla så att messages inte är null innan
                    Message = x.ChatMessage
                }).ToList()
            };
            return conversationDto;

        }
    }
}
