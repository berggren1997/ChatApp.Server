using ChatApp.Contracts.Repositories;
using ChatApp.Entities.Exceptions.NotFoundRequests;
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

        public async Task CreateConversation(string recipientUsername)
        {
            var creator = await _userManager.Users
                .FirstOrDefaultAsync(x => x.Id == _userAccessor.GetCurrentUserId());

            if (creator == null) throw new UserNotFoundException();

            var recipient = await _userManager.Users
                .FirstOrDefaultAsync(x => x.UserName == recipientUsername);

            if (recipient == null) throw new UserNotFoundException();

            var conversationEntity = new Conversation
            {
                CreatedAt = DateTime.Now,
                CreatedByAppUser = creator,
                Recipient = recipient
            };
            
            _repository.ConversationRepository.CreateConversation(conversationEntity);
            await _repository.SaveAsync();
        }

        public async Task<List<ConversationDto>> GetAllUserConversations(bool trackChanges)
        {
            var senderUsername = _userAccessor.GetCurrentUserName();
            
            if (senderUsername == null) 
                throw new Exception();
            
            var user = await _userManager.FindByNameAsync(senderUsername);
            
            if (user == null) 
                throw new UserNotFoundException();

            var conversations = await _repository.ConversationRepository
                .GetAllUserConversations(user.Id, trackChanges);

            if (conversations == null)
                throw new ConversationNotFoundException();

            return conversations.Select(x => new ConversationDto
            {
                Id = x.Id,
                CreatedAt = x.CreatedAt,
                CreatedBy = x.CreatedByAppUser?.UserName!,
                Recipient = x.Recipient?.UserName!,
                ChatMessages = x.Messages.Select(x => new MessageDto
                {
                    Id = x.Id,
                    CreatedAt = x.CreatedAt,
                    FromUsername = x.Sender?.UserName!,
                    Message = x.ChatMessage
                }).ToList()
            }).ToList();
        }

        public async Task<ConversationDto> GetConversation(int id, bool trackChanges)
        {
            var conversation = await _repository.ConversationRepository.GetConversation(id, trackChanges);

            if (conversation == null)
                throw new ConversationNotFoundException(id);

            var messages = await _repository.ChatMessageRepository.GetMessages(id, trackChanges: false);
            
            var conversationDto = new ConversationDto
            {
                Id = conversation.Id,
                CreatedAt = conversation.CreatedAt,
                CreatedBy = conversation.CreatedByAppUser?.UserName!,
                Recipient = conversation.Recipient?.UserName!,
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
