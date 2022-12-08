using System.Net;
using Grupp3_Elevator.Models;

namespace Grupp3_Elevator.Services.Errand;

public interface IErrandService
{
    Task<ErrandModel> GetErrandByIdAsync(string errandId);
    Task<List<ErrandModel>> GetErrandsAsync();

    Task<string> CreateErrandAsync(string elevatorId, string title, string description, string createdBy, string technicianId);

    Task<List<ErrandModel>> GetErrandsFromElevatorIdAsync(string elevatorId);

    Task<ErrandModel> EditErrandAsync(string errandId, ErrandModel inputErrand, string technicianId,
        List<ErrandCommentModel> comments);

    Task<HttpStatusCode> DeleteErrandAsync(string errandId);
}