using Grupp3_Elevator.Models;

namespace Grupp3_AdminApp.Services.ErrandComment
{
    public interface IErrandCommentService
    {
        Task<ErrandCommentModel> GetErrandCommentsByIdAsync(string commentId);
        Task<List<ErrandCommentModel>> GetErrandCommentsAsync();
        Task<List<ErrandCommentModel>> GetErrandCommentsFromErrandIdAsync(string errandId);
        Task<ErrandCommentModel> CreateErrandCommentAsync(ErrandModel errand, string chosenSelectTechnician, string content);
    }
}