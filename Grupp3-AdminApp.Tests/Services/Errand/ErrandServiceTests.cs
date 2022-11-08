using Grupp3_AdminApp.Services.ErrandComment;
using Grupp3_Elevator.Data;
using Grupp3_Elevator.Models;
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
        private readonly ApplicationDbContext _context;
        private readonly ErrandService _sut;
        private readonly IElevatorService _elevatorService;

        public ErrandServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("AdminApp").Options;
            _context = new ApplicationDbContext(options);

            _sut = new ErrandService(_context, new ElevatorService(_context), new TechnicianService(_context), new ErrandCommentService(_context));

            var data = new TestDataInitializer(_context);
            data.SeedData();
        }

        [TestMethod]
        public void CreateErrandAsync_ShouldReturnErrandId()
        {
            //ARRANGE

            //ACT
            var errandId = _sut.CreateErrandAsync("5435f3c3-56f7-49da-8ef4-24937f71fd70", "TestTitle", "TestDescription", "TestCreatedBy", "62e4a265-ceb7-4254-81f9-7d4a78cfbed8");
            var errand = _sut.GetErrands().Last();

            //ASSERT
            Assert.AreEqual(errandId, errand.Id.ToString());
        }

        [TestMethod]
        public void CreateErrandAsync_ShouldCreateErrandModel()
        {
            //ARRANGE

            //ACT
            var errandId = _sut.CreateErrandAsync("5435f3c3-56f7-49da-8ef4-24937f71fd70", "TestTitle", "TestDescription", "TestCreatedBy", "62e4a265-ceb7-4254-81f9-7d4a78cfbed8");
            var errand = _sut.GetErrandByIdAsync(errandId);

            //ASSERT
            Assert.IsInstanceOfType(errand, typeof(ErrandModel));
        }

        [TestMethod]
        public void GetErrands_ShouldReturnAllErrands()
        {
            //ARRANGE
            var allErrands = _context.Errands.Count();

            //ACT
            var errands = _sut.GetErrands().Count();

            //ASSERT
            Assert.AreEqual(allErrands,errands);
        }

        [TestMethod]
        public void GetErrands_ShouldReturnListOfErrandModels()
        {
            //ARRANGE

            //ACT
            var errands = _sut.GetErrands();

            //ASSERT
            Assert.IsInstanceOfType(errands, typeof(List<ErrandModel>));
        }

        [TestMethod]
        public void GetErrandByIdAsync_ShouldReturnCorrectErrand()
        {
            //ARRANGE
            var errandIdToCompare = _sut.CreateErrandAsync("5435f3c3-56f7-49da-8ef4-24937f71fd70", "TestTitle", "TestDescription", "TestCreatedBy", "62e4a265-ceb7-4254-81f9-7d4a78cfbed8");
            
            //ACT
            var errand = _sut.GetErrandByIdAsync(errandIdToCompare);
            var myErrandId = errand.Id.ToString();
            
            //ASSERT
            Assert.AreEqual(errandIdToCompare, myErrandId);
        }

        [TestMethod]
        public void GetErrandByIdAsync_ShouldReturnErrandModel()
        {
            //ARRANGE
            var errandId = _sut.CreateErrandAsync("5435f3c3-56f7-49da-8ef4-24937f71fd70", "TestTitle", "TestDescription", "TestCreatedBy", "62e4a265-ceb7-4254-81f9-7d4a78cfbed8");
            
            //ACT
            var errand = _sut.GetErrandByIdAsync(errandId);
            
            //ASSERT
            Assert.IsInstanceOfType(errand, typeof(ErrandModel));
        }

        //[TestMethod]
        //public void EditErrand_Should_Return

        //private readonly ErrandService _errandService;
        //private readonly IElevatorService _elevatorService;
        //private readonly ITechnicianService _technicianService;

        //public ErrandServiceTests()
        //{
        //    var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("AdminApp").Options;
        //    _context = new ApplicationDbContext(options);
        //    _errandService = new ErrandService(_context, new ElevatorService(_context), new TechnicianService(_context), new ErrandCommentService(_context));
        //}
    }
}
