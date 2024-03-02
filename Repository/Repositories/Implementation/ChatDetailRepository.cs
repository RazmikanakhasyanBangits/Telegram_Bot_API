using Repository.Repositories.Interfaces;
using Repository.Entity;

namespace Repository.Repositories.Implementation;

public class ChatDetailRepository : GenericRepository<ChatDetail>, IChatDetailRepository
{
    public ChatDetailRepository(ExchangeBotDbContext context) : base(context) { }
}