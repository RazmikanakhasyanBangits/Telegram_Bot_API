using System.Threading.Tasks;

namespace Core.Services.Implementations;

public interface ILocation
{
    Task<string> GetLocationsAsync(string selection = null);
}
