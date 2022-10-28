namespace ChatApp.Contracts.Repositories
{
    public interface IRepositoryManager
    {
        public IConversationRepository ConversationRepository { get; }
        public IMessageRepository ChatMessageRepository { get; }
        public IGroupRepository GroupRepository { get; }
        public IGroupMessageRepository GroupMessageRepository { get; }

        public Task SaveAsync();
    }
}
