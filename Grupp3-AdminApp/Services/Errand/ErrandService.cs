using System.Collections.Immutable;
using Dapper;
using Grupp3_Elevator.Data;
using Grupp3_Elevator.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Xml.Linq;
using Grupp3_Elevator.Pages.Errand;
using Grupp3_Elevator.Services.Technician;
using Grupp3_AdminApp.Services.ErrandComment;

namespace Grupp3_Elevator.Services.Errand
{
    public class ErrandService : IErrandService
    {
        private readonly ApplicationDbContext _context;
        private readonly IElevatorService _elevatorService;
        private readonly ITechnicianService _technicianService;
        private readonly IErrandCommentService _errandCommentService;

        public ErrandService(ApplicationDbContext context, IElevatorService elevatorService, ITechnicianService technicianService)
        {
            _context = context;
            _elevatorService = elevatorService;
            _technicianService = technicianService;
            //_errandCommentService = errandCommentService;
        }
        public ErrandModel GetErrandByIdAsync(string errandId)
        {          
            var result = _context.Errands.Include(a => a.Technician).Include(b => b.Comments).FirstOrDefault(aa => aa.Id.ToString() == errandId);

            result.Technician = _technicianService.GetTechnicianFromErrandId(errandId);
            result.Comments = _context.ErrandComments.Select(c => c).Where(c => c.Id == result.Id).ToList();

            if (result == null)
                return null!;
            return result;
        }

        public ErrandModel GetErrandById(string errandId)
        {
            var result = _context.Errands.Include(e => e.Comments).FirstOrDefault(e => e.Id == Guid.Parse(errandId));
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
                errand.Technician = _technicianService.GetTechnicianFromErrandId(errand.Id.ToString());
                errand.Comments = _context.ErrandComments.Select(c => c).Where(c => c.Id == errand.Id).ToList();
            }

            if (result == null)
                return null!;
            return result.Errands;
        }

        public string CreateErrandAsync(string elevatorId, string Title, string Description, string CreatedBy, string TechnicianId)
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
            elevator.Errands.Add(errand);
            _context.SaveChanges();

            var id = errand.Id.ToString();
            return id;
        }

        public async Task<string> EditErrandAsync(string errandId, ErrandModel errand, string technicianId, List<ErrandCommentModel> comments)
        {
            ErrandModel errandToEdit = GetErrandByIdAsync(errandId);

            errandToEdit.Title = errand.Title;
            errandToEdit.Description = errand.Description;
            errandToEdit.LastEdited = DateTime.Now;
            errandToEdit.Status = errand.Status;
            errandToEdit.CreatedBy = errand.CreatedBy;
            errandToEdit.Comments = comments;
            errandToEdit.Technician = _technicianService.GetTechnicianById(technicianId);

            _context.Update(errandToEdit);
            await _context.SaveChangesAsync();

            var id = errandToEdit.Id.ToString();
            return id;
            


            //var id = errandToEdit.Id.ToString();
            //return id;

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
                Value = t.Id.ToString(),

            }).OrderBy(t => t.Value != technicianId).ToList();

            return technicians;
        }
    }
}
