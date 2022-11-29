using Grupp3_Elevator.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;

namespace Grupp3_Elevator.Services.Errand
{
    public interface IErrandService
    {
        Task<ErrandModel> GetErrandByIdAsync(string errandId);
        Task<List<ErrandModel>> GetErrandsAsync();
        Task<string> CreateErrandAsync(string elevatorId, string Title, string Description, string CreatedBy, string TechnicianId);
        Task<List<ErrandModel>> GetErrandsFromElevatorIdAsync(string elevatorId);
        Task<ErrandModel> EditErrandAsync(string errandId, ErrandModel inputErrand, string technicianId, List<ErrandCommentModel> comments);
        Task<HttpStatusCode> DeleteErrandAsync(string errandId);
    }

}