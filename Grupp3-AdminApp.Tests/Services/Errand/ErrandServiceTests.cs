using Grupp3_AdminApp.Services.ErrandComment;
using Grupp3_Elevator.Data;
using Grupp3_Elevator.Services;
using Grupp3_Elevator.Services.Errand;
using Grupp3_Elevator.Services.Technician;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminAppTests.Services.Errand
{
    [TestClass]
    public class ErrandServiceTests
    {
        private ApplicationDbContext _context;
        private ErrandService _sut;
        private readonly ErrandService _errandService;
        private readonly IElevatorService _elevatorService;
        private readonly ITechnicianService _technicianService;
        private readonly IErrandCommentService _errandCommentService;

        [TestInitialize]
        public void Initialize()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(contextOptions);
            _context.Database.EnsureCreated();

            _sut = new ErrandService(_context, _elevatorService, _technicianService, _errandCommentService);
        }

        [TestMethod]
        public void EditErrand_Should_Return

        //private readonly ErrandService _errandService;
        //private readonly IElevatorService _elevatorService;
        //private readonly ITechnicianService _technicianService;

        //public ErrandServiceTests()
        //{
        //    var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("AdminApp").Options;
        //    _context = new ApplicationDbContext(options);
        //    _errandService = new ErrandService(_context, new ElevatorService(_context), new TechnicianService(_context), new ErrandCommentService(_context));
        //}

        [TestMethod]
        public void CreateErrand_Should_Return_ErrandId()
        {
            //ARRANGE
            var elevator = _elevatorService.GetElevatorById("TestElevatorId");
            var technician = _technicianService.GetTechnicianById("testTechnician");

            //ACT
            var errandId = _errandService.CreateErrandAsync(elevator.Id.ToString(), "TestTitle", "TestDescription", "TestCreatedBy", technician);
            var errand = _errandService.GetErrandByIdAsync(errandId);

            //ASSERT
            Assert.AreEqual(errandId, errand.Id);

        }
    }
}
