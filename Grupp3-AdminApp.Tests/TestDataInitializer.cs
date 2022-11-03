using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grupp3_Elevator.Data;
using Grupp3_Elevator.Models;

namespace AdminAppTests
{
    public class TestDataInitializer
    {
        private readonly ApplicationDbContext _context;

        public TestDataInitializer(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SeedData()
        {
            _context.Elevators.Add(
                new ElevatorModel
                {
                    Id = Guid.NewGuid(),
                    ConnectionString = "totally real connectionString",
                    Errands = new List<ErrandModel>
                    {
                        new ErrandModel
                        {
                            Id = Guid.NewGuid(),
                            Title = "Title",
                            Description = "Description",
                            Status = ErrandStatus.Done,
                            LastEdited = DateTime.Now,
                            CreatedAt = DateTime.Now,
                            CreatedBy = "Philip",
                            Technician = new TechnicianModel("Namn")
                        }
                    }
                });
            _context.Elevators.Add(
                new ElevatorModel
                {
                    Id = Guid.NewGuid(),
                    ConnectionString = "totally real connectionString",
                    Errands = new List<ErrandModel>
                    {
                        new ErrandModel
                        {
                            Id = Guid.NewGuid(),
                            Title = "Title",
                            Description = "Description",
                            Status = ErrandStatus.Done,
                            LastEdited = DateTime.Now,
                            CreatedAt = DateTime.Now,
                            CreatedBy = "Philip",
                            Technician = new TechnicianModel("Namn")

                        }
                    }
                });
            _context.SaveChanges();
        }
    }
}
