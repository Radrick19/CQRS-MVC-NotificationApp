﻿using FastSchedule.Domain.Infrastucture.Enums;
using FastSchedule.Domain.Models;
using FastSchedule.Domain.Models.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace FastSchedule.Domain
{
    public class FastScheduleContext : DbContext
    {
        public DbSet<ScheduleTask> Tasks { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<ScheduleTask>()
                .HasIndex(task => task.Guid)
                .IsUnique();

            modelBuilder
                .Entity<ScheduleTask>()
                .HasIndex(task => task.UserId);

            modelBuilder.Entity<ScheduleTask>()
                .Property(task => task.EventDate)
                .HasConversion(date => date.ToShortDateString(), date => DateOnly.FromDateTime(Convert.ToDateTime(date)));

            modelBuilder.Entity<ScheduleTask>()
                .Property(task => task.EventTime)
                .HasConversion(time => time.ToString(), time => TimeOnly.FromDateTime(Convert.ToDateTime(time)));

            modelBuilder.Entity<ScheduleTask>()
                .Property(task => task.PreNotifyTime)
                .HasConversion(new TimeSpanToTicksConverter());

            modelBuilder.Entity<ScheduleTask>()
                .Property(task => task.TaskType)
                .HasConversion(new EnumToStringConverter<TaskType>());

            modelBuilder.Entity<ScheduleTask>()
                .Property(task => task.RemindType)
                .HasConversion(new EnumToStringConverter<RemindType>());

            modelBuilder.Entity<ScheduleTask>()
                .Property(task => task.CompletedDays)
                .HasConversion(cd=> JsonSerializer.Serialize(cd, new JsonSerializerOptions()), cd=> JsonSerializer.Deserialize<IEnumerable<DateOnly>>(cd, new JsonSerializerOptions()));

            modelBuilder.Entity<ScheduleTask>()
                .Property(task => task.DeletedDays)
                .HasConversion(dd => JsonSerializer.Serialize(dd, new JsonSerializerOptions()), dd => JsonSerializer.Deserialize<IEnumerable<DateOnly>>(dd, new JsonSerializerOptions()));

            modelBuilder
                .Entity<User>()
                .HasIndex(user => user.Guid)
                .IsUnique();
        }

        public FastScheduleContext(DbContextOptions<FastScheduleContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public FastScheduleContext()
        {
            Database.EnsureCreated();
        }
    }
}
