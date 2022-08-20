using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace net6webapi.Models
{
    public class AppDBContext : DbContext
    {
        public DbSet<UserStatus> UserStatuses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Pacient> Pacients { get; set; }
        public DbSet<MedicalPlan> MedicalPlans { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentStatus> AppointmentStatuses { get; set; }

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        //daniel: different connection string!
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    => optionsBuilder.UseNpgsql("Host=my_host;Database=my_db;Username=my_user;Password=my_pw");

        //daniel: override table names!
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //modelBuilder.Entity<UserStatus>().ToTable("UserStatuses");
        //modelBuilder.Entity<User>().ToTable("Users");
        //}
    }

    public class IdDescriptionEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }

    public class UserStatus : IdDescriptionEntity
    {
        //
    }

    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public UserStatus UserStatus { get; set; }
    }

    public class MedicalPlan : IdDescriptionEntity
    {
        //
    }

    public class AppointmentStatus : IdDescriptionEntity
    {
        //
    }

    public class Pacient
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string NationalId { get; set; }
        public MedicalPlan MedicalPlan { get; set; }
    }

    public class Appointment
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Pacient Pacient { get; set; }
        [Column(TypeName = "timestamp")]
        public DateTime When { get; set; }
        public AppointmentStatus AppointmentStatus { get; set; }
    }
}
