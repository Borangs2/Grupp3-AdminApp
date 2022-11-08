using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grupp3_Elevator.Services;
using Grupp3_Elevator.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using Grupp3_Elevator.Models;
using Microsoft.Extensions.Configuration;

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

        //NOTE: Do not test any method that uses the IoTHub. It is more work then worth if it is even possible.

        [TestMethod]
        public void EnsureGetElevatorByIdReturns_ReturnsNotNull()
        {
            //Arrange


            //Act
            var result = _sut.GetElevatorById("5435f3c3-56f7-49da-8ef4-24937f71fd70");

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void EnsureGetElevatorByIdReturnsAnElevatorModel_ReturnsElevatorModel()
        {
            //Arrange


            //Act
            var result = _sut.GetElevatorById("5435f3c3-56f7-49da-8ef4-24937f71fd70");

            //Assert
            Assert.IsInstanceOfType(result, typeof(ElevatorModel));
        }

        [TestMethod]
        public void EnsureGetElevatorByIdReturnsCorrectElevator()
        {
            //Arrange


            //Act
            var result = _sut.GetElevatorById("5435f3c3-56f7-49da-8ef4-24937f71fd70");

            //Assert
            Assert.AreEqual(result.Id.ToString(), "5435f3c3-56f7-49da-8ef4-24937f71fd70");
        }
    }
}
