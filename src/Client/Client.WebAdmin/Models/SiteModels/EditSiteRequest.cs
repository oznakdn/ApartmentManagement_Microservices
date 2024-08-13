using System.ComponentModel.DataAnnotations;

namespace AdminWebApp.Models.SiteModels;

public record EditSiteRequest(string Id, string ManagerId, [Required] string Name, [Required] string Address);

