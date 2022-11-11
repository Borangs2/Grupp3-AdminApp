using Grupp3_Elevator.Data;
using Grupp3_Elevator.Models;
using Grupp3_Elevator.Services;
using Grupp3_Elevator.Services.Errand;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Grupp3_Elevator.Pages.Errand
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IErrandService _errandService;

        public IndexModel(ApplicationDbContext context, IErrandService errandService)
        {
            _context = context;
            _errandService = errandService;
        }

        public List<ErrandModel> Errands { get; set; }

        public void OnGet()
        {
            Errands = _errandService.GetErrands();
        }
    }
}
