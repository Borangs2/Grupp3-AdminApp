using Grupp3_Elevator.Models;

namespace Grupp3_Elevator.Services.Technician
{
    public interface ITechnicianService
    {
        TechnicianModel? GetTechnicianById(Guid technicianId);
        List<TechnicianModel> GetTechnicians();
        TechnicianModel GetTechnicanFromErrandId(string errandId);
    }
}
