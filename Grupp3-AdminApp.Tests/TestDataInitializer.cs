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
            if (_context.Elevators.Count() != 0)
                return;

            _context.Elevators.Add(
            new ElevatorModel
            {
                Id = Guid.NewGuid(),
                ConnectionString = "totally real connectionString",
                Errands = new List<ErrandModel>
                {
                        new ErrandModel
                        {
                            Id = Guid.Parse("9f091fd6-9657-4db3-a41c-7bb9e24a43fd"),
                            Title = "Title",
                            Description = "Description",
                            Status = ErrandStatus.Done,
                            LastEdited = DateTime.Now,
                            CreatedAt = DateTime.Now,
                            CreatedBy = "Philip",
                            Technician = new TechnicianModel(Guid.Parse("62e4a265-ceb7-4254-81f9-7d4a78cfbed8"), "Namn")
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
                            Id = Guid.Parse("b9701bcb-e8af-4824-a23f-add3c4a0d60b"),
                            Title = "Title",
                            Description = "Description",
                            Status = ErrandStatus.Done,
                            LastEdited = DateTime.Now,
                            CreatedAt = DateTime.Now,
                            CreatedBy = "Philip",
                            Technician = new TechnicianModel(Guid.Parse("dacf220b-2c9d-4d1a-a867-92a667de2a11"), "Namn")

                        }
                    }
                });
            _context.SaveChanges();
        }
    }
}
