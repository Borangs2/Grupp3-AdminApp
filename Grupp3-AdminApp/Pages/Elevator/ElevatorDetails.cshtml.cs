using Grupp3_Elevator.Data;
using Grupp3_Elevator.Models;
using Grupp3_Elevator.Services;
using Grupp3_Elevator.Services.Errand;
using Grupp3_Elevator.Services.Technician;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Azure.Devices;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Grupp3_Elevator.Pages.Elevator
{
    public class ElevatorDetailsModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly IElevatorService _elevatorService;
        private readonly IErrandService _errandService;

        public ElevatorDetailsModel(IConfiguration configuration, IElevatorService elevatorService, IErrandService errandService)
        {
            _configuration = configuration;
            _elevatorService = elevatorService;
            _errandService = errandService;
        }

        public ElevatorDeviceItem Elevator { get; set; }
        public List<ErrandModel> Errands { get; set; }

        public async Task OnGetAsync(string elevatorId)
        {
            Elevator = await _elevatorService.GetElevatorDeviceByIdAsync(elevatorId);
            Errands = await _errandService.GetErrandsFromElevatorIdAsync(elevatorId);
        }

        public async Task<IActionResult> OnPostChangeLevel(string elevatorId, int targetLevel)
        {
            try
            {
                using ServiceClient serviceClient = ServiceClient.CreateFromConnectionString(_configuration.GetValue<string>("IoTHubConnection"));
                var directMethod = new CloudToDeviceMethod("ChangeLevel");
                directMethod.SetPayloadJson(JsonConvert.SerializeObject(targetLevel));
                var result = await serviceClient.InvokeDeviceMethodAsync(elevatorId, directMethod);
            }
            catch { }
            return RedirectToPage("ElevatorDetails", new { elevatorId = elevatorId });
        }

        public async Task<IActionResult> OnPostOpenDoors(string elevatorId)
        {
            try
            {
                using ServiceClient serviceClient = ServiceClient.CreateFromConnectionString(_configuration.GetValue<string>("IoTHubConnection"));
                var directMethod = new CloudToDeviceMethod("OpenDoors");
                var result = await serviceClient.InvokeDeviceMethodAsync(elevatorId, directMethod);
            }
            catch { }
            return RedirectToPage("ElevatorDetails", new { elevatorId = elevatorId });
        }

        public async Task<IActionResult> OnPostCloseDoors(string elevatorId)
        {
            try
            {
                using ServiceClient serviceClient = ServiceClient.CreateFromConnectionString(_configuration.GetValue<string>("IoTHubConnection"));
                var directMethod = new CloudToDeviceMethod("CloseDoors");
                var result = await serviceClient.InvokeDeviceMethodAsync(elevatorId, directMethod);
            }
            catch { }
            return RedirectToPage("ElevatorDetails", new { elevatorId = elevatorId });
        }

        public async Task<IActionResult> OnPostResetElevator(string elevatorId)
        {
            try
            {
                using ServiceClient serviceClient = ServiceClient.CreateFromConnectionString(_configuration.GetValue<string>("IoTHubConnection"));
                var directMethod = new CloudToDeviceMethod("ResetElevator");
                var result = await serviceClient.InvokeDeviceMethodAsync(elevatorId, directMethod);
            }
            catch { }
            return RedirectToPage("ElevatorDetails", new { elevatorId = elevatorId });
        }

        public async Task<IActionResult> OnPostTurnOnElevator(string elevatorId)
        {
            try
            {
                using ServiceClient serviceClient = ServiceClient.CreateFromConnectionString(_configuration.GetValue<string>("IoTHubConnection"));
                var directMethod = new CloudToDeviceMethod("TurnOnElevator");
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
                var directMethod = new CloudToDeviceMethod("TurnOffElevator");
                var result = await serviceClient.InvokeDeviceMethodAsync(elevatorId, directMethod);
            }
            catch { }
            return RedirectToPage("ElevatorDetails", new { elevatorId = elevatorId });
        }
    }
}