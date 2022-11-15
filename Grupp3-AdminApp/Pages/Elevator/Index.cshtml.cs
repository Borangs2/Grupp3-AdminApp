using Grupp3_Elevator.Data;
using Grupp3_Elevator.Models;
using Grupp3_Elevator.Pages.Errand;
using Grupp3_Elevator.Services;
using Grupp3_Elevator.Services.Errand;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using static Grupp3_Elevator.Models.ElevatorDeviceItem;

namespace Grupp3_Elevator.Pages.Elevator
{
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
}
