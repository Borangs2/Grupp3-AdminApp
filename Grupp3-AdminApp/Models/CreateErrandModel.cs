using Grupp3_AdminApp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Grupp3_Elevator.Models;

//public class CreateErrandModel
//{
//    [Key] public Guid Id { get; set; } = Guid.NewGuid();

//    [Required]
//    [Column(TypeName = "nvarchar(300)")]
//    public string Title { get; set; } = "";

//    [Required]
//    [Column(TypeName = "nvarchar(2000)")]
//    public string Description { get; set; } = "";

//    [Required]
//    [Column(TypeName = "nvarchar(100)")]
//    public ErrandStatus Status { get; set; } = ErrandStatus.NotStarted;

//    public DateTime LastEdited { get; set; } = DateTime.Now;
//    public DateTime CreatedAt { get; set; } = DateTime.Now;

//    [Required]
//    [Column(TypeName = "nvarchar(200)")]
//    public string CreatedBy { get; set; }

//    [Required] public TechnicianModel Technician { get; set; } = null!;

//    [Required] public List<ErrandCommentModel> Comments { get; set; } = null!;
//}