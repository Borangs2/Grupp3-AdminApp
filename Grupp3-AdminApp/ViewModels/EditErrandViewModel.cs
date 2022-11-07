using Grupp3_Elevator.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Grupp3_AdminApp.ViewModels
{
    public enum ErrandStatus
    {
        NotStarted,
        InProgress,
        Done
    }

    public class EditErrandViewModel
    {
        [Required]
        [Column(TypeName = "nvarchar(300)")]
        public string Title { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(2000)")]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public ErrandStatus Status { get; set; }

        public DateTime LastEdited { get; set; }
        
        [Required]
        [Column(TypeName = "nvarchar(200)")]
        public string CreatedBy { get; set; }

        [Required]
        public TechnicianModel Technician { get; set; }

    }
}
