﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkerCompany.DAL.Helpers;

namespace WorkerCompany.DAL.Models
{
    public partial class WorkerCompanyPetContext : IdentityDbContext<AppUser, IdentityRole, string>
    {
        public WorkerCompanyPetContext()
        {
        }

        public WorkerCompanyPetContext(DbContextOptions<WorkerCompanyPetContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Worker> Worker { get; set; }
        public virtual DbSet<WorkerCopy> WorkerCopy { get; set; }
        public virtual DbSet<AppUserProfile> AppUserProfile { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionStrings.Default);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.CompanyName)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.Description)
                      .IsRequired()
                      .HasMaxLength(500);
            });

            modelBuilder.Entity<Worker>(entity =>
            {
                //entity.Property(e => e.Dob)
                //      .HasColumnName("DOB")
                //      .HasColumnType("datetime");

                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(75);

                entity.Property(e => e.TimeUpdated)
                      .HasColumnType("datetime")
                      .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Company)
                      .WithMany(p => p.Workers)
                      .HasForeignKey(d => d.CompanyId)
                      .HasConstraintName("FK__Worker__CompanyI__276EDEB3")
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(w => w.AppUser)
                      .WithOne(au => au.Worker)
                      .HasForeignKey<AppUser>(au => au.WorkerId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.HasOne(au => au.Worker)
                      .WithOne(w => w.AppUser)
                      .HasForeignKey<Worker>(w => w.AppUserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(au => au.AppUserProfile)
                      .WithOne(aup => aup.AppUser)
                      .HasForeignKey<AppUserProfile>(aup => aup.AppUserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<AppUserProfile>(entity =>
            {
                entity.ToTable("AppUserProfile");

                entity.HasOne(aup => aup.AppUser)
                      .WithOne(au => au.AppUserProfile)
                      .HasForeignKey<AppUser>(au => au.AppUserProfileId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<WorkerCopy>(entity =>
            {
                entity.ToTable("WorkerCopy");
                entity.Property(e => e.Dob)
                      .HasColumnName("DOB")
                      .HasColumnType("datetime");

                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(75);

                entity.Property(e => e.TimeUpdated)
                      .HasColumnType("datetime")
                      .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Company)
                      .WithMany(p => p.WorkerCopies)
                      .HasForeignKey(d => d.CompanyId)
                      .HasConstraintName("FK__WorkerCop__Compa__5DCAEF64")
                      .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelBuilder);
            //OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
