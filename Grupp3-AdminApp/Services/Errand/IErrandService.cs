using Grupp3_Elevator.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Grupp3_Elevator.Services.Errand
{
    public interface IErrandService
    {
        Task<ErrandModel>? GetErrandByIdAsync(string errandId);

        List<ErrandModel> GetErrands();

        //string CreateErrandAsync(string elevatorId, string Title, string Description, string CreatedBy, Guid TechnicianId);

        Task<EditErrandModel> EditErrandAsync(Guid errandId);
        List<SelectListItem> SelectTechnician();
    }

}
