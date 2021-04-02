using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrService.Models
{
    public class HrDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeDirector> EmployeeDirectors { get; set; }
        public DbSet<HrSpecialist> HrSpecialists { get; set; }
        public DbSet<Hrtask> Hrtasks { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<WorkPlan> WorkPlans { get; set; }
        public DbSet<Training> Training { get; set; }



        public HrDbContext(DbContextOptions<HrDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin"; // hr-директор
            string userManagerRoleName = "userManager"; // руководитель сотрудника
            string hrSpecialistRoleName = "hrSpecialist"; // HR-специалист
            string userRoleName = "user"; // сотрудник

            string adminEmail = "hradmin@gmail.com";
            string adminPassword = "pass535jkHka";

            // добавляем роли
            Role adminRole = new() { Id = 1, Name = adminRoleName };
            Role userRole = new() { Id = 2, Name = userRoleName };
            Role hrSpecialist = new() { Id = 3, Name = hrSpecialistRoleName };
            Role userManager = new() { Id = 4, Name = userManagerRoleName };
            User adminUser = new() { Id = 1, Email = adminEmail, Password = adminPassword, RoleId = adminRole.Id };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole, hrSpecialist, userManager });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.Satus).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdDirectorNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.IdDirector)
                    .HasConstraintName("FK_Employee_Director_Id");

                entity.HasOne(d => d.IdDivisionNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.IdDivision)
                    .HasConstraintName("FK_NewEmployee_Divisions_Id");

                entity.HasOne(d => d.IdPositionNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.IdPosition)
                    .HasConstraintName("FK_NewEmployee_Position_Id");
            });
            modelBuilder.Entity<EmployeeDirector>(entity =>
            {
                entity.ToTable("EmployeeDirector");

                entity.Property(e => e.BirthData).HasColumnType("date");

                entity.HasOne(d => d.IdPositionNavigation)
                    .WithMany(p => p.EmployeeDirectors)
                    .HasForeignKey(d => d.IdPosition)
                    .HasConstraintName("FK_Employee_Position_Id");

                entity.HasOne(d => d.IdDivisionNavigation)
                    .WithMany(p => p.EmployeeDirectors)
                    .HasForeignKey(d => d.IdDivision)
                    .HasConstraintName("FK_Director_Divisions_Id");
            });

            modelBuilder.Entity<HrSpecialist>(entity =>
            {
                entity.ToTable("HrSpecialist");

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.HasOne(d => d.IdDivisionNavigation)
                    .WithMany(p => p.HrSpecialists)
                    .HasForeignKey(d => d.IdDivision)
                    .HasConstraintName("FK_HrSpecialist_Divisions_Id");

                entity.HasOne(d => d.IdPositionNavigation)
                    .WithMany(p => p.HrSpecialists)
                    .HasForeignKey(d => d.IdPosition)
                    .HasConstraintName("FK_HrSpecialist_Position_Id");
            });

            modelBuilder.Entity<Hrtask>(entity =>
            {
                entity.ToTable("HRTasks");

                entity.Property(e => e.Deadline)
                    .HasColumnType("date")
                    .HasColumnName("Deadline ");

                entity.HasOne(d => d.IdHrSpecialistNavigation)
                    .WithMany(p => p.Hrtasks)
                    .HasForeignKey(d => d.IdHrSpecialist)
                    .HasConstraintName("FK_HRTasks_HrSpecialist_Id");
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.ToTable("Position");
            });

            modelBuilder.Entity<WorkPlan>(entity =>
            {
                entity.ToTable("WorkPlan");

                entity.Property(e => e.Deadline).HasColumnType("date");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.WorkPlans)
                    .HasForeignKey(d => d.IdEmployee)
                    .HasConstraintName("FK_WorkPlan_Employee_Id");
            });

            modelBuilder.Entity<Training>(entity =>
            {
                entity.ToTable("Training");

                entity.Property(e => e.Deadline).HasColumnType("date");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.Training)
                    .HasForeignKey(d => d.IdEmployee)
                    .HasConstraintName("FK_Training_Employee_Id");
            });
            

            base.OnModelCreating(modelBuilder);
        }

    }
}
