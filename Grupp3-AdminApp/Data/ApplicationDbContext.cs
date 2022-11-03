using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Grupp3_Elevator.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ElevatorModel> Elevators { get; set; }
        public DbSet<ErrandModel> Errands { get; set; }
        public DbSet<ErrandCommentModel> ErrandComments { get; set; }
        public DbSet<TechnicianModel> Technicians { get; set; }

    }
}