﻿using ChatApp.Shared.DataTransferObjects.ChatMessages;

namespace ChatApp.Service.Contracts.Message
{
    public interface IChatMessageService
    {
        Task<MessageDto> CreateChatMessage(int conversationId, CreateChatMessageDto chatMessage, bool trackChanges);
        Task<IEnumerable<MessageDto>> GetChatMessagesInConversation(int conversationId, bool trackChanges);
    }
}
