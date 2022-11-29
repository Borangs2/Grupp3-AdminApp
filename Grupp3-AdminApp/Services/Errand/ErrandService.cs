using System.Collections.Immutable;
using Dapper;
using Grupp3_Elevator.Data;
using Grupp3_Elevator.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Data;
using System.Net;
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

        public ErrandService(ApplicationDbContext context, IElevatorService elevatorService, ITechnicianService technicianService)
        {
            _context = context;
            _elevatorService = elevatorService;
            _technicianService = technicianService;
        }
        public async Task<ErrandModel>? GetErrandByIdAsync(string errandId)
        {
            var result = _context.Errands
                .Include(errand => errand.Technician)
                .Include(errand => errand.Comments)
                .FirstOrDefault(e => e.Id == Guid.Parse(errandId));

            if (result == null)
                return null!;
            return result;
        }

        public async Task<List<ErrandModel>> GetErrandsAsync()
        {
            var result = _context.Errands
                .Include(errand => errand.Comments)
                .Include(errand => errand.Technician).ToList();
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
                Technician = await _technicianService.GetTechnicianByIdAsync(TechnicianId),
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
            errandToEdit.Technician = await _technicianService.GetTechnicianByIdAsync(technicianId);

            _context.Update(errandToEdit);
            await _context.SaveChangesAsync();

            return errandToEdit;
        }

        public async Task<HttpStatusCode> DeleteErrandAsync(string errandId)
        {
            var errand = await GetErrandByIdAsync(errandId);
            if (errand != null)
            {
                _context.Errands.Remove(errand);
                return HttpStatusCode.OK;
            }

            return HttpStatusCode.BadRequest;
        }
    }
}