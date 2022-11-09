using Grupp3_Elevator.Data;
using Grupp3_Elevator.Models;
using Grupp3_Elevator.Services.Technician;
using Grupp3_Elevator.Services;
using Microsoft.EntityFrameworkCore;
using Grupp3_Elevator.Services.Errand;
using System.ComponentModel.Design;

namespace Grupp3_AdminApp.Services.ErrandComment
{
    public class ErrandCommentService : IErrandCommentService
    {
        private readonly ApplicationDbContext _context;
        private readonly ITechnicianService _technicianService;

        public ErrandCommentService(ApplicationDbContext context, ITechnicianService technicianService)
        {
            _context = context;
            _technicianService = technicianService;
        }

        public ErrandCommentModel GetCommentsById(string commentId)
        {
            return _context.ErrandComments.FirstOrDefault(e => e.Id == Guid.Parse(commentId));
        }
       
        public List<ErrandCommentModel> GetComments()
        {
            return _context.ErrandComments.ToList();
        }

        public List<ErrandCommentModel> GetErrandCommentsFromErrandId(string errandId)
        {
            var result = _context.Errands.Include(e => e.Comments).FirstOrDefault(e => e.Id == Guid.Parse(errandId));
            return result.Comments;
        }

        public string CreateErrandCommentAsync(ErrandModel errand, string content, string technicianId)
        {
            errand = _context.Errands.Include(a => a.Comments).FirstOrDefault(b => b.Id == errand.Id);

            var errandComment = new ErrandCommentModel
            {
                Id = Guid.NewGuid(),
                Content = content,
                Author = Guid.Parse(technicianId),
                PostedAt = DateTime.Now,
            };

            try
            {

                errand.Comments.Add(errandComment);
                //_context.ErrandComments.Add(errandComment);
                _context.SaveChanges();
            }
            catch
            {
                return errandComment.Id.ToString();
            }


            var id = errandComment.Id.ToString();
            return id;
        }
    }
}
