using System.ComponentModel.DataAnnotations.Schema;
using Grupp3_Elevator.Models;
using Microsoft.Build.Framework;

namespace Grupp3_AdminApp.ViewModels
{
    public class ErrandCommentViewModel
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public TechnicianModel Author { get; set; }
        public DateTime PostedAt { get; set; }
    }
}
