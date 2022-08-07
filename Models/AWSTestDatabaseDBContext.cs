using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace testapicore31.Models
{
    public class AWSTestDatabaseDBContext : DbContext
    {
        public DbSet<UserStatus> UserStatuses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Pacient> Pacients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        public AWSTestDatabaseDBContext(DbContextOptions<AWSTestDatabaseDBContext> options) : base(options)
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

    public class UserStatus
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public UserStatus UserStatus { get; set; }
    }

    public class Pacient
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string NationalId { get; set; }
    }

    public class Appointment
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Pacient Pacient { get; set; }
        [Column(TypeName = "timestamp")]
        public DateTime When { get; set; }
        public bool Confirmed { get; set; }
    }
}
