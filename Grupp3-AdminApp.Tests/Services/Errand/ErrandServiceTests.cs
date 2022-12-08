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
using System.Net;
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

            _sut = new ErrandService(_context, new ElevatorService(_context), new TechnicianService(_context));

            var data = new TestDataInitializer(_context);
            data.SeedData();
        }

        [TestMethod]
        public async Task CreateErrandAsync_ShouldReturnErrandId()
        {
            //ARRANGE

            //ACT
            var errandId = await _sut.CreateErrandAsync("5435f3c3-56f7-49da-8ef4-24937f71fd70", "TestTitle", "TestDescription", "TestCreatedBy", "62e4a265-ceb7-4254-81f9-7d4a78cfbed8");
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
        public async Task EditErrandAsync_ShouldReturnCorrectType()
        {
            //Arrange
            var errand = _context.Errands.FirstOrDefault(i => i.Id.ToString() == "9f091fd6-9657-4db3-a41c-7bb9e24a43fd");

            //Act
            var result = await _sut.EditErrandAsync(errand.Id.ToString(), errand, "62e4a265-ceb7-4254-81f9-7d4a78cfbed8", errand.Comments);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ErrandModel));
        }

        [TestMethod]
        public async Task GetErrandsAsync_ShouldReturnAllErrands()
        {
            //ARRANGE
            var allErrands = _context.Errands.Count();

            //ACT
            var errands = await _sut.GetErrandsAsync();

            //ASSERT
            Assert.AreEqual(allErrands, errands.Count());
        }

        [TestMethod]
        public async Task GetErrandsAsync_ShouldReturnListOfErrandModels()
        {
            //ARRANGE

            //ACT
            var errands = await _sut.GetErrandsAsync();

            //ASSERT
            Assert.IsInstanceOfType(errands, typeof(List<ErrandModel>));
        }

        [TestMethod]
        public async Task GetErrandsAsync_ReturnShouldContainListOfComments()
        {
            //ARRANGE

            //ACT
            var errands = await _sut.GetErrandsAsync();

            var errand = errands.First();

            //ASSERT
            Assert.IsInstanceOfType(errand.Comments, typeof(List<ErrandCommentModel>));
        }

        [TestMethod]
        public async Task GetErrandsAsync_ReturnShouldContainTechnicianModel()
        {
            //ARRANGE

            //ACT
            var errands = await _sut.GetErrandsAsync();

            var errand = errands.First();

            //ASSERT
            Assert.IsInstanceOfType(errand.Technician, typeof(TechnicianModel));
        }

        [TestMethod]
        public async Task GetErrandByIdAsync_ReturnShouldContainListOfComments()
        {
            //ARRANGE

            //ACT
            var result = _sut.GetErrandByIdAsync("9f091fd6-9657-4db3-a41c-7bb9e24a43fd");

            //ASSERT
            Assert.IsInstanceOfType(result.Result.Comments, typeof(List<ErrandCommentModel>));
        }

        [TestMethod]
        public async Task GetErrandByIdAsync_ReturnShouldContainTechnicianModel()
        {
            //ARRANGE

            //ACT
            var result = _sut.GetErrandByIdAsync("9f091fd6-9657-4db3-a41c-7bb9e24a43fd");

            //ASSERT
            Assert.IsInstanceOfType(result.Result.Technician, typeof(TechnicianModel));
        }

        [TestMethod]
        public async Task GetErrandByIdAsync_ShouldReturnCorrectErrand()
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
        public async Task GetErrandByIdAsync_ShouldReturnErrandModel()
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
        public async Task GetErrandsFromElevatorId_ShouldReturnListOfErrandModel()
        {
            ////ARRANGE

            ////ACT
            var errands = await _sut.GetErrandsFromElevatorIdAsync("5435f3c3-56f7-49da-8ef4-24937f71fd70");

            ////ASSERT
            Assert.IsInstanceOfType(errands, typeof(List<ErrandModel>));
        }

        [TestMethod]
        public async Task GetErrandsFromElevatorIdAsync_ReturnShouldContainTechnicianModel()
        {
            //ARRANGE

            //ACT
            var result = await _sut.GetErrandsFromElevatorIdAsync("5435f3c3-56f7-49da-8ef4-24937f71fd70");
            var errand = result.First();
            //ASSERT
            Assert.IsInstanceOfType(errand.Technician, typeof(TechnicianModel));
        }

        [TestMethod]
        public async Task GetErrandsFromElevatorIdAsync_ReturnShouldContainListOfComments()
        {
            //ARRANGE

            //ACT
            var result = await _sut.GetErrandsFromElevatorIdAsync("5435f3c3-56f7-49da-8ef4-24937f71fd70");
            var errand = result.First();
            //ASSERT
            Assert.IsInstanceOfType(errand.Comments, typeof(List<ErrandCommentModel>));
        }

        [TestMethod]
        public async Task EnsureDeleteErrandDeletesErrand_ReturnsOk()
        {
            //Arrange
            await _sut.CreateErrandAsync("5435f3c3-56f7-49da-8ef4-24937f71fd70", "ShouldBeDeleted", "", "", "62e4a265-ceb7-4254-81f9-7d4a78cfbed8");

            //Act
            var errand = _context.Errands.FirstOrDefault(e => e.Title == "ShouldBeDeleted");
            var result = await _sut.DeleteErrandAsync(errand.Id.ToString());

            //Assert
            Assert.AreEqual(result, HttpStatusCode.OK);
            Assert.IsNull(_context.Errands.FirstOrDefault(e => e.Title == "ShouldBeDeleted"));
        }

        [TestMethod]
        public async Task EnsureDeleteErrandAsyncThrowsWhenIncorrectIdUsed_ReturnsBadRequest()
        {
            //Arrange


            //Act
            var result = await _sut.DeleteErrandAsync("93a0b8df-093a-484c-a4e1-89d55f9ea829"); //Doesn't exist

            //Assert
            Assert.AreEqual(result, HttpStatusCode.NotFound);
        }
    }
}