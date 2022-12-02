using System.Net;
using Grupp3_Elevator.Data;
using Grupp3_Elevator.Models;
using Grupp3_Elevator.Services.Technician;
using Microsoft.EntityFrameworkCore;

namespace Grupp3_Elevator.Services.Errand;

public class ErrandService : IErrandService
{
    private readonly ApplicationDbContext _context;
    private readonly IElevatorService _elevatorService;
    private readonly ITechnicianService _technicianService;

    public ErrandService(ApplicationDbContext context, IElevatorService elevatorService, ITechnicianService technicianService)
    {
        _context = context;
        _elevatorService = elevatorService;
        _technicianService = technicianService;
    }
    public async Task<ErrandModel>? GetErrandByIdAsync(string errandId)
    {
        var result = _context.Errands
            .Include(errand => errand.Technician)
            .Include(errand => errand.Comments
                .OrderByDescending(comment => comment.PostedAt))
            .FirstOrDefault(e => e.Id == Guid.Parse(errandId));

        return result ?? null!;
    }

    public async Task<List<ErrandModel>> GetErrandsAsync()
    {
        var result = _context.Errands
            .OrderByDescending(errand => errand.Status == ErrandStatus.InProgress)
            .ThenByDescending(errand => errand.Status == ErrandStatus.NotStarted)
            .Include(errand => errand.Comments
                .OrderByDescending(comment => comment.PostedAt))
            .Include(errand => errand.Technician).ToList();

        return result;
    }


    public async Task<List<ErrandModel>> GetErrandsFromElevatorIdAsync(string elevatorId)
    {
        var result = _context.Elevators
            .Include(elevator => elevator.Errands
                .OrderByDescending(errand => errand.Status == ErrandStatus.InProgress)
                .ThenByDescending(errand => errand.Status == ErrandStatus.NotStarted))
            .ThenInclude(errand => errand.Comments
                .OrderByDescending(comment => comment.PostedAt))
            .Include(elevator => elevator.Errands)
            .ThenInclude(errand => errand.Technician)
            .FirstOrDefault(e => e.Id == Guid.Parse(elevatorId));

        return result.Errands.ToList();
    }

    public async Task<string> CreateErrandAsync(string elevatorId, string title, string description, string createdBy, string technicianId)
    {
        var elevator = _elevatorService.GetElevatorById(elevatorId);

        var errand = new ErrandModel
        {
            Id = Guid.NewGuid(),
            Title = title,
            Description = description,
            Status = ErrandStatus.NotStarted,
            CreatedAt = DateTime.Now,
            LastEdited = DateTime.Now,
            CreatedBy = createdBy,
            Technician = await _technicianService.GetTechnicianByIdAsync(technicianId),
            Comments = new List<ErrandCommentModel>()
        };
        elevator.Errands.Add(errand);
        await _context.SaveChangesAsync();

        var id = errand.Id.ToString();
        return id;
    }

    public async Task<ErrandModel> EditErrandAsync(string errandId, ErrandModel inputErrand, string technicianId,
        List<ErrandCommentModel> comments)
    {
        var errandToEdit = await GetErrandByIdAsync(errandId);

        errandToEdit.Title = inputErrand.Title;
        errandToEdit.Description = inputErrand.Description;
        errandToEdit.LastEdited = DateTime.Now;
        errandToEdit.Status = inputErrand.Status;
        errandToEdit.CreatedBy = inputErrand.CreatedBy;
        errandToEdit.Comments = comments;
        errandToEdit.Technician = await _technicianService.GetTechnicianByIdAsync(technicianId);

        _context.Update(errandToEdit);
        await _context.SaveChangesAsync();

        return errandToEdit;
    }

    public async Task<HttpStatusCode> DeleteErrandAsync(string errandId)
    {
        var errand = await GetErrandByIdAsync(errandId);
        if (errand != null)
        {
            foreach (var comment in errand.Comments) _context.RemoveRange(comment);

            _context.Errands.Remove(errand);
            await _context.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
        return HttpStatusCode.NotFound;
    }
}