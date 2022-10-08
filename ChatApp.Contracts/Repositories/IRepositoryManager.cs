namespace ChatApp.Contracts.Repositories
{
    public interface IRepositoryManager
    {
        public IConversationRepository ConversationRepository { get; }
        public IChatMessageRepository ChatMessageRepository { get; }

        Task SaveAsync();
    }
}
