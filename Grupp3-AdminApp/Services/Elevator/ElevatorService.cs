using System.Collections.Generic;
using System.Data;
using Dapper;
using Grupp3_Elevator.Data;
using Grupp3_Elevator.Models;
using Microsoft.Azure.Devices;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Grupp3_Elevator.Services;

public class ElevatorService : IElevatorService
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _config;
    private readonly RegistryManager _registryManager;

    public ElevatorService(ApplicationDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
        _registryManager = RegistryManager.CreateFromConnectionString(_config.GetValue<string>("IoTHubConnection"));
    }

    public ElevatorService(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Gets an <see cref="ElevatorDeviceItem"/> based on the specified <paramref name="elevatorId"/>
    /// </summary>
    /// <param name="elevatorId"></param>
    /// <returns>The specified <see cref="ElevatorDeviceItem"/> or <see langword="null"></see> if none exists</returns>
    public async Task<ElevatorDeviceItem>? GetElevatorDeviceByIdAsync(string elevatorId)
    {
        var result = _context.Elevators.FirstOrDefault(e => e.Id == Guid.Parse(elevatorId));

        if (result == null)
        {
            return null!;
        }

        var device = await _registryManager.GetDeviceAsync(elevatorId);
        var twin = await _registryManager.GetTwinAsync(device.Id);

        var elevator = new ElevatorDeviceItem();
        elevator.Id = Guid.Parse(twin.DeviceId);

        try { elevator.Name = twin.Properties.Reported["deviceName"]; }
        catch { elevator.Name = "Name unknown"; }

        try { elevator.Status = twin.Properties.Reported["status"]; }
        catch { elevator.Status = ElevatorDeviceItem.ElevatorStatus.Unknown; }

        try { elevator.DoorStatus = twin.Properties.Reported["doorStatus"]; }
        catch { elevator.DoorStatus = false; }

        try { elevator.CurrentLevel = twin.Properties.Reported["currentLevel"]; }
        catch { elevator.CurrentLevel = 0; }

        try { elevator.TargetLevel = twin.Properties.Reported["targetLevel"]; }
        catch { elevator.TargetLevel = 0; }

        return elevator;
    }

    /// <summary>
    /// Gets an <see cref="ElevatorModel"/> from the database based on the specified <paramref name="elevatorId"/>
    /// </summary>
    /// <param name="elevatorId"></param>
    /// <returns>An <see cref="ElevatorModel"/> or <see langword="null"></see> if none exists</returns>
    public ElevatorModel? GetElevatorById(string elevatorId)
    {
        var result = _context.Elevators.Include(e => e.Errands).FirstOrDefault(e => e.Id == Guid.Parse(elevatorId));
        return result;
    }

    /// <summary>
    /// Gets all elevators from the IoTHub
    /// </summary>
    /// <returns>List of <see cref="ElevatorDeviceItem"/></returns>
    public async Task<List<ElevatorDeviceItem>> GetElevatorsAsync()
    {
        var elevatorList = new List<ElevatorDeviceItem>();
        var result = _registryManager.CreateQuery($"SELECT * FROM devices");

        if (result.HasMoreResults)
        {
            foreach (var twin in await result.GetNextAsTwinAsync())
            {
                var elevator = new ElevatorDeviceItem();
                elevator.Id = Guid.Parse(twin.DeviceId);

                try { elevator.Name = twin.Properties.Reported["deviceName"]; }
                catch { elevator.Name = "Name unknown"; }

                try { elevator.Status = twin.Properties.Reported["status"]; }
                catch { elevator.Status = ElevatorDeviceItem.ElevatorStatus.Unknown; }

                try { elevator.DoorStatus = twin.Properties.Reported["doorStatus"]; }
                catch { elevator.DoorStatus = false; }

                try { elevator.CurrentLevel = twin.Properties.Reported["currentLevel"]; }
                catch { elevator.CurrentLevel = 0; }

                try { elevator.TargetLevel = twin.Properties.Reported["targetLevel"]; }
                catch { elevator.TargetLevel = 0; }

                 elevatorList.Add(elevator);
            }
        }

        return elevatorList;
    }
}