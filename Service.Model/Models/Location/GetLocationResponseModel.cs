using System;
using System.Collections.Generic;

namespace Service.Model.Models.Location;

public class GetLocationResponseModel
{
    public List<Result> Results { get; set; }
    public string Status { get; set; }
}

public class BankLocationResponseModel
{
    public long Id { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public string LocationName { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime LastUpdatedDate { get; set; }
    public int BankId { get; set; }
}
