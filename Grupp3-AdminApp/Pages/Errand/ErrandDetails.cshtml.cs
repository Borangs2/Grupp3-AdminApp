using Grupp3_Elevator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Grupp3_Elevator.Data;
using Grupp3_Elevator.Services.Errand;
using Grupp3_Elevator.Services;
using Grupp3_Elevator.Services.Technician;
using Grupp3_AdminApp.Services.ErrandComment;

namespace Grupp3_Elevator.Pages.Errand
{
    [BindProperties]
    public class ErrandDetailsModel : PageModel
    {
        private readonly IElevatorService _elevatorService;
        private readonly IErrandService _errandService;
        private readonly IErrandCommentService _errandCommentService;

        public ErrandDetailsModel(IElevatorService elevatorService, IErrandService errandService, IErrandCommentService errandCommentService)
        {
            _elevatorService = elevatorService;
            _errandService = errandService;
            _errandCommentService = errandCommentService;
        }

        public ElevatorDeviceItem Elevator { get; set; }
        public ErrandModel Errand { get; set; }
        public ErrandCommentModel ErrandComment { get; set; }

        public async Task OnGetAsync(string elevatorId, string errandId)
        {
            Elevator = await _elevatorService.GetElevatorDeviceByIdAsync(elevatorId);
            Errand = await _errandService.GetErrandByIdAsync(errandId);
        }

        public async Task OnPostAsync(string elevatorId, string errandId)
        {
            Errand = await _errandService.GetErrandByIdAsync(errandId);

            var CreateErrandCommentAsync = _errandCommentService.CreateErrandCommentAsync(Errand, ErrandComment.Content);
        }
    }
}
