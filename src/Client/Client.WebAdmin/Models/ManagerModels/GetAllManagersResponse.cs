namespace AdminWebApp.Models.ManagerModels;

public class GetAllManagersResponse
{
    public string Id { get; set; }
    public string? SiteId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Picture { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
}
