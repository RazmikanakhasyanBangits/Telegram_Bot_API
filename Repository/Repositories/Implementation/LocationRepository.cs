using Repository.Repositories.Interfaces;
using Repository.Entity;

namespace Repository.Repositories.Implementation;

public class LocationRepository : GenericRepository<BankLocation>, ILocationRepository
{
    public LocationRepository(TelegramBotDbContext context) : base(context)
    {
    }
}
