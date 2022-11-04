using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminAppTests;
using Grupp3_Elevator.Data;
using Grupp3_Elevator.Models;
using Grupp3_Elevator.Services;
using Grupp3_Elevator.Services.Technician;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Grupp3_AdminApp.Tests.Services.Technician
{
    [TestClass]
    public class TechnicianServiceTests
    {
        private readonly TechnicianService _sut;
        private readonly ApplicationDbContext _context;

        public TechnicianServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("AdminApp")
                .Options;

            _context = new ApplicationDbContext(options);

            _sut = new TechnicianService(_context);

            var data = new TestDataInitializer(_context);
            data.SeedData();
        }

        [TestMethod]
        public void EnsureGetTechnicianByIdReturnsTechnician_ReturnsTechnician()
        {
            //Arrange


            //Act
            var result = _sut.GetTechnicianById("62e4a265-ceb7-4254-81f9-7d4a78cfbed8");

            //Assert
            Assert.IsInstanceOfType(result, typeof(TechnicianModel));
        }

        [TestMethod]
        public void EnsureGetTechnicianByIdReturnsNotNull_ReturnsNotNull()
        {
            //Arrange

            //Act
            var result = _sut.GetTechnicianById("62e4a265-ceb7-4254-81f9-7d4a78cfbed8");

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void EnsureCorrectTechnicianIsReturned_ReturnsCorrectTechnician()
        {
            //Arrange
            var guid = "62e4a265-ceb7-4254-81f9-7d4a78cfbed8";

            //Act
            var result = _sut.GetTechnicianById(guid)!;

            //Assert
            Assert.AreEqual(result.Id, guid);
        }

        [TestMethod]
        public void EnsureGetTechniciansReturnsNotNull_ReturnsNotNull()
        {
            //Arrange


            //Act
            var result = _sut.GetTechnicians();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result[0]);
        }

        [TestMethod]
        public void EnsureGetTechniciansGetsAllTechnicians_ReturnsAListOfAllTechnicians()
        {
            //Arrange
            var technicianCount = _context.Technicians.Count();

            //Act
            var result = _sut.GetTechnicians();

            //Assert
            Assert.AreEqual(result.Count(), technicianCount);
        }


        [TestMethod]
        public void EnsureGetTechnicianFromErrandIdReturnsNotNull_ReturnsNotNull()
        {
            //Arrange
            var title = "tested";
            _context.Errands.FirstOrDefault()!.Title = title;
            _context.SaveChanges();

            //Act
            var result = _sut.GetTechnicianFromErrandId("9f091fd6-9657-4db3-a41c-7bb9e24a43fd");

            //Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual(result.Id, Guid.Empty);
        }

        [TestMethod]
        public void EnsureGetTechnicianFromErrandIdReturnsTechnicianModel_ReturnsTechnicianModel()
        {
            //Arrange


            //Act
            var result = _sut.GetTechnicianFromErrandId("9f091fd6-9657-4db3-a41c-7bb9e24a43fd");

            //Assert
            Assert.IsInstanceOfType(result, typeof(TechnicianModel));
        }

        [TestMethod]
        public void EnsureGetTechniciansFromErrandIdReturnsCorrectTechnician_ReturnsCorrectTechnician()
        {
            var errandGuid = Guid.Parse("9f091fd6-9657-4db3-a41c-7bb9e24a43fd");
            var technicianName = "tested";
            _context.SaveChanges();

            //Arrange
            _context.Errands.FirstOrDefault()!.Id = errandGuid;
            _context.Errands.Include(t => t.Technician).FirstOrDefault()!.Technician.Name = technicianName;

            //Act
            var result = _sut.GetTechnicianFromErrandId(errandGuid.ToString())!;

            //Assert
            Assert.AreEqual(result.Name, technicianName);
        }
    }
}
