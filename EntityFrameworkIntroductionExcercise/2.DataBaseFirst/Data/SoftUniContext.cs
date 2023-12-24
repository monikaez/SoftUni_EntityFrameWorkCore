using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SoftUni.Models;

namespace SoftUni.Data

{
    public partial class SoftUniContext : DbContext
    {
        public SoftUniContext()
        {
        }

        public SoftUniContext(DbContextOptions<SoftUniContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Project> Projects { get; set; } = null!;
        public virtual DbSet<Town> Towns { get; set; } = null!;
//add new maping table
        public virtual DbSet<EmployeeProject> EmployeesProjects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database =SoftUni;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasOne(d => d.Town)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.TownId)
                    .HasConstraintName("FK_Addresses_Towns");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.ManagerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Departments_Employees");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK_Employees_Addresses");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employees_Departments");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.InverseManager)
                    .HasForeignKey(d => d.ManagerId)
                    .HasConstraintName("FK_Employees_Employees");

               
            });

            modelBuilder.Entity<EmployeeProject>(entity =>
            {
                entity.HasKey(pk => new { pk.EmployeeId, pk.ProjectId });

                entity
                    .HasOne(e => e.Employee)
                    .WithMany(e => e.EmployeesProjects)
                    .HasForeignKey(ep => ep.EmployeeId);

                entity
                    .HasOne(e => e.Project)
                    .WithMany(p => p.EmployeesProjects)
                    .HasForeignKey(ep => ep.ProjectId);

            });
            //modelBuilder.Entity<EmployeeProject>(entity =>
            //{
            //    entity.HasKey(pk => new
            //    {//PK V MAPING TABLE
            //        pk.EmployeeId,
            //        pk.ProjectId
            //    });
            //    //change Employee.cs
            //    entity.HasOne(ep => ep.Employee)
            //    .WithMany(e => e.EmployeesProjects)
            //    .HasForeignKey(ep => ep.EmployeeId);
            //    //change Project.cs
            //    entity.HasOne(ep => ep.Project)
            //    .WithMany(p => p.EmployeesProjects)
            //    .HasForeignKey(ep => ep.ProjectId);

            //});

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
