using Grupp3_Elevator.Data;
using Grupp3_Elevator.Models;
using Grupp3_Elevator.Services;
using Grupp3_Elevator.Services.Errand;
using Grupp3_Elevator.Services.Technician;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Azure.Devices;
using Microsoft.EntityFrameworkCore;

namespace Grupp3_Elevator.Pages.Elevator
{
    public class ElevatorDetailsModel : PageModel
    {
        private readonly IElevatorService _elevatorService;
        private readonly IErrandService _errandService;
        private readonly IConfiguration _configuration;

        public ElevatorDetailsModel(IElevatorService elevatorService, IErrandService errandService, IConfiguration configuration)
        {
            _elevatorService = elevatorService;
            _errandService = errandService;
            _configuration = configuration;
        }

        public ElevatorDeviceItem Elevator { get; set; }
        public List<ErrandModel> Errands { get; set; }

        public async Task OnGetAsync(string elevatorId)
        {
            Elevator = await _elevatorService.GetElevatorDeviceByIdAsync(elevatorId);

            Errands = _errandService.GetErrandsFromElevatorId(elevatorId);
        }

        public async Task<IActionResult> OnPostTurnOnElevator(string elevatorId)
        {
            try
            {
                using ServiceClient serviceClient = ServiceClient.CreateFromConnectionString(_configuration.GetValue<string>("IoTHubConnection"));
                var directMethod = new CloudToDeviceMethod("TurnOnElevatorDM");
                var result = await serviceClient.InvokeDeviceMethodAsync(elevatorId, directMethod);
            }
            catch { }
            return RedirectToPage("ElevatorDetails", new { elevatorId = elevatorId });
        }

        public async Task<IActionResult> OnPostTurnOffElevator(string elevatorId)
        {
            try
            {
                using ServiceClient serviceClient = ServiceClient.CreateFromConnectionString(_configuration.GetValue<string>("IoTHubConnection"));
                var directMethod = new CloudToDeviceMethod("TurnOffElevatorDM");
                var result = await serviceClient.InvokeDeviceMethodAsync(elevatorId, directMethod);
            }
            catch { }
            return RedirectToPage("ElevatorDetails", new { elevatorId = elevatorId });
        }
    }
}