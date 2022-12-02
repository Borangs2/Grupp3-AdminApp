using Grupp3_Elevator.Models;

namespace Grupp3_Elevator.Services;

public interface IElevatorService
{
    Task<ElevatorDeviceItem>? GetElevatorDeviceByIdAsync(string elevatorId);
    ElevatorModel? GetElevatorById(string elevatorId);
    Task<List<ElevatorDeviceItem>> GetElevatorsAsync();
}