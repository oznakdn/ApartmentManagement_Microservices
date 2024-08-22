using Client.WebAdmin.ClientServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Client.WebAdmin.Pages.Dashboard;

public class IndexModel(AggregateService aggregateService) : PageModel
{
    [BindProperty]
    public int SiteCount { get; set; }

    [BindProperty]
    public int BlockCount { get; set; }

    [BindProperty]
    public int UnitCount { get; set; }

    public async Task OnGetAsync()
    {
        var result = await aggregateService.GetApartmentCounts();
        if (result is null)
            return;

        SiteCount = result.SiteCount;
        BlockCount = result.BlockCount;
        UnitCount = result.UnitCount;
    }
}
