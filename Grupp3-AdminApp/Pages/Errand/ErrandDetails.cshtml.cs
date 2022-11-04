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
        private readonly ITechnicianService _technicianService;
        private readonly IErrandCommentService _errandCommentService;

        public ErrandDetailsModel(IElevatorService elevatorService, IErrandService errandService, ITechnicianService technicianService, IErrandCommentService errandCommentService)
        {
            _elevatorService = elevatorService;
            _errandService = errandService;
            _technicianService = technicianService;
            _errandCommentService = errandCommentService;
        }

        public ErrandModel Errand { get; set; }
        public TechnicianModel Technician { get; set; }
        public List<ErrandCommentModel> Comments { get; set; }
        public ElevatorDeviceItem Elevator { get; set; }



        public async Task OnGetAsync(string elevatorId, string errandId)
        {
            Elevator = await _elevatorService.GetElevatorDeviceByIdAsync(elevatorId);
            Errand = await _errandService.GetErrandByIdAsync(errandId);
            Technician = _technicianService.GetTechnicianById(Errand.Technician.Id.ToString());
            Comments = _errandCommentService.GetErrandCommentsFromErrandId(errandId);
        }
    }
}
