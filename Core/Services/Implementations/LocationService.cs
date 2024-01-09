using AutoMapper;
using Core.Services.Interfaces;
using DataAccess.Entity;
using DataAccess.Repositories.Interfaces;
using Shared.Models.Location;
using Shared.Models.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services.Implementations;

public class LocationService : ILocationService
{
    private readonly ILocationRepository locationRepository;
    private readonly IBankRepository bankRepository;
    private readonly IMapper mapper;

    public LocationService(ILocationRepository locationRepository, IMapper mapper,
        IBankRepository bankRepository)
    {
        this.locationRepository = locationRepository;
        this.mapper = mapper;
        this.bankRepository = bankRepository;
    }


    public async Task<List<BankLocationResponseModel>> GetBankLocationsByName(int bankId)
    {

        IEnumerable<BankLocation> result = await locationRepository.GetAllAsync(x => x.BankId == bankId);

        return mapper.Map<List<BankLocationResponseModel>>(result.ToList());
    }

    public async Task AddBankLocation(GetLocationResponseModel request, string bankName)
    {
        foreach (Result item in request.Results)
        {
            BankLocation location = new()
            {
                LastUpdatedDate = DateTime.UtcNow,
                CreationDate = DateTime.UtcNow,
                BankId = BanksDictionary.Banks[bankName],
                Latitude = item.Geometry.Location.Lat,
                Longitude = item.Geometry.Location.Lng,
                LocationName = item.Formatted_address
            };
            await locationRepository.AddAsync(location);
        }
    }
}
