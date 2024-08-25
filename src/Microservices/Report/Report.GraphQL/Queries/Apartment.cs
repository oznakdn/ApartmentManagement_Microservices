namespace Report.GraphQL.Queries;

public class Apartment
{
    public string SiteId { get; set; }
    public int Blocks { get; set; }
    public int TotalUnits { get; set; }
    public int EmptyUnits { get; set; }
    public int FullUnits { get; set; }
}
