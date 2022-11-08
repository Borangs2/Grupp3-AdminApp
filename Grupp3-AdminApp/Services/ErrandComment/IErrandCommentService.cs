using Grupp3_Elevator.Models;

namespace Grupp3_AdminApp.Services.ErrandComment
{
    public interface IErrandCommentService
    {
        ErrandCommentModel GetCommentsById(string commentId);
        List<ErrandCommentModel> GetErrandCommentsFromErrandId(string errandId);
        string CreateErrandCommentAsync(ErrandModel errand, string content, string technicianId);
        List<ErrandCommentModel> GetComments();

        //ErrandCommentModel GetErrandCommentFromErrandId(string errandId);

    }
}
