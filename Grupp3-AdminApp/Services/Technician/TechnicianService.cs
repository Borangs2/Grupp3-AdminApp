using Grupp3_Elevator.Data;
using Grupp3_Elevator.Models;
using Microsoft.EntityFrameworkCore;

namespace Grupp3_Elevator.Services.Technician;

class TechnicianService : ITechnicianService
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
    public TechnicianModel? GetTechnicianById(Guid technicianId)
    {
        return _context.Technicians.FirstOrDefault(t => t.Id == technicianId);
    }

    /// <summary>
    /// Gets all technicians
    /// </summary>
    /// <returns>A list of <see cref="TechnicianModel"/></returns>
    public List<TechnicianModel> GetTechnicians()
    {
        return _context.Technicians.ToList();
    }
    public TechnicianModel GetTechnicanFromErrandId(string errandId)
    {
        var result = _context.Errands.Include(e => e.Technician).FirstOrDefault(e => e.Id == Guid.Parse(errandId));
        return result.Technician;
    }
}