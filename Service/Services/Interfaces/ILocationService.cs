using Service.Model.Models.Location;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Services.Interfaces;

public interface ILocationService
{
    Task AddBankLocation(GetLocationResponseModel request, string bankName);
    Task<List<BankLocationResponseModel>> GetBankLocationsByName(int bankId);
}
