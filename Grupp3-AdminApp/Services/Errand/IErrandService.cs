using Grupp3_Elevator.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Grupp3_Elevator.Services.Errand
{
    public interface IErrandService
    {
        Task<ErrandModel>? GetErrandByIdAsync(string errandId);

        List<ErrandModel> GetErrands();

        string CreateErrandAsync(string elevatorId, string Title, string Description, string CreatedBy, string TechnicianId);
        List<ErrandModel> GetErrandsFromElevatorId(string elevatorId);
        Task<string> EditErrandAsync(string errandId, ErrandModel errand, string TechnicianId);
        List<SelectListItem> SelectTechnician();

        List<SelectListItem> SelectTechnicianEdit(string technicianId);
    }

}
