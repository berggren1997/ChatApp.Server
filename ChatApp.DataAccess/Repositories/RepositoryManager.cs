using ChatApp.Contracts.Repositories;

namespace ChatApp.DataAccess.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<IConversationRepository> _conversationRepository;
        private readonly Lazy<IMessageRepository> _chatMessageRepository;
        private readonly Lazy<IGroupRepository> _groupRepository;
        private readonly Lazy<IGroupMessageRepository> _groupMessageRepository;
        private readonly AppDbContext _context;
        
        public RepositoryManager(AppDbContext context)
        {
            _context = context;
            
            _conversationRepository = new Lazy<IConversationRepository>(() =>
            new ConversationRepository(context));

            _chatMessageRepository = new Lazy<IMessageRepository>(() => 
            new MessageRepository(context));

            _groupRepository = new Lazy<IGroupRepository>(() => 
                new GroupRepository(context));
            
            _groupMessageRepository = new Lazy<IGroupMessageRepository>(() =>
                new GroupMessageRepository(context));
        }

        public IConversationRepository ConversationRepository => _conversationRepository.Value;
        public IMessageRepository ChatMessageRepository => _chatMessageRepository.Value;
        public IGroupRepository GroupRepository => _groupRepository.Value;
        public IGroupMessageRepository GroupMessageRepository => _groupMessageRepository.Value;

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
