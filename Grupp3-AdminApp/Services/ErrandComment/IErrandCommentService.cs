using Grupp3_Elevator.Models;

namespace Grupp3_AdminApp.Services.ErrandComment
{
    public interface IErrandCommentService
    {
        ErrandCommentModel GetCommentsById(string commentId);
        List<ErrandCommentModel> GetErrandCommentsFromErrandId(string errandId);

        //ErrandCommentModel GetErrandCommentFromErrandId(string errandId);

    }
}
