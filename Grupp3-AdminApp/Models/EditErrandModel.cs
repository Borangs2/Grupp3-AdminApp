using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Grupp3_Elevator.Models
{
    public class EditErrandModel
    {
        [Required]
        [Column(TypeName = "nvarchar(300)")]
        public string Title { get; set; } = "";
        [Required]
        [Column(TypeName = "nvarchar(2000)")]
        public string Description { get; set; } = "";
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public ErrandStatus Status { get; set; } = ErrandStatus.InProgress;
        public DateTime LastEdited { get; set; } = DateTime.Now;
        [Required]
        public TechnicianModel Technician { get; set; }
        [Required]
        public List<ErrandCommentModel> Comments { get; set; }
    }
}
