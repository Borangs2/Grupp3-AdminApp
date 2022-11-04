using Grupp3_Elevator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Grupp3_Elevator.Data;
using Grupp3_Elevator.Services.Errand;
using Grupp3_Elevator.Services;
using Grupp3_Elevator.Services.Technician;

namespace Grupp3_Elevator.Pages.Errand
{
    [BindProperties]
    public class ErrandDetailsModel : PageModel
    {      
        private readonly ApplicationDbContext _context;
        private readonly IErrandService _errandService;
        private readonly ITechnicianService _technicianService;

        public ErrandDetailsModel(ApplicationDbContext context, IErrandService errandService, ITechnicianService technicianService)
        {
            _context = context;
            _errandService = errandService;
            _technicianService = technicianService;
        }
        public ErrandModel Errand { get; set; }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ErrandStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastEdited { get; set; }
        public string CreatedBy { get; set; }
        public TechnicianModel Technician { get; set; }
        public List<ErrandCommentModel> Comments { get; set; }



        public async Task OnGetAsync(string elevatorId, string errandId)
        {
            Errand = await _errandService.GetErrandByIdAsync(Guid.Parse(errandId));
            //Title = Errand.Title;
            //Description = Errand.Description;
            //Status = Errand.Status;
            //CreatedAt = Errand.CreatedAt;
            //LastEdited = Errand.LastEdited;
            //CreatedBy = Errand.CreatedBy;
            Technician = _technicianService.GetTechnicianById(Errand.Technician.Id);
        }
    }
}
