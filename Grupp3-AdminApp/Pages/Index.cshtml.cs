using Grupp3_Elevator.Data;
using Grupp3_Elevator.Models;
using Grupp3_Elevator.Services;
using Grupp3_Elevator.Services.Errand;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static MudBlazor.CategoryTypes;

namespace Grupp3_Elevator.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IElevatorService _elevatorService;
        private readonly IErrandService _errandService;

        public IndexModel(ApplicationDbContext context, ILogger<IndexModel> logger, IElevatorService elevatorService)
        {
            _context = context;
            _logger = logger;
            _elevatorService = elevatorService;
            
        }
        public string ErrandsAmount { get; set; }
        public string ElevatorsAmount { get; set; }
        public string TechniciansAmount { get; set; }
        public string CommentsAmount { get; set; }

        public string ElevatorsList { get; set; }
        public List<ErrandModel> GlobeWorksErrands { get; set; }

        public class ElevatorChartModel
        {
            public string Name { get; set; }
            public int ErrandCount { get; set; }
        }


        //En lista med alla hissars errands
        //textarea som är hidden som renderar ut alla värden, sätt ett id på textarea
        //I javascript,  GetElementbyId

        public async Task OnGet()
        {
            ErrandsAmount = _context.Errands.Select(a => a.Id).Count().ToString();
            ElevatorsAmount = _context.Elevators.Select(a => a.Id).Count().ToString();

            TechniciansAmount = _context.Technicians.Select(a => a.Id).Count().ToString();
            CommentsAmount = _context.ErrandComments.Select(a => a).Count().ToString();

            var elevatorNameList = await _elevatorService.GetElevatorsAsync();
            var elevators = new List<ElevatorChartModel>();
            foreach (var elevator in _context.Elevators.Include(e => e.Errands))
            {
                elevators.Add(new ElevatorChartModel
                {
                    Name = elevatorNameList.FirstOrDefault(e => e.Id == elevator.Id)!.Name,
                    ErrandCount = elevator.Errands.Count(),
                });
            }

            ElevatorsList = JsonConvert.SerializeObject(elevators);

            //var elevatorList = await _elevatorService.GetElevatorsAsync();

            //List<ElevatorChartModel> fetchedElevators = _context.Elevators.Include(e => e.Errands).Select(e =>

            //new ElevatorChartModel
            //{
            //    Name = elevatorList.FirstOrDefault(i => i.Id == e.Id)!.Name,
            //    ErrandCount = e.Errands.Count(),
            //}).ToList();


            //ElevatorsList = JsonConvert.SerializeObject(fetchedElevators);


        }
    }   
}