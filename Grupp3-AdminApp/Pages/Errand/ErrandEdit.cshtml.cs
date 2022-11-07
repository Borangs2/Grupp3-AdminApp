using Grupp3_Elevator.Data;
using Grupp3_Elevator.Models;
using Grupp3_Elevator.Services;
using Grupp3_Elevator.Services.Errand;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Grupp3_Elevator.Pages.Errand
{
    [BindProperties]
    public class ErrandEditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IElevatorService _elevatorService;
        private readonly IErrandService _errandService;

        public ErrandEditModel(ApplicationDbContext context, IElevatorService elevatorService, IErrandService errandService)
        {
            _context = context;
            _elevatorService = elevatorService;
            _errandService = errandService;
        }

        
        public ErrandModel Errand { get; set; }
        public List<SelectListItem> SelectTechnicianEdit { get; set; }
        public Guid TechnicianId { get; set; }
        public ElevatorDeviceItem Elevator { get; set; }


        public async Task<IActionResult> OnGetAsync(string elevatorId, string? errandId)
        {
            Elevator = await _elevatorService.GetElevatorDeviceByIdAsync(elevatorId);
            Errand = await _errandService.GetErrandByIdAsync(errandId);

            SelectTechnicianEdit = _errandService.SelectTechnicianEdit(Errand.Technician.Id.ToString());

            if (Errand == null)
            {
                return NotFound();
            }

            return Page();
        }


        public async Task<IActionResult> OnPost(string errandId)
        {
            try
            {
                await _errandService.EditErrandAsync(errandId, Errand, TechnicianId.ToString());

                return RedirectToPage("/Errand/Index");
            }
            catch
            {
                return Page();
            }

        }
    }
}
