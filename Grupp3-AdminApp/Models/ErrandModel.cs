using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Grupp3_Elevator.Models;

public enum ErrandStatus
{
    NotStarted,
    InProgress,
    Done
}

public class ErrandModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }
    [Required]
    [Column(TypeName = "nvarchar(300)")]
    public string Title { get; set; } = "";
    [Required]
    [Column(TypeName = "nvarchar(2000)")]
    public string Description { get; set; } = "";
    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public ErrandStatus Status { get; set; }
    public DateTime LastEdited { get; set; }
    public DateTime CreatedAt { get; set; }
    [Required]
    [Column(TypeName = "nvarchar(200)")]
    public string CreatedBy { get; set; }
    [Required]
    public TechnicianModel Technician { get; set; } = null!;
    [Required]
    public List<ErrandCommentModel> Comments { get; set; } = null!;
}