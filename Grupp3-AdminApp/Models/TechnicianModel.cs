using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Grupp3_Elevator.Models;

public class TechnicianModel
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    [Column(TypeName = "nvarchar(200)")]
    public string Name { get; set; }

    public TechnicianModel()
    {

    }
    public TechnicianModel(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
    public TechnicianModel(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}