using Grupp3_Elevator.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Grupp3_Elevator.Services.Errand
{
    public interface IErrandService
    {
        ErrandModel GetErrandByIdAsync(string errandId);

        List<ErrandModel> GetErrands();
        List<ErrandModel> GetErrandsById(string elevatorId);
        ErrandModel GetErrandById(string errandId);
        string CreateErrandAsync(string elevatorId, string Title, string Description, string CreatedBy, string TechnicianId);
        List<ErrandModel> GetErrandsFromElevatorId(string elevatorId);
        Task<string> EditErrandAsync(ErrandModel errand);
        List<SelectListItem> SelectTechnician();

        List<SelectListItem> SelectTechnicianEdit(string technicianId);
    }

}
