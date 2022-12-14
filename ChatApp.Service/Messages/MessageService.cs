using ChatApp.Contracts.Repositories;
using ChatApp.Entities.Exceptions.NotFoundRequests;
using ChatApp.Entities.Models;
using ChatApp.Service.Contracts.Authentication;
using ChatApp.Service.Contracts.Message;
using ChatApp.Shared.DataTransferObjects.ChatMessages;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Service.Messages
{
    public class MessageService : IMessageService
    {
        private readonly IRepositoryManager _repository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserAccessor _userAccessor;

        public MessageService(IRepositoryManager repository, UserManager<AppUser> userManager, 
            IUserAccessor userAccessor)
        {
            _repository = repository;
            _userManager = userManager;
            _userAccessor = userAccessor;
        }

        public async Task<MessageDto> CreateChatMessage(int conversationId, CreateChatMessageDto chatMessage, 
            bool trackChanges)
        {
            var conversation = await _repository.ConversationRepository
                .GetConversation(conversationId, trackChanges);

            if (conversation == null) 
                throw new ConversationNotFoundException();

            var username = _userAccessor.GetCurrentUserName();
            
            if(username == null)
                throw new UserNotFoundException("User not authorized");

            //TODO: TESTA IF-CHECKEN, EJ TESTAD
            if (username != conversation.CreatedByAppUser?.UserName && 
                username != conversation.Recipient?.UserName)
            {
                throw new UserNotFoundException("User not part of conversation");
            }
            //TODO: maybe this call is unnecessary. I think I already have the user via conversation object
            var userSender = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == username);

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

            var messageToReturn = new MessageDto
            {
                Id = chatMessageEntity.Id,
                CreatedAt = chatMessageEntity.CreatedAt,
                Message = chatMessage.Message,
                FromUsername = chatMessageEntity.Sender.UserName
            };
            return messageToReturn;
        }

        public async Task<IEnumerable<MessageDto>> GetChatMessagesInConversation(int conversationId, 
            bool trackChanges)
        {
            var messages = await _repository.ChatMessageRepository.GetMessages(conversationId, trackChanges);
            //TODO: Not sure if i want to throw an exception here? Maybe returning an empty list is fine?
            if (messages == null) 
                throw new MessagesNotFoundException("No messages found in conversation.");

            var messagesToReturn = messages.Select(x => new MessageDto
            {
                Id = x.Id,
                Message = x.ChatMessage,
                CreatedAt = x.CreatedAt,
                FromUsername = x.Sender!.UserName
            }).ToList();

            return messagesToReturn;
        }
    }
}
