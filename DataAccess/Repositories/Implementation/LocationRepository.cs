using DataAccess.Models;
using DataAccess.Repositories.Interfaces;

namespace DataAccess.Repositories.Implementation;

public class LocationRepository : GenericRepository<BankLocation>, ILocationRepository
{
    public LocationRepository(TelegramBotDbContext context) : base(context)
    {
    }
}
