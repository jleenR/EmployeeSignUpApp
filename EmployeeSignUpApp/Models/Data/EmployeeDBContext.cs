using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EmployeeSignUpApp.Models.Data
{
    public partial class EmployeeDBContext : DbContext
    {
        public EmployeeDBContext()
        {
        }

        public EmployeeDBContext(DbContextOptions<EmployeeDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Credential> Credentials { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Gender> Genders { get; set; } = null!;
        public virtual DbSet<MaritalStatus> MaritalStatuses { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlite("Data Source=Data/EmployeeDB.db;Cache=Shared;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Credential>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.HasIndex(e => e.Password, "IX_Credentials_Password")
                    .IsUnique();

                entity.HasIndex(e => e.UserName, "IX_Credentials_User_Name")
                    .IsUnique();

                entity.Property(e => e.EmployeeId)
                    .ValueGeneratedNever()
                    .HasColumnName("Employee_Id");

                entity.Property(e => e.UserName).HasColumnName("User_Name");

                entity.HasOne(d => d.Employee)
                    .WithOne(p => p.Credential)
                    .HasForeignKey<Credential>(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.EmployeeId)
                    .ValueGeneratedNever()
                    .HasColumnName("Employee_Id");

                entity.Property(e => e.FirstName).HasColumnName("First_Name");

                entity.Property(e => e.GenderId).HasColumnName("Gender_Id");

                entity.Property(e => e.LastName).HasColumnName("Last_Name");

                entity.Property(e => e.MaritalStatusId).HasColumnName("Marital_Status_Id");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.GenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.MaritalStatus)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.MaritalStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.ToTable("Gender");

                entity.Property(e => e.GenderId)
                    .ValueGeneratedNever()
                    .HasColumnName("Gender_Id");

                entity.Property(e => e.GenderDesc).HasColumnName("Gender_Desc");
            });

            modelBuilder.Entity<MaritalStatus>(entity =>
            {
                entity.ToTable("Marital_Status");

                entity.Property(e => e.MaritalStatusId)
                    .ValueGeneratedNever()
                    .HasColumnName("Marital_Status_Id");

                entity.Property(e => e.MaritalStatusDesc).HasColumnName("Marital_Status_Desc");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
