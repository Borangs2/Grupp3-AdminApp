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
        private readonly IErrandCommentService _errandCommentService;

        public ErrandDetailsModel(ApplicationDbContext context, IErrandService errandService, IElevatorService elevatorService, IErrandCommentService errandCommentService)
        {
            _context = context;
            _errandService = errandService;
            _elevatorService = elevatorService;
            _errandCommentService = errandCommentService;
        }


        public ElevatorDeviceItem Elevator { get; set; }
 
        public ErrandModel Errand { get; set; }


        public Guid TechnicianId { get; set; }
        public string Content { get; set; }
        public List<SelectListItem> SelectTechnician { get; set; }
        public List<ErrandCommentModel> Comments { get; set; }

        public async Task<IActionResult> OnGetAsync(string elevatorId, string errandId)
        {
            Elevator = await _elevatorService.GetElevatorDeviceByIdAsync(elevatorId);
            Errand = _errandService.GetErrandById(errandId);

            SelectTechnician = _errandService.SelectTechnician();

            if (Errand == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPost(string elevatorId, string errandId, string technicianId, string content)
        {
            Errand = _errandService.GetErrandById(errandId);
            

            if (ModelState.IsValid)
            {
                //CreateComment(Errand, technicianId, content);
                await _errandCommentService.CreateErrandComment(Errand, technicianId, content);

                //return RedirectToPage("ErrandDetails", new { errandId = Errand.Id.ToString() });
                //return RedirectToPage("ErrandDetails", new { elevatorId = Elevator.Id, errandId = Errand.Id });
                return RedirectToPage("ErrandDetails", new { elevatorId, errandId = Errand.Id });
            }
            
            SelectTechnician = _errandService.SelectTechnician();
            
            return Page();
        }
    }
}
