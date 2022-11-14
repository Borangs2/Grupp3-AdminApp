using AdminAppTests;
using Grupp3_AdminApp.Services.ErrandComment;
using Grupp3_Elevator.Data;
using Grupp3_Elevator.Models;
using Grupp3_Elevator.Services;
using Grupp3_Elevator.Services.Errand;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp3_AdminApp.Tests.Services.ErrandComment
{
    [TestClass]
    public class ErrandCommentServiceTests
    {
        private ApplicationDbContext _context;
        private readonly ErrandCommentService _sut;

        public ErrandCommentServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("AdminApp").Options;
            _context = new ApplicationDbContext(options);

            _sut = new ErrandCommentService(_context);

            var data = new TestDataInitializer(_context);
            data.SeedData();
        }

        [TestMethod]
        public void GetErrandComments_ShouldReturnAllErradnComments()
        {
            //ARRANGE
            var allErrandComments = _context.ErrandComments.Count();

            //ACT
            var errandComments = _sut.GetComments().Count();

            //ASSERT
            Assert.AreEqual(allErrandComments, errandComments);
        }

        [TestMethod]
        public void GetErrandComments_ShouldReturnListOfErrandCommentModel()
        {
            //ARRANGE

            //ACT
            var errandComments = _sut.GetComments();

            //ASSERT
            Assert.IsInstanceOfType(errandComments, typeof(List<ErrandCommentModel>));
        }

        [TestMethod]
        public void GetErrandCommentsFromErrandId_ShouldReturnListOfErrandCommentModel()
        {
            //ARRANGE

            //ACT
            var errandComments = _sut.GetErrandCommentsFromErrandId("9f091fd6-9657-4db3-a41c-7bb9e24a43fd");

            //ASSERT
            Assert.IsInstanceOfType(errandComments, typeof(List<ErrandCommentModel>));
        }

        [TestMethod]
        public void GetCommentsById_ShouldReturnErrandCommentModel()
        {
            //ARRANGE
            var errandCommentToCompare = _context.ErrandComments.FirstOrDefault(e => e.Id.ToString() == "136b0112-246f-4891-a36f-0ff09738be34");
            var errandCommentIdToCompare = errandCommentToCompare.Id.ToString();

            //ACT
            var errandComment = _sut.GetCommentsById(errandCommentIdToCompare);

            //ASSERT
            Assert.IsInstanceOfType(errandComment, typeof(ErrandCommentModel));
        }

        [TestMethod]
        public void GetCommentsById_ShouldReturnCorrectErrandComment()
        {
            //ARRANGE
            var errandCommentToCompare = _context.ErrandComments.FirstOrDefault(e => e.Id.ToString() == "136b0112-246f-4891-a36f-0ff09738be34");
            var errandCommentIdToCompare = errandCommentToCompare.Id.ToString();

            //ACT
            var errandComment = _sut.GetCommentsById(errandCommentIdToCompare);
            var myErrandCommentId = errandComment.Id.ToString();

            //ASSERT
            Assert.AreEqual(errandCommentIdToCompare, myErrandCommentId);
        }
    }
}
