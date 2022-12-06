using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grupp3_AdminApp.Models;
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
                Id = Guid.Parse("5435f3c3-56f7-49da-8ef4-24937f71fd70"),
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
                            Technician = new TechnicianModel(Guid.Parse("62e4a265-ceb7-4254-81f9-7d4a78cfbed8"), "Namn"),
                            Comments = new List<ErrandCommentModel>
                            {
                                new ErrandCommentModel
                                {
                                    Id = Guid.Parse("b156ed09-0d9b-47c9-a87c-61834ca14e88"),
                                    Author = Guid.Parse("62e4a265-ceb7-4254-81f9-7d4a78cfbed8"),
                                    Content = "This is content of Comment 1",
                                    PostedAt = DateTime.Now.AddMinutes(-4)
                                },
                                new ErrandCommentModel
                                {
                                    Id = Guid.Parse("e2d4cefd-4056-4ec1-9111-0e02ef0ed0e1"),
                                    Author = Guid.Parse("dacf220b-2c9d-4d1a-a867-92a667de2a11"),
                                    Content = "This is content of Comment 2",
                                    PostedAt = DateTime.Now.AddMinutes(-10)
                                }
                            }
                        }
                }
            });
            _context.Elevators.Add(
                new ElevatorModel
                {
                    Id = Guid.Parse("8fd4e759-daf2-4fa7-b4a7-58ee9657160d"),
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
                            Technician = new TechnicianModel(Guid.Parse("dacf220b-2c9d-4d1a-a867-92a667de2a11"), "Namn"),
                            Comments = new List<ErrandCommentModel>
                            {
                                new ErrandCommentModel
                                {
                                    Id = Guid.Parse("136b0112-246f-4891-a36f-0ff09738be34"),
                                    Author = Guid.Parse("62e4a265-ceb7-4254-81f9-7d4a78cfbed8"),
                                    Content = "This is content of Comment 1",
                                    PostedAt = DateTime.Now.AddMinutes(-6)
                                },
                                new ErrandCommentModel
                                {
                                    Id = Guid.Parse("c185362e-1caf-482a-96a9-e981ada42944"),
                                    Author = Guid.Parse("dacf220b-2c9d-4d1a-a867-92a667de2a11"),
                                    Content = "This is content of Comment 2",
                                    PostedAt = DateTime.Now.AddMinutes(-15)
                                }
                            }
                        }
                    }
                });
            _context.SaveChanges();
        }
    }
}
