using DataAccess.Entity;
using DataAccess.Repositories.Interfaces;

namespace DataAccess.Repositories.Implementation;

public class LocationRepository : GenericRepository<BankLocation>, ILocationRepository
{
    public LocationRepository(TelegramBotDbContext context) : base(context)
    {
    }
}
