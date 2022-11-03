using Dapper;
using Grupp3_Elevator.Data;
using Grupp3_Elevator.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Grupp3_Elevator.Services.Errand
{
    public class ErrandService : IErrandService
    {
        private readonly ApplicationDbContext _context;
        private readonly IElevatorService _elevatorService;

        public ErrandService(ApplicationDbContext context, IElevatorService elevatorService)
        {
            _context = context;
            _elevatorService = elevatorService;
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

        public Task<EditErrandModel> EditErrandAsync(string errandId)
        {
            throw new NotImplementedException();
        }
    }
}
