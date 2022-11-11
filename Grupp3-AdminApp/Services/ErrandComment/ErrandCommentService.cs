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

        public async Task<ErrandCommentModel> GetErrandCommentsByIdAsync(string commentId)
        {
            return _context.ErrandComments.FirstOrDefault(e => e.Id == Guid.Parse(commentId));
        }
       
        public async Task<List<ErrandCommentModel>> GetErrandComments()
        {
            return _context.ErrandComments.ToList();
        }

        public async Task<List<ErrandCommentModel>> GetErrandCommentsFromErrandId(string errandId)
        {
            var result = _context.Errands.Include(e => e.Comments).FirstOrDefault(e => e.Id == Guid.Parse(errandId));
            return result.Comments;
        }
        public async Task<ErrandCommentModel> CreateErrandComment(ErrandModel errand, string chosenSelectTechnician, string content)
        {
            var ErrandComment = new ErrandCommentModel
            {
                Id = Guid.NewGuid(),
                Content = content,
                Author = Guid.Parse(chosenSelectTechnician),
                PostedAt = DateTime.Now
            };
            errand.Comments.Add(ErrandComment);
            _context.Entry(ErrandComment).State = EntityState.Added;
            await _context.SaveChangesAsync();

            ErrandCommentModel result = await GetErrandCommentsByIdAsync(ErrandComment.Id.ToString());
            return result;
        }
    }
}
