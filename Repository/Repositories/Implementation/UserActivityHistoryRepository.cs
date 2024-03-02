using Repository.Entity;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories.Implementation;

public class UserActivityHistoryRepository : GenericRepository<UserActivityHistory>, IUserActivityHistoryRepository
{
    public UserActivityHistoryRepository(ExchangeBotDbContext context) : base(context) { }
}
