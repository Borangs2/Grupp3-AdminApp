using Grupp3_Elevator.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Grupp3_Elevator.Services.Technician
{
    public interface ITechnicianService
    {
        TechnicianModel? GetTechnicianById(string technicianId);
        List<TechnicianModel> GetTechnicians();
        TechnicianModel GetTechnicianFromErrandId(string errandId);
        List<SelectListItem> SelectTechnician();
        List<SelectListItem> SelectTechnicianEdit(string technicianId);
    }
}
