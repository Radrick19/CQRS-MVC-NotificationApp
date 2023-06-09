using FastSchedule.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FastSchedule.Domain
{
    public class FastScheduleContext : DbContext
    {
        public DbSet<DailyTask> DailyTasks { get; set; }
        public DbSet<WeeklyTask> WeeklyTasks { get; set; }
        public DbSet<MonthlyTask> MonthlyTasks { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<DailyTask>()
                .HasIndex(task => task.Guid)
                .IsUnique();
            modelBuilder.Entity<DailyTask>()
                .Property(task => task.EventDay)
                .HasConversion(date => date.ToShortDateString(), date => DateOnly.FromDateTime(Convert.ToDateTime(date)));

            modelBuilder.Entity<MonthlyTask>()
                .Property(task => task.EventTime)
                .HasConversion(time => time.ToString(), time => TimeOnly.FromDateTime(Convert.ToDateTime(time)));

            modelBuilder.Entity<WeeklyTask>()
                .Property(task => task.EventTime)
                .HasConversion(time => time.ToString(), time => TimeOnly.FromDateTime(Convert.ToDateTime(time)));

            modelBuilder.Entity<DailyTask>()
                .Property(task => task.EventTime)
                .HasConversion(time => time.ToString(), time => TimeOnly.FromDateTime(Convert.ToDateTime(time)));

            modelBuilder.Entity<DailyTask>()
                .Property(task => task.PreNotifyTime)
                .HasConversion(new TimeSpanToTicksConverter());

            modelBuilder.Entity<WeeklyTask>()
                .Property(task => task.PreNotifyTime)
                .HasConversion(new TimeSpanToTicksConverter());

            modelBuilder.Entity<MonthlyTask>()
                .Property(task => task.PreNotifyTime)
                .HasConversion(new TimeSpanToTicksConverter());

            modelBuilder.Entity<MonthlyTask>()
                .Property(task => task.EventDaysOfMonth)
                .HasConversion(days => JsonConvert.SerializeObject(days), days => JsonConvert.DeserializeObject<IEnumerable<int>>(days));

            modelBuilder.Entity<WeeklyTask>()
                .Property(task => task.EventDaysOfWeek)
                .HasConversion(days => JsonConvert.SerializeObject(days), days => JsonConvert.DeserializeObject<IEnumerable<DayOfWeek>>(days));

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
