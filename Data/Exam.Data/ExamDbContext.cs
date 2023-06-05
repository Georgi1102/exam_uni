using Exam.DataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace Exam.Data
{
    public class ExamDbContext : IdentityDbContext<ExamUser, IdentityRole<string>, string>
    {
        public DbSet<Car> Cars { get; set; }

        public DbSet<RentCar> RentCars { get; set; }

        public DbSet<Status> Statuses { get; set; }

        public ExamDbContext()
        {

        }

        public ExamDbContext(DbContextOptions<ExamDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=ExamCarsDatabase-GeorgyKonst;Trusted_Connection=True");

            base.OnConfiguring(optionsBuilder);
        }
    
    }
}
