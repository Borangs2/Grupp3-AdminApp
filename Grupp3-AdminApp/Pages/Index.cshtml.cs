using Grupp3_Elevator.Data;
using Grupp3_Elevator.Models;
using Grupp3_Elevator.Services;
using Grupp3_Elevator.Services.Errand;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static MudBlazor.CategoryTypes;

namespace Grupp3_Elevator.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IElevatorService _elevatorService;
        private readonly IErrandService _errandService;

        public IndexModel(ApplicationDbContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }
        public string ErrandsAmount { get; set; }
        public string ElevatorsAmount { get; set; }
        public string TechniciansAmount { get; set; }
        public string CommentsAmount { get; set; }

        public ElevatorDeviceItem GlobeWorks { get; set; }
        public List<ErrandModel> GlobeWorksErrands { get; set; }



        //En lista med alla hissars errands
        //textarea som är hidden som renderar ut alla värden, sätt ett id på textarea
        //I javascript,  GetElementbyId

        public async Task OnGet()
        {
            ErrandsAmount = _context.Errands.Select(a => a.Id).Count().ToString();
            ElevatorsAmount = _context.Elevators.Select(a => a.Id).Count().ToString();

            TechniciansAmount = _context.Technicians.Select(a => a.Id).Count().ToString();
            CommentsAmount = _context.ErrandComments.Select(a => a).Count().ToString();

            GlobeWorks = await _elevatorService.GetElevatorDeviceByIdAsync("64248f21-f9cf-4e99-a2f8-5b3c30ff307f");
            GlobeWorksErrands = await _errandService.GetErrandsFromElevatorIdAsync("64248f21-f9cf-4e99-a2f8-5b3c30ff307f");

        }
    }   
}