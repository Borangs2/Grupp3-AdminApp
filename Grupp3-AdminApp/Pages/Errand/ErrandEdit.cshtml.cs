using Grupp3_AdminApp.Services.ErrandComment;
using Grupp3_Elevator.Data;
using Grupp3_Elevator.Models;
using Grupp3_Elevator.Services;
using Grupp3_Elevator.Services.Errand;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        private readonly IErrandCommentService _errandCommentService;

        public ErrandEditModel(ApplicationDbContext context, IElevatorService elevatorService, IErrandService errandService, IErrandCommentService errandCommentService)
        {
            _context = context;
            _elevatorService = elevatorService;
            _errandService = errandService;
            _errandCommentService = errandCommentService;
        }

        
        public ErrandModel Errand { get; set; }
        public List<SelectListItem> SelectTechnicianEdit { get; set; }
        public Guid TechnicianId { get; set; }
        public ElevatorDeviceItem Elevator { get; set; }
        public List<ErrandCommentModel> Comments { get; set; }


        public async Task<IActionResult> OnGetAsync(string elevatorId, string? errandId)
        {
            Elevator = await _elevatorService.GetElevatorDeviceByIdAsync(elevatorId);
            Errand = _errandService.GetErrandByIdAsync(errandId);

            SelectTechnicianEdit = _errandService.SelectTechnicianEdit(Errand.Technician.Id.ToString());

            if (Errand == null)
            {
                return NotFound();
            }

            return Page();
        }

        // TO DO: ModelState, RedirectToPage("ElevatorDetails")
        public async Task<IActionResult> OnPost(string errandId)
        {
            Comments = _errandCommentService.GetErrandCommentsFromErrandId(errandId).ToList();

            try
            {
                var editedErrandId = await _errandService.EditErrandAsync(errandId, Errand, TechnicianId.ToString(), Comments);

                return RedirectToPage("/Elevator/Index");
                
                //return RedirectToPage("ErrandDetails", new { elevatorId = Elevator.Id, editedErrandId });
            }
            catch
            {

                return Page();
            }
            
            //if(ModelState.IsValid)
            //{
            //    var id = await _errandService.EditErrandAsync(errandId, Errand, TechnicianId.ToString(), Comments);

            //    return RedirectToPage("ErrandDetails", new { errandId = id });
            //}

            //return Page();

        }
    }
}
