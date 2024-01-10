namespace Service.Model.Models.Location;

public class Result
{
    public string Business_status { get; set; }
    public string Formatted_address { get; set; }
    public string Name { get; set; }
    public GeometryModel Geometry { get; set; }
}
