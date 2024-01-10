using Repository.Entity;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories.Implementation;

public class UserActivityHistoryRepository : GenericRepository<UserActivityHistory>, IUserActivityHistoryRepository
{
    public UserActivityHistoryRepository(TelegramBotDbContext context) : base(context) { }
}
