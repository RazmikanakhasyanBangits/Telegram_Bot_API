using DataAccess.Entity;
using DataAccess.Repositories.Interfaces;

namespace DataAccess.Repositories.Implementation;

public class ChatDetailRepository : GenericRepository<ChatDetail>, IChatDetailRepository
{
    public ChatDetailRepository(TelegramBotDbContext context) : base(context) { }
}