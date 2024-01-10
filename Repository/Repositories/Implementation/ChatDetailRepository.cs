using Repository.Repositories.Interfaces;
using Repository.Entity;

namespace Repository.Repositories.Implementation;

public class ChatDetailRepository : GenericRepository<ChatDetail>, IChatDetailRepository
{
    public ChatDetailRepository(TelegramBotDbContext context) : base(context) { }
}