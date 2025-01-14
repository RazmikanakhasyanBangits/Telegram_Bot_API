﻿// Ignore Spelling: DataImp

using Service.Services.DataScrapper.Impl;
using Service.Services.Implementations;
using Service.Services.Interfaces;
using Service.Model.Models.Location;
using Service.Model.Models.Static;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.DataScrapper.Implementation
{
    public class Location : ILocation
    {
        private readonly ILocationService locationService;

        public Location(ILocationService locationService)
        {
            this.locationService = locationService;
        }

        public Dictionary<string, ILocation> Dictionary()
        {
            return new Dictionary<string, ILocation>
            {
                {  nameof(AmeriaBankDataScrapper) , new AmeriaBankDataScrapper(locationService) },
                {  nameof(EvocaBankDataScrapper) , new EvocaBankDataScrapper(locationService) },
                {  nameof(AcbaBankDataScrapper), new AcbaBankDataScrapper(locationService) },
                {  nameof(InecoBankDataScrapper), new InecoBankDataScrapper(locationService) },
                {  nameof(UniBankDataScrapper) , new UniBankDataScrapper(locationService) },
            };
        }

        public async Task<string> GetLocationsAsync(string selection)
        {
            List<BankLocationResponseModel> locations = await locationService.GetBankLocationsByName(BanksDictionary.Banks[selection]);

            if (locations is null || !locations.Any())
            {
                string result = await Dictionary()[selection].GetLocationsAsync(selection);
                return result;

            }
            StringBuilder builder = new();
            foreach (BankLocationResponseModel item in locations)
            {
                _ = builder.AppendLine($"{item.LocationName}");
                _ = builder.AppendLine($"Թարմացվել է` {item.LastUpdatedDate:G}");
                _ = builder.AppendLine("--------------------------------------------");
            }
            return builder.ToString();
        }




    }
}
