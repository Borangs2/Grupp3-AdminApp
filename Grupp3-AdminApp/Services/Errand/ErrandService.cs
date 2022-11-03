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
        public async Task<ErrandModel>? GetErrandByIdAsync(string errandId)
        {
            var result = _context.Errands.Include(c => c.Comments).FirstOrDefault(e => e.Id.ToString() == errandId);

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
            var result = _context.Elevators.Include(c => c.Errands).FirstOrDefault(e => e.Id.ToString() == elevatorId);

            if (result == null)
                return null!;
            return result.Errands;
        }
        

        public async Task<ErrandModel> EditErrandAsync(string errandId, ErrandModel errand)
        {
            ErrandModel errandToEdit = await GetErrandByIdAsync(errandId);

            errandToEdit.Title = errand.Title;
            errandToEdit.Description = errand.Description;
            errandToEdit.LastEdited = DateTime.Now;
            errandToEdit.Status = errand.Status;
            errandToEdit.CreatedBy = errand.CreatedBy;

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
    }
}
