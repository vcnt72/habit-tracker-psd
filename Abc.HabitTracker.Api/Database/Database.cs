using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Abc.HabitTracker.Api.Database
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        public DatabaseContext()
        {
        }
        public DbSet<UserDb> Users { get; set; }
        public DbSet<HabitDb> Habits { get; set; }
        public DbSet<BadgeDb> Badges { get; set; }

        public DbSet<LogDb> Logs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Database=habits_tracker;Username=postgres;Password=mateup123");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp").Entity<UserDb>().Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");
            modelBuilder.HasPostgresExtension("uuid-ossp").Entity<BadgeDb>().Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");
            modelBuilder.HasPostgresExtension("uuid-ossp").Entity<HabitDb>().Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");
            modelBuilder.HasPostgresExtension("uuid-ossp").Entity<LogDb>().Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");
            modelBuilder.Entity<HabitDb>().HasMany(e => e.Logs).WithOne(e => e.Habit).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UserDb>().HasMany(e => e.Habits).WithOne(e => e.User);

        }
    }

    public class UserDb
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<HabitDb> Habits { get; set; }
    }

    public class BadgeDb
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }

    public class HabitDb
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string[] DaysOff { get; set; }

        public virtual UserDb User { get; set; }

        public ICollection<LogDb> Logs { get; set; }

        [ForeignKey("UserDb")]
        public Guid UserId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public class LogDb
    {
        public Guid Id { get; set; }
        public virtual UserDb User { get; set; }
        public virtual HabitDb Habit { get; set; }

        public bool DaysOff { get; set; }

        public int Streak { get; set; }
        [ForeignKey("UserDb")]
        public Guid UserId { get; set; }

        [ForeignKey("HabitDb")]

        public Guid HabitId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

}