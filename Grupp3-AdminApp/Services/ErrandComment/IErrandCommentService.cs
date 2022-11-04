using Grupp3_Elevator.Models;

namespace Grupp3_AdminApp.Services.ErrandComment
{
    public interface IErrandCommentService
    {

        ErrandCommentModel GetCommentsById(Guid commentId);
        List<ErrandCommentModel> GetComments();
        //ErrandCommentModel GetErrandCommentFromErrandId(string errandId);

    }
}
