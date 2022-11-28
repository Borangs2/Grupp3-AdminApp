using Grupp3_Elevator.Data;
using Grupp3_Elevator.Models;
using Grupp3_Elevator.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Grupp3_Elevator.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IElevatorService _elevatorService;
        public IndexModel(ApplicationDbContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }
        public string ErrandsAmount { get; set; }
        public string ElevatorsAmount { get; set; }
        public string TechnichansAmount { get; set; }
        public string CommentsAmount { get; set; }
        public List<int> ErrandsPerElevatorOne { get; set; }

        public List<ElevatorDeviceItem> Elevators { get; set; }
        public List<int> Errands { get; set; }

        public async Task OnGet()
        {
            ErrandsAmount = _context.Errands.Select(a => a.Id).Count().ToString();
            ElevatorsAmount = _context.Elevators.Select(a => a.Id).Count().ToString();

            TechnichansAmount = _context.Technicians.Select(a => a.Id).Count().ToString();
            CommentsAmount = _context.ErrandComments.Select(a => a).Count().ToString();

            Elevators = await _elevatorService.GetElevatorsAsync();



            Errands = _context.Elevators.Include(e => e.Errands).Select(e => e.Errands.Count()).ToList();

        }
    }
}