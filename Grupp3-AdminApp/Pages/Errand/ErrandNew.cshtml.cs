using Grupp3_Elevator.Data;
using Grupp3_Elevator.Models;
using Grupp3_Elevator.Services.Errand;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.Framework;
using NToastNotify;
using System.ComponentModel.DataAnnotations;
using Grupp3_Elevator.Services;
using Grupp3_Elevator.Services.Technician;
using Microsoft.AspNetCore.Mvc.Rendering;
using Grupp3_Elevator.Migrations;

namespace Grupp3_Elevator.Pages.Errand
{
    [BindProperties]
    public class ErrandNewModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IErrandService _errandService;
        private readonly IElevatorService _elevatorService;
        private readonly ITechnicianService _technicianService;

        public ErrandNewModel(ApplicationDbContext context, IErrandService errandService, IElevatorService elevatorService, ITechnicianService technicianService)
        {
            _context = context;
            _errandService = errandService;
            _elevatorService = elevatorService;
            _technicianService = technicianService;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public Guid TechnicianId { get; set; }
        public List<SelectListItem> SelectTechnician { get; set; }
        public ElevatorDeviceItem Elevator { get; set; }


        public async Task<IActionResult> OnGetAsync(string elevatorId)
        {
            Elevator = await _elevatorService.GetElevatorDeviceByIdAsync(elevatorId);

            SelectTechnician = _errandService.SelectTechnician();
            return Page();
        }

        public IActionResult OnPost(string elevatorId)
        {
            if (ModelState.IsValid)
            {
                var id = _errandService.CreateErrandAsync(elevatorId, Title, Description, CreatedBy, TechnicianId.ToString());
                return RedirectToPage("ErrandDetails", new { errandId = id });
            }
            SelectTechnician = _errandService.SelectTechnician();
            return Page();
        }
    }
}
