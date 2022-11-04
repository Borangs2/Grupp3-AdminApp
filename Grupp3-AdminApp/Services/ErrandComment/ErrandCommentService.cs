using Grupp3_Elevator.Data;
using Grupp3_Elevator.Models;
using Microsoft.EntityFrameworkCore;

namespace Grupp3_AdminApp.Services.ErrandComment
{
    public class ErrandCommentService : IErrandCommentService
    {
        private readonly ApplicationDbContext _context;

        public ErrandCommentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public ErrandCommentModel GetCommentsById(Guid commentId)
        {
            return _context.ErrandComments.FirstOrDefault(e => e.Id == commentId);
        }
       
        public List<ErrandCommentModel> GetComments()
        {
            return _context.ErrandComments.ToList();
        }

        //public ErrandCommentModel GetErrandCommentFromErrandId(string errandId)
        //{

        //    var result = _context.Errands.Include(e => e.Comments).FirstOrDefault(e => e.Id == Guid.Parse(errandId));
        //    return test;

        //}
    }
}
