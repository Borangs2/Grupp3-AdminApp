using Grupp3_Elevator.Models;
using Grupp3_Elevator.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Grupp3_Elevator.Pages.Elevator;

public class IndexModel : PageModel
{
    private readonly IElevatorService _elevatorService;

    public IndexModel(IElevatorService elevatorService)
    {
        _elevatorService = elevatorService;
    }

    public List<ElevatorDeviceItem> ElevatorsTwin { get; set; }

    public async Task OnGetAsync()
    {
        ElevatorsTwin = await _elevatorService.GetElevatorsAsync();
    }
}