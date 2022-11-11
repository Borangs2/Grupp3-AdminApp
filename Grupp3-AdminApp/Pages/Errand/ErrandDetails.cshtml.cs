using Grupp3_Elevator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using Grupp3_Elevator.Data;
using Grupp3_Elevator.Services.Errand;
using Grupp3_Elevator.Services;
using Grupp3_Elevator.Services.Technician;
using Grupp3_AdminApp.Services.ErrandComment;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;

namespace Grupp3_Elevator.Pages.Errand
{
    public class ErrandDetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IErrandService _errandService;
        private readonly IElevatorService _elevatorService;
        private readonly IToastNotification _toastNotification;
        
        public ErrandDetailsModel(ApplicationDbContext context, IErrandService errandService, IElevatorService elevatorService, IToastNotification toastNotification)
        {
            _context = context;
            _errandService = errandService;
            _elevatorService = elevatorService;
            _toastNotification = toastNotification;
        }

        [BindProperty]
        public ElevatorDeviceItem Elevator { get; set; }
        public ErrandModel Errand { get; set; }
        public ElevatorModel Elevator { get; set; }
        public List<ErrandCommentModel> Comments { get; set; }

        public string CreateComment(string errandId, string content, string technicianId)
        {
            var errand = _errandService.GetErrandById(errandId);


        public List<SelectListItem> SelectTechnician { get; set; }
        [BindProperty]
        public Guid ChosenSelectTechnician { get; set; }

        [BindProperty]
        public string Content { get; set; }

        public async Task<IActionResult> OnGetAsync(string elevatorId, string errandId)
        {
            Elevator = await _elevatorService.GetElevatorDeviceByIdAsync(elevatorId);
            Errand = await _errandService.GetErrandByIdAsync(errandId);

            SelectTechnician = _errandService.SelectTechnician();

            if (Errand == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string elevatorId, string errandId)
        {
            Errand = await _errandService.GetErrandByIdAsync(errandId);

            if (ModelState.IsValid)
            {
                var id = CreateComment( errandId, Content, TechnicianId.ToString());
                _toastNotification.AddSuccessToastMessage("Comment successfully saved!");

                return RedirectToPage("ErrandDetails", new { errandId = Errand.Id.ToString() });
            }

            _toastNotification.AddErrorToastMessage("Failed to save comment!");
            SelectTechnician = _errandService.SelectTechnician();
            
            return Page();
        }
    }
}
