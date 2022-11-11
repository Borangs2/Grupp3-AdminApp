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
        private ApplicationDbContext _context;
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
            var errand = _context.Errands.Last();

            //ASSERT
            Assert.AreEqual(errandId, errand.Id.ToString());
        }

        [TestMethod]
        public void CreateErrandAsync_ShouldCreateErrandModel()
        {
            //ARRANGE

            //ACT
            var errandId = _sut.CreateErrandAsync("5435f3c3-56f7-49da-8ef4-24937f71fd70", "TestTitle", "TestDescription", "TestCreatedBy", "62e4a265-ceb7-4254-81f9-7d4a78cfbed8");
            var errand = _context.Errands.Last();

            //ASSERT
            Assert.IsInstanceOfType(errand, typeof(ErrandModel));
        }

        [TestMethod]
        public async Task GetErrands_ShouldReturnAllErrands()
        {
            //ARRANGE
            var allErrands = _context.Errands.Count();

            //ACT
            var errands = _sut.GetErrandsAsync();

            //ASSERT
            Assert.AreEqual(allErrands, errands);
        }

        [TestMethod]
        public void GetErrands_ShouldReturnListOfErrandModels()
        {
            //ARRANGE

            //ACT
            var errands = _sut.GetErrandsAsync();

            //ASSERT
            Assert.IsInstanceOfType(errands, typeof(List<ErrandModel>));
        }

        [TestMethod]
        public async Task GetErrandByIdAsync_ShouldReturnCorrectErrandAsync()
        {
            //ARRANGE
            var errandToCompare = _context.Errands.FirstOrDefault(e => e.Id.ToString() == "9f091fd6-9657-4db3-a41c-7bb9e24a43fd");
            var errandIdToCompare = errandToCompare.Id.ToString();

            //ACT
            var errand = await _sut.GetErrandByIdAsync(errandIdToCompare);
            var myErrandId = errand.Id.ToString();

            //ASSERT
            Assert.AreEqual(errandIdToCompare, myErrandId);
        }

        [TestMethod]
        public async Task GetErrandByIdAsync_ShouldReturnErrandModelAsync()
        {
            //ARRANGE
            var errandToCompare = _context.Errands.FirstOrDefault(e => e.Id.ToString() == "9f091fd6-9657-4db3-a41c-7bb9e24a43fd");
            var errandIdToCompare = errandToCompare.Id.ToString();

            //ACT
            var errand = await _sut.GetErrandByIdAsync(errandIdToCompare);

            //ASSERT
            Assert.IsInstanceOfType(errand, typeof(ErrandModel));
        }

        [TestMethod]
        public void GetErrandsFromElevatorId_ShouldReturnListOfErrandModel()
        {
            ////ARRANGE

            ////ACT
            //var errands = _sut.GetErrandsFromElevatorId("5435f3c3-56f7-49da-8ef4-24937f71fd70");

            ////ASSERT
            //Assert.IsInstanceOfType(errands, typeof(List<ErrandModel>));
        }

        //[TestMethod]
        //public void EditErrand_ShouldReturnCorrectType()
        //{
        //    //Arrange
        //    var errand = _context.Errands.FirstOrDefault(i => i.Id.ToString() == "9f091fd6-9657-4db3-a41c-7bb9e24a43fd");

        //    //Act
        //    var result = _sut.EditErrandAsync(errand.Id.ToString(), errand, "dacf220b-2c9d-4d1a-a867-92a667de2a11", new List<ErrandCommentModel> { } );

        //    //Assert
        //    Assert.IsInstanceOfType(result, typeof(Task<string>));
        //}
    }
}
