using System.ComponentModel.DataAnnotations;

namespace Client.WebAdmin.Models.ApartmentModels;

public record CreateSiteRequest(string ManagerId, [Required] string Name, [Required] string Address);
