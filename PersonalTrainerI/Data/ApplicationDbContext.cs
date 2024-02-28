using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonalTrainerI.Models;

namespace PersonalTrainerI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<PersonalTrainerI.Models.PersonalTrainerViewModel> PersonalTrainers { get; set; }
        public DbSet<PersonalTrainerI.Models.TrainingViewModel> Training { get; set; }

        public DbSet<PersonalTrainerI.Models.GymViewModel> Gym { get; set; }

        public DbSet<PersonalTrainerI.Models.GymTrainerViewModel> GymTrainer { get; set; }

        public DbSet<PersonalTrainerI.Models.AvailabilityViewModel> Availability { get; set; }

       
    }

}