using Dapper;
using Grupp3_Elevator.Data;
using Grupp3_Elevator.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Xml.Linq;
using Grupp3_Elevator.Services.Technician;

namespace Grupp3_Elevator.Services.Errand
{
    public class ErrandService : IErrandService
    {
        private readonly ApplicationDbContext _context;
        private readonly ITechnicianService _technicianService;

        public ErrandService(ApplicationDbContext context, ITechnicianService technicianService)
        {
            _context = context;
            _technicianService = technicianService;
        }
        public async Task<ErrandModel>? GetErrandByIdAsync(string errandId)
        {
            var result = _context.Errands.Include(c => c.Comments).Include(t => t.Technician).FirstOrDefault(e => e.Id.ToString() == errandId);

            if (result == null)
                return null!;
            return result;
        }
        public List<ErrandModel> GetErrands()
        {
            var result = _context.Errands.Include(c => c.Comments).ToList();

            if (result == null)
                return null!;
            return result;
        }
       

        public List<ErrandModel> GetErrandsFromElevatorId(string elevatorId)
        {
            var result = _context.Elevators.Include(c => c.Errands).FirstOrDefault(e => e.Id == Guid.Parse(elevatorId));

            foreach (var errand in result.Errands)
            {
                errand.Technician = _technicianService.GetTechnicanFromErrandId(errand.Id.ToString());
                //errand.Comments = _commentService.GetCommentFromErrandId(errand.Id.ToString());
            }

            if (result == null)
                return null!;
            return result.Errands;
        }
        

        public async Task<ErrandModel> EditErrandAsync(string errandId, ErrandModel errand, string technicianId)
        {
            ErrandModel errandToEdit = await GetErrandByIdAsync(errandId);

            errandToEdit.Title = errand.Title;
            errandToEdit.Description = errand.Description;
            errandToEdit.LastEdited = DateTime.Now;
            errandToEdit.Status = errand.Status;
            errandToEdit.CreatedBy = errand.CreatedBy;
            errandToEdit.Technician = _technicianService.GetTechnicianById(Guid.Parse(technicianId));

            _context.SaveChanges();

            return errand;

            //return RedirectToPage("/Errand/ErrandDetails", new { Id = errandId });
        }

        public List<SelectListItem> SelectTechnician()
        {
            var technicians = _context.Technicians.Select(t => new SelectListItem
            {
                Text = t.Name.ToString(),
                Value = t.Id.ToString()

            }).ToList();

            technicians.Insert(0, new SelectListItem
            {
                Value = "",
                Text = "Please select technician"
            });
            return technicians;
        }

        public List<SelectListItem> SelectTechnicianEdit(string technicianId)
        {
            var technicians = _context.Technicians.Select(t => new SelectListItem
            {
                Text = t.Name.ToString(),
                Value = t.Id.ToString()

            }).OrderBy(t => t.Value != technicianId).ToList();


            return technicians;
        }
    }
}
