using Grupp3_Elevator.Models;

namespace Grupp3_AdminApp.Services.ErrandComment
{
    public interface IErrandCommentService
    {
        ErrandCommentModel GetCommentsById(string commentId);
        List<ErrandCommentModel> GetComments();
        List<ErrandCommentModel> GetErrandCommentsFromErrandId(string errandId);
        Task<string> CreateErrandComment(ErrandModel errand, string technicianId, string content);
    }
}
