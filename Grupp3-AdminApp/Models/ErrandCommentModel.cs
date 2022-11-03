using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Grupp3_Elevator.Models;

public class ErrandCommentModel
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    [Column(TypeName = "nvarchar(2000)")]
    public string Content { get; set; }
    [Required]
    [Column(TypeName = "nvarchar(200)")]
    public Guid Author { get; set; }
    public DateTime PostedAt { get; set; } = DateTime.Now;

}