using Grupp3_Elevator.Data;
using Grupp3_Elevator.Models;
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
        private readonly IErrandService _errandService;

        public ErrandEditModel(ApplicationDbContext context, IErrandService errandService)
        {
            _context = context;
            _errandService = errandService;
        }

        
        public ErrandModel Errand { get; set; }
        public List<SelectListItem> SelectTechnicianEdit { get; set; }
        public Guid TechnicianId { get; set; }


        public async Task<IActionResult> OnGetAsync(string? errandId)
        {
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
            //var comments = 

            if(ModelState.IsValid)
            {
                var id = await _errandService.EditErrandAsync(errandId, Errand, TechnicianId.ToString());

                return RedirectToPage("ErrandDetails", new { errandId = id });
            }

            return Page();

        }
    }
}
