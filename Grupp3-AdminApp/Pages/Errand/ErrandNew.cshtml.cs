using Grupp3_Elevator.Models;
using Grupp3_Elevator.Services;
using Grupp3_Elevator.Services.Errand;
using Grupp3_Elevator.Services.Technician;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Grupp3_Elevator.Pages.Errand;

[BindProperties]
public class ErrandNewModel : PageModel
{
    private readonly IElevatorService _elevatorService;
    private readonly IErrandService _errandService;
    private readonly ITechnicianService _technicianService;

    public ErrandNewModel(IErrandService errandService, IElevatorService elevatorService,
        ITechnicianService technicianService)
    {
        _errandService = errandService;
        _elevatorService = elevatorService;
        _technicianService = technicianService;
    }

    [BindProperty] public ElevatorDeviceItem Elevator { get; set; }

    public List<SelectListItem> SelectTechnician { get; set; }
    public Guid TechnicianId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string CreatedBy { get; set; }

    public async Task<IActionResult> OnGetAsync(string elevatorId)
    {
        Elevator = await _elevatorService.GetElevatorDeviceByIdAsync(elevatorId);

        SelectTechnician = await _technicianService.SelectTechniciansAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string elevatorId)
    {
        if (ModelState.IsValid)
        {
            var id = await _errandService.CreateErrandAsync(elevatorId, Title, Description, CreatedBy,
                TechnicianId.ToString());
            return RedirectToPage("ErrandDetails", new {elevatorId, errandId = id});
        }

        SelectTechnician = await _technicianService.SelectTechniciansAsync();
        return Page();
    }
}