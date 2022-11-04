using Grupp3_Elevator.Models;

namespace Grupp3_Elevator.Services.Technician
{
    public interface ITechnicianService
    {
        TechnicianModel? GetTechnicianById(string technicianId);
        List<TechnicianModel> GetTechnicians();
        TechnicianModel GetTechnicianFromErrandId(string errandId);
    }
}
