using Grupp3_Elevator.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Grupp3_Elevator.Services.Errand
{
    public interface IErrandService
    {
        Task<ErrandModel> GetErrandByIdAsync(string errandId);

        Task<List<ErrandModel>> GetErrandsAsync();
        //ErrandModel GetErrandById(string errandId);
        string CreateErrandAsync(string elevatorId, string Title, string Description, string CreatedBy, string TechnicianId);
        Task<List<ErrandModel>> GetErrandsFromElevatorIdAsync(string elevatorId);
        Task<ErrandModel> EditErrandAsync(ErrandModel errand);
        List<SelectListItem> SelectTechnician();

        List<SelectListItem> SelectTechnicianEdit(string technicianId);
    }

}
