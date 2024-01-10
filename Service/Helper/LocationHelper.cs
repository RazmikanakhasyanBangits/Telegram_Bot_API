using Newtonsoft.Json;
using Shared.Models.Location;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Core.Helper
{
    public static class LocationHelper
    {
        public static async Task<GetLocationResponseModel> GetLocationsByNameAsync(GetLocationRequestModel model)
        {
            string requestUrl = $"{model.BaseUrl}{WebUtility.UrlEncode(model.LocationName)}&key={model.ApiKey}";
            try
            {
                using HttpClient client = new();
                string json = await client.GetStringAsync(requestUrl);

                GetLocationResponseModel data = JsonConvert.DeserializeObject<GetLocationResponseModel>(json);


                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new GetLocationResponseModel();
            }
        }
    }
}
