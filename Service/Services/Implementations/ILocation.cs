using System.Threading.Tasks;

namespace Service.Services.Implementations;

public interface ILocation
{
    Task<string> GetLocationsAsync(string selection = null);
}
