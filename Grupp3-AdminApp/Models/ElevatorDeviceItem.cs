using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Grupp3_Elevator.Models;
public class ElevatorDeviceItem
{
    public enum ElevatorStatus
    {
        Disabled /*Elevator off, doors closed*/,
        Idle /*Elevator on, doors closed, not running*/,
        DoorsOpen /*Elevator on, doors open, not running*/,
        Running /*Elevator on, doors closed, running*/,
        Error /*Elevator error*/,
        Unknown /*Status Unknown*/
    }

    public Guid Id { get; set; } = Guid.NewGuid();
    [Column(TypeName = "nvarchar(250)")]
    public string Name { get; set; } = "";
    [Column(TypeName = "nvarchar(100)")]
    public ElevatorStatus Status { get; set; } = ElevatorStatus.Disabled;
    [Column(TypeName = "bit")]
    public bool DoorStatus { get; set; } = false;
    public int CurrentLevel { get; set; } = 0;
    public int TargetLevel { get; set; } = 0;


    public void OpenDoors()
    {
        DoorStatus = true;
    }

    public void CloseDoors()
    {
        DoorStatus = false;
    }

    public void GoToFloor()
    {
        throw new NotImplementedException();
    }

    public void ResetElevator()
    {
        throw new NotImplementedException();
    }
}