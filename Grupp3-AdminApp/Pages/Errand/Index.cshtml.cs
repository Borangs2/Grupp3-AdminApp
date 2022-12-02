using Grupp3_Elevator.Models;
using Grupp3_Elevator.Services.Errand;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Grupp3_Elevator.Pages.Errand;

public class IndexModel : PageModel
{
    private readonly IErrandService _errandService;

    public IndexModel(IErrandService errandService)
    {
        _errandService = errandService;
    }

    public List<ErrandModel> Errands { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        Errands = await _errandService.GetErrandsAsync();
        return Page();
    }
}