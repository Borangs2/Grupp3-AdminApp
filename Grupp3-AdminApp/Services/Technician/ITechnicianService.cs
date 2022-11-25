using Grupp3_Elevator.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Grupp3_Elevator.Services.Technician
{
    public interface ITechnicianService
    {
        Task<TechnicianModel> GetTechnicianById(string technicianId);
        Task<List<TechnicianModel>> GetTechnicians();
        Task<TechnicianModel> GetTechnicianFromErrandId(string errandId);
        Task<List<SelectListItem>> SelectTechnician();
        Task<List<SelectListItem>> SelectTechnicianEdit(string technicianId);
    }
}
