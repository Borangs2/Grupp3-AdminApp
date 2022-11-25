using Grupp3_AdminApp.Services.ErrandComment;
using Grupp3_Elevator.Data;
using Grupp3_Elevator.Models;
using Grupp3_Elevator.Services;
using Grupp3_Elevator.Services.Errand;
using Grupp3_Elevator.Services.Technician;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Grupp3_Elevator.Pages.Errand
{
    public class ErrandEditModel : PageModel
    {
        private readonly IElevatorService _elevatorService;
        private readonly IErrandService _errandService;
        private readonly IErrandCommentService _errandCommentService;
        private readonly ITechnicianService _technicianService;

        public ErrandEditModel(IElevatorService elevatorService, IErrandService errandService, IErrandCommentService errandCommentService, ITechnicianService technicianService)
        {
            _elevatorService = elevatorService;
            _errandService = errandService;
            _errandCommentService = errandCommentService;
            _technicianService = technicianService;
        }

        [BindProperty]
        public ElevatorDeviceItem Elevator { get; set; }
        [BindProperty]
        public ErrandModel Errand { get; set; }
        public List<SelectListItem> SelectTechnicianEdit { get; set; }

        public async Task<IActionResult> OnGetAsync(string elevatorId, string errandId)
        {
            Elevator = await _elevatorService.GetElevatorDeviceByIdAsync(elevatorId);
            Errand = await _errandService.GetErrandByIdAsync(errandId);

            SelectTechnicianEdit = await _technicianService.SelectTechnicianEdit(Errand.Technician.Id.ToString());

            if (Errand == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPost(string elevatorId)
        {
            Errand.Technician = await _technicianService.GetTechnicianById(Errand.Technician.Id.ToString());
            Errand.Comments = await _errandCommentService.GetErrandCommentsFromErrandIdAsync(Errand.Id.ToString());

            if (ModelState.IsValid)
            {
                await _errandService.EditErrandAsync(Errand.Id.ToString(), Errand, Errand.Technician.Id.ToString(), Errand.Comments);

                return RedirectToPage("ErrandDetails", new { elevatorId, errandId = Errand.Id.ToString() });
            }
            SelectTechnicianEdit = await _technicianService.SelectTechnicianEdit(Errand.Technician.Id.ToString());
            return Page();
        }
    }
}