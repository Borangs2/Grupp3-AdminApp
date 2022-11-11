using Grupp3_Elevator.Models;

namespace Grupp3_AdminApp.Services.ErrandComment
{
    public interface IErrandCommentService
    {
        Task<ErrandCommentModel> GetErrandCommentsByIdAsync(string commentId);
        Task<List<ErrandCommentModel>> GetErrandComments();
        Task<List<ErrandCommentModel>> GetErrandCommentsFromErrandId(string errandId);
        Task<ErrandCommentModel> CreateErrandComment(ErrandModel errand, string chosenSelectTechnician, string content);
    }
}
