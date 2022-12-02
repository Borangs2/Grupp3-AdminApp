using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Grupp3_Elevator.Models;

public class ElevatorModel
{
    [Key] public Guid Id { get; set; }

    [Column(TypeName = "nvarchar(1000)")] public string ConnectionString { get; set; } = "";

    public List<ErrandModel> Errands { get; set; }
}