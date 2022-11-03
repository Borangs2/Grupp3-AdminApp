using Grupp3_Elevator.Data;
using Grupp3_Elevator.Models;
using Grupp3_Elevator.Services.Errand;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Grupp3_Elevator.Pages.Errand
{
    public class ErrandEditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IErrandService _errandService;

        public ErrandEditModel(ApplicationDbContext context, IErrandService errandService)
        {
            _context = context;
            _errandService = errandService;
        }

        [BindProperty]
        public ErrandModel Errand { get; set; }

        public List<SelectListItem> SelectTechnician { get; set; }


        public async Task<IActionResult> OnGetAsync(string? errandId)
        {
            Errand = await _errandService.GetErrandByIdAsync(errandId);

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
                await _errandService.EditErrandAsync(errandId, Errand);
                return RedirectToPage("/Errand/Index");
            }
            catch
            {
                return Page();
            }



            // !!!THIS WORKS!!!

            //try
            //{
            //    var errandToEdit = _context.Errands.FirstOrDefault(e => e.Id.ToString() == errandId);
            //    //var errandToEdit = _errandService.GetErrandByIdAsync(errandId);
            //    errandToEdit.Title = Errand.Title;
            //    errandToEdit.Description = Errand.Description;

            //    _context.SaveChanges();

            //    return RedirectToPage("/Errand/Index");
            //    //return RedirectToPage("/Errand/ErrandDetails", new { Id = errandId });
            //}
            //catch
            //{
            //    return Page();
            //}


        }
    }
}
