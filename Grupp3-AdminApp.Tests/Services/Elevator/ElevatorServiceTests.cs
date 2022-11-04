using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grupp3_Elevator.Services;
using Grupp3_Elevator.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using Grupp3_Elevator.Models;

namespace AdminAppTests.Services.Elevator
{


[TestClass]
    public class ElevatorServiceTests
    {
        private readonly ElevatorService _sut;
        private readonly ApplicationDbContext _context;

        public ElevatorServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("AdminApp")
                .Options;

            _context = new ApplicationDbContext(options);

            _sut = new ElevatorService(_context);

            var data = new TestDataInitializer(_context);
            data.SeedData();
        }

        [TestMethod]
        public async Task CheckIfElevatorsIsNotNull_ReturnsNotNull()
        {
            //Arrange


            //Act
            var result = await _sut.GetElevatorsAsync();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result[0]);
        }


        [TestMethod]
        public async Task CheckIfElevatorGetsNonNullId_ReturnsNotEmptyGuid()
        {
            //Arrange


            //Act
            var result = await _sut.GetElevatorsAsync();

            //Assert
            Assert.AreNotEqual(Guid.Empty, result[0].Id);
        }


    }
}
