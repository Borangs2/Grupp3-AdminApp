using Grupp3_Elevator.Data;
using Grupp3_Elevator.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Grupp3_Elevator.Services.Technician;

public class TechnicianService : ITechnicianService
{
    private readonly ApplicationDbContext _context;

    public TechnicianService(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get a technician based on the specified <paramref name="technicianId"></paramref>
    /// </summary>
    /// <param name="technicianId"></param>
    /// <returns>A <see cref="TechnicianModel"/></returns>
    public async Task<TechnicianModel> GetTechnicianByIdAsync(string technicianId)
    {
        return _context.Technicians.FirstOrDefault(t => t.Id == Guid.Parse(technicianId));
    }


    /// <summary>
    /// Gets all technicians
    /// </summary>
    /// <returns>A list of <see cref="TechnicianModel"/></returns>
    public async Task<List<TechnicianModel>> GetTechniciansAsync()
    {
        return _context.Technicians.ToList();
    }

    /// <summary>
    /// Gets the technician assigned to a specific errand
    /// </summary>
    /// <param name="errandId"></param>
    /// <returns>A <see cref="TechnicianModel"/> or <see langword="null"></see> if none exists</returns>
    public async Task<TechnicianModel> GetTechnicianFromErrandIdAsync(string errandId)
    {
        var result = _context.Errands.Include(e => e.Technician).FirstOrDefault(e => e.Id == Guid.Parse(errandId));

        if (result == null || result.Technician.Id == Guid.Empty)
            return null;
        return result.Technician;
    }

    public async Task<List<SelectListItem>> SelectTechniciansAsync()
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
    public async Task<List<SelectListItem>> SelectListTechniciansEditAsync(string technicianId)
    {
        var technicians = _context.Technicians.Select(t => new SelectListItem
        {
            Text = t.Name.ToString(),
            Value = t.Id.ToString(),

        }).OrderBy(t => t.Value != technicianId).ToList();
        return technicians;
    }
}