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

        public ElevatorDetailsModel(IElevatorService elevatorService, IErrandService errandService)
        {
            _elevatorService = elevatorService;
            _errandService = errandService;
        }

        public ElevatorDeviceItem Elevator { get; set; }
        public List<ErrandModel> Errands { get; set; }

        public async Task OnGetAsync(string elevatorId)
        {
            Elevator = await _elevatorService.GetElevatorDeviceByIdAsync(elevatorId);

            Errands = _errandService.GetErrandsFromElevatorId(elevatorId);
        }
        public async Task<IActionResult> OnPostTurnOffElevator(string elevatorId)
        {
            try
            {
                using ServiceClient serviceClient = ServiceClient.CreateFromConnectionString("HostName=kyh-shared-iothub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=/5asl5agNK3raYZNyfkumb0vcsnT+OdUeoUOupOWLQo=");

                var directMethod = new CloudToDeviceMethod("TurnOffElevatorDM");
                var result = await serviceClient.InvokeDeviceMethodAsync(elevatorId, directMethod);
            }
            catch
            {

            }
            return RedirectToPage("ElevatorDetails", new { elevatorId = elevatorId });
        }
    }
}