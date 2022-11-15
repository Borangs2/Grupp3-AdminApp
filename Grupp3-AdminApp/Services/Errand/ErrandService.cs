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

        public ErrandService(ApplicationDbContext context, IElevatorService elevatorService, ITechnicianService technicianService, IErrandCommentService errandCommentService)
        {
            _context = context;
            _elevatorService = elevatorService;
            _technicianService = technicianService;
            _errandCommentService = errandCommentService;
        }
        public async Task<ErrandModel> GetErrandByIdAsync(string errandId)
        {
            var result = _context.Errands.Include(a => a.Technician).Include(b => b.Comments).FirstOrDefault(aa => aa.Id == Guid.Parse(errandId));

            result.Technician = _technicianService.GetTechnicianFromErrandId(errandId);
            result.Comments = await _errandCommentService.GetErrandCommentsFromErrandIdAsync(errandId);

            if (result == null)
                return null!;
            return result;
        }

        public async Task<List<ErrandModel>> GetErrandsAsync()
        {
            var result = _context.Errands
                .Include(e => e.Comments)
                .Include(e => e.Technician).ToList();
            return result;
        }


        public async Task<List<ErrandModel>> GetErrandsFromElevatorIdAsync(string elevatorId)
        {
            var result = _context.Elevators
                .Include(elevator => elevator.Errands)
                .ThenInclude(errand => errand.Comments)
                .Include(elevator => elevator.Errands)
                .ThenInclude(errand => errand.Technician)
                .FirstOrDefault(e => e.Id == Guid.Parse(elevatorId));

            return result.Errands.ToList();
        }

        public async Task<string> CreateErrandAsync(string elevatorId, string Title, string Description, string CreatedBy, string TechnicianId)
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
            await _context.SaveChangesAsync();

            var id = errand.Id.ToString();
            return id;
        }

        public async Task<ErrandModel> EditErrandAsync(string errandId, ErrandModel inputErrand, string technicianId, List<ErrandCommentModel> comments)
        {
            ErrandModel errandToEdit = await GetErrandByIdAsync(errandId);

            errandToEdit.Title = inputErrand.Title;
            errandToEdit.Description = inputErrand.Description;
            errandToEdit.LastEdited = DateTime.Now;
            errandToEdit.Status = inputErrand.Status;
            errandToEdit.CreatedBy = inputErrand.CreatedBy;
            errandToEdit.Comments = comments;
            errandToEdit.Technician = _technicianService.GetTechnicianById(technicianId);

            _context.Update(errandToEdit);
            await _context.SaveChangesAsync();

            return errandToEdit;
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