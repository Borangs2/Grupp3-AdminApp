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
        private readonly IToastNotification _toastNotification;

        private string elevatorId;
        private TechnicianModel technician;

        public ErrandNewModel(ApplicationDbContext context, IErrandService errandService, IElevatorService elevatorService, ITechnicianService technicianService, IToastNotification toastNotification)
        {
            _context = context;
            _errandService = errandService;
            _elevatorService = elevatorService;
            _technicianService = technicianService;
            _toastNotification = toastNotification;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public Guid TechnicianId { get; set; }
        public List<SelectListItem> SelectTechnician { get; set; }

        
        public void OnGet()
        {
            SelectTechnician = _errandService.SelectTechnician();
        }

        public IActionResult OnPost(string elevatorId)
        {

            if (ModelState.IsValid)
            {
                var id = CreateErrandAsync(elevatorId, Title, Description, CreatedBy);
                return RedirectToPage("ErrandDetails", new { errandId = id });
            }
            SelectTechnician = _errandService.SelectTechnician();
            return Page();
        }

        public string CreateErrandAsync(string elevatorId, string Title, string Description, string CreatedBy)
        {
            var elevator = _elevatorService.GetElevatorById(elevatorId);

            var errand = new ErrandModel
            {
                Id = Guid.NewGuid(),
                Title = Title,
                Description = Description,
                Status = ErrandStatus.NotStarted,
                CreatedAt = DateTime.Now,
                LastEdited = DateTime.Now,
                CreatedBy = CreatedBy,
                Technician = _technicianService.GetTechnicianById(TechnicianId),
                Comments = new List<ErrandCommentModel>()
            };
            elevator?.Errands.Add(errand);
            _context.SaveChanges();
            _toastNotification.AddSuccessToastMessage("New errand created!");

            var id = errand.Id.ToString();
            return id;
        }
    }
}
