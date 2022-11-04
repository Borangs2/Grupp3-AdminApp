using Dapper;
using Grupp3_Elevator.Data;
using Grupp3_Elevator.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public async Task<ErrandModel>? GetErrandByIdAsync(Guid errandId)
        {
            var result = _context.Errands.Include(c => c.Comments).FirstOrDefault(e => e.Id == errandId);

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
        //public string CreateErrandAsync(string elevatorId, string Title, string Description, string CreatedBy, Guid TechnicianId)
        //{
        //    var elevator = _elevatorService.GetElevatorById(elevatorId);

        //    var errand = new ErrandModel
        //    {
        //        Id = Guid.NewGuid(),
        //        Title = Title,
        //        Description = Description,
        //        Status = ErrandStatus.NotStarted,
        //        CreatedAt = DateTime.Now,
        //        LastEdited = DateTime.Now,
        //        CreatedBy = CreatedBy,
        //        TechnicianId = TechnicianId,
        //        Comments = new List<ErrandCommentModel>()
        //    };
        //    elevator.Errands.Add(errand);
        //    _context.SaveChanges();

        //    return errand.Id.ToString();
        //}

        public List<ErrandModel> GetErrandsFromElevatorId(string elevatorId)
        {
            var result = _context.Elevators.Include(c => c.Errands).FirstOrDefault(e => e.Id == Guid.Parse(elevatorId));

            foreach (var errand in result.Errands)
            {
                errand.Technician = _technicianService.GetTechnicianFromErrandId(errand.Id.ToString());
                //errand.Comments = _commentService.GetCommentFromErrandId(errand.Id.ToString());
            }

            if (result == null)
                return null!;
            return result.Errands;
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

        public Task<EditErrandModel> EditErrandAsync(Guid errandId)
        {
            throw new NotImplementedException();
        }
    }
}
