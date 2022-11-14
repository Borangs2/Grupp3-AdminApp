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
        private readonly IToastNotification _toastNotification;
        public ErrandDetailsModel(ApplicationDbContext context, IErrandService errandService, IToastNotification toastNotification)
        {
            _context = context;
            _errandService = errandService;
            _toastNotification = toastNotification;
        }
        public Guid ErrandId { get; set; }
        [BindProperty]
        public string Content { get; set; }
        [BindProperty]
        public Guid TechnicianId { get; set; }
        [BindProperty]
        public List<SelectListItem> SelectTechnician { get; set; }
        public ErrandModel Errand { get; set; }

        public ElevatorModel Elevator { get; set; }
        public List<ErrandCommentModel> Comments { get; set; }

        public string CreateComment(string errandId, string content, string technicianId)
        {
            var errand = _errandService.GetErrandById(errandId);

            var comment = new ErrandCommentModel
            {
                Id = Guid.NewGuid(),
                Content = content,
                Author = Guid.Parse(technicianId),
                PostedAt = DateTime.Now
            };
            errand.Comments.Add(comment);
            _context.Entry(comment).State = EntityState.Added;
            _context.SaveChanges();

            var id = comment.Id.ToString();
            return id;
        }

        public void OnGet(string errandId)
        {
            Errand = _errandService.GetErrandById(errandId);

            SelectTechnician = _errandService.SelectTechnician();
        }

        public IActionResult OnPost(string errandId)
        {
            Errand = _errandService.GetErrandById(errandId);

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
