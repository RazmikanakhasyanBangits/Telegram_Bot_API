using DataAccess.Entity;
using DataAccess.Repositories.Interfaces;

namespace DataAccess.Repositories.Implementation;

public class UserActivityHistoryRepository : GenericRepository<UserActivityHistory>, IUserActivityHistoryRepository
{
    public UserActivityHistoryRepository(TelegramBotDbContext context) : base(context) { }
}
