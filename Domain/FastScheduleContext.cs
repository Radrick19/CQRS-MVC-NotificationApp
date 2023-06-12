using FastSchedule.Domain.Models;
using FastSchedule.Domain.Models.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FastSchedule.Domain
{
    public class FastScheduleContext : DbContext
    {
        public DbSet<EverydayTask> EverydayTasks { get; set; }
        public DbSet<OnetimeTask> OnetimeTasks { get; set; }
        public DbSet<WeeklyTask> WeeklyTasks { get; set; }
        public DbSet<MonthlyTask> MonthlyTasks { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<OnetimeTask>()
                .HasIndex(task => task.Guid)
                .IsUnique();
            modelBuilder.Entity<OnetimeTask>()
                .Property(task => task.EventDay)
                .HasConversion(date => date.ToShortDateString(), date => DateOnly.FromDateTime(Convert.ToDateTime(date)));

            modelBuilder.Entity<MonthlyTask>()
                .Property(task => task.EventTime)
                .HasConversion(time => time.ToString(), time => TimeOnly.FromDateTime(Convert.ToDateTime(time)));

            modelBuilder.Entity<WeeklyTask>()
                .Property(task => task.EventTime)
                .HasConversion(time => time.ToString(), time => TimeOnly.FromDateTime(Convert.ToDateTime(time)));

            modelBuilder.Entity<OnetimeTask>()
                .Property(task => task.EventTime)
                .HasConversion(time => time.ToString(), time => TimeOnly.FromDateTime(Convert.ToDateTime(time)));

            modelBuilder.Entity<EverydayTask>()
                .Property(task => task.EventTime)
                .HasConversion(time => time.ToString(), time => TimeOnly.FromDateTime(Convert.ToDateTime(time)));

            modelBuilder.Entity<OnetimeTask>()
                .Property(task => task.PreNotifyTime)
                .HasConversion(new TimeSpanToTicksConverter());

            modelBuilder.Entity<WeeklyTask>()
                .Property(task => task.PreNotifyTime)
                .HasConversion(new TimeSpanToTicksConverter());

            modelBuilder.Entity<MonthlyTask>()
                .Property(task => task.PreNotifyTime)
                .HasConversion(new TimeSpanToTicksConverter());

            modelBuilder
                .Entity<User>()
                .HasIndex(user => user.Guid)
                .IsUnique();
        }

        public FastScheduleContext(DbContextOptions<FastScheduleContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        public FastScheduleContext()
        {
            Database.Migrate();
        }
    }
}
