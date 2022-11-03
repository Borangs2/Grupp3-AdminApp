using Grupp3_Elevator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Grupp3_Elevator.Data;
using Grupp3_Elevator.Services.Errand;
using Grupp3_Elevator.Services;

namespace Grupp3_Elevator.Pages.Errand
{
    public class ErrandDetailsModel : PageModel
    {
        //public Guid Id { get; set; } 
        //public string Title { get; set; }
        //public string Description { get; set; }
        //public ErrandStatus Status { get; set; } 
        //public DateTime LastEdited { get; set; } 
        //public DateTime CreatedAt { get; set; }
        //public Guid CreatedBy { get; set; }
        //public TechnicianModel Technician { get; set; }

        //public class ErrandCommentViewModel
        //{
        //    public Guid Id { get; set; } = Guid.NewGuid();
        //    public string Content { get; set; }
        //    public Guid Author { get; set; }
        //    public DateTime PostedAt { get; set; } = DateTime.Now;
        //}

        private readonly ApplicationDbContext _context;
        private readonly IErrandService _errandService;

        public ErrandDetailsModel(ApplicationDbContext context, IErrandService errandService)
        {
            _context = context;
            _errandService = errandService;
        }
        public ErrandModel Errand { get; set; }

        public async Task OnGetAsync(string elevatorId, string errandId)
        {
            Errand = await _errandService.GetErrandByIdAsync(Guid.Parse(errandId));
        }
    }
}
