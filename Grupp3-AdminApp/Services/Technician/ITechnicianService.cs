using Grupp3_Elevator.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Grupp3_Elevator.Services.Technician;

public interface ITechnicianService
{
    Task<TechnicianModel> GetTechnicianByIdAsync(string technicianId);
    Task<List<TechnicianModel>> GetTechniciansAsync();
    Task<TechnicianModel> GetTechnicianFromErrandIdAsync(string errandId);
    Task<List<SelectListItem>> SelectTechniciansAsync();
    Task<List<SelectListItem>> SelectListTechniciansEditAsync(string technicianId);
}