using ChatApp.Contracts.Repositories;

namespace ChatApp.DataAccess.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<IConversationRepository> _conversationRepository;
        private readonly Lazy<IChatMessageRepository> _chatMessageRepository;
        private readonly AppDbContext _context;
        
        public RepositoryManager(AppDbContext context)
        {
            _context = context;
            
            _conversationRepository = new Lazy<IConversationRepository>(() =>
            new ConversationRepository(context));

            _chatMessageRepository = new Lazy<IChatMessageRepository>(() => 
            new ChatMessageRepository(context));
        }

        public IConversationRepository ConversationRepository => _conversationRepository.Value;
        public IChatMessageRepository ChatMessageRepository => _chatMessageRepository.Value;

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
