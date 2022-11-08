using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Grupp3_Elevator.Models;

public class ErrandCommentModel
{
    [Key]
    [Required]
    public Guid Id { get; set; }
    [Required]
    [Column(TypeName = "nvarchar(200)")]
    public string Content { get; set; }
    [Required]
    public TechnicianModel Technician { get; set; }
    [Required]
    public DateTime PostedAt { get; set; }
}