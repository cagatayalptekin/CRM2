using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Domain;

public partial class CompanyContext : DbContext
{
    public CompanyContext()
    {
    }

    public CompanyContext(DbContextOptions<CompanyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Departman> Departmen { get; set; }

    public virtual DbSet<Personel> Personels { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Company;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Departman>(entity =>
        {
            entity.ToTable("Departman");

            entity.Property(e => e.DepartmanName).HasMaxLength(50);
        });

        modelBuilder.Entity<Personel>(entity =>
        {
            entity.ToTable("Personel");

            entity.Property(e => e.Ad).HasMaxLength(50);
            entity.Property(e => e.DogumTarihi).HasColumnType("datetime");
            entity.Property(e => e.Soyad).HasMaxLength(50);

            entity.HasOne(d => d.Departman).WithMany(p => p.Personels)
                .HasForeignKey(d => d.DepartmanId)
                .HasConstraintName("FK_Personel_Departman").OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Role).WithMany(p => p.Personels)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_Personel_Role").OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.ToTable("Task");

            entity.Property(e => e.Deadline).HasColumnType("date");

            entity.HasOne(d => d.Personel).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.PersonelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Task_Personel").OnDelete(DeleteBehavior.Cascade);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
