using System.ComponentModel.DataAnnotations;

namespace AdminWebApp.Models.SiteModels;

public record CreateSiteRequest(string ManagerId, [Required] string Name, [Required] string Address);
