using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Grupp3_Elevator.Models;

public class ErrandCommentModel
{
    [Key]
    [Required]
    public Guid Id { get; set; }
    [Required]
    [Column(TypeName = "nvarchar(2000)")]
    public string Content { get; set; }
    [Required]
    public Guid Author { get; set; }
    [Required]
    public DateTime PostedAt { get; set; }
}