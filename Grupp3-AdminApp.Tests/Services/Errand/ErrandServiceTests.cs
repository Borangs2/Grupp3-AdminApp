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
    internal class ErrandServiceTests
    {
        private readonly ApplicationDbContext _context;
        private readonly ErrandService _errandService;
        private readonly IElevatorService _elevatorService;

        public ErrandServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("AdminApp").Options;
            _context = new ApplicationDbContext(options);
            _errandService = new ErrandService(_context, new TechnicianService(_context), new ErrandCommentService(_context));
        }

        [TestMethod]
        public void GetErrands_ReturnAll()
        {
            //ARRANGE

            //ACT

            //ASSERT
        }

        [TestMethod]
        public void GetErrands_Return_1(int id)
        {
            //ARRANGE

            //ACT

            //ASSERT
        }

        [TestMethod]
        public void GetErrands_Return_3()
        {
            //ARRANGE

            //ACT

            //ASSERT
        }
    }
}
