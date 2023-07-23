using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Hectre;

public partial class HectreContext : DbContext
{
    public HectreContext()
    {
    }

    public HectreContext(DbContextOptions<HectreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Harvest> Harvests { get; set; }

    public virtual DbSet<Orchard> Orchards { get; set; }

    public virtual DbSet<Timesheet> Timesheets { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
         => optionsBuilder.UseSqlServer("Server=DESKTOP-P17TH39\\SQLEXPRESS;Database=Hectre;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Harvest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Harvests__3214EC0750D6BBE9");

            entity.Property(e => e.HourlyWageRate).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PickingDate).HasColumnType("datetime");
            entity.Property(e => e.Type).HasMaxLength(50);
            entity.Property(e => e.Variety).HasMaxLength(100);

            entity.HasOne(d => d.Orchard).WithMany(p => p.Harvests)
                .HasForeignKey(d => d.OrchardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Harvest_Orchard");
        });

        modelBuilder.Entity<Orchard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orchards__3214EC07A6093ED3");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.SubBlock).HasMaxLength(50);
        });

        modelBuilder.Entity<Timesheet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Timeshee__3214EC0767A2990A");

            entity.Property(e => e.Activity).HasMaxLength(50);
            entity.Property(e => e.EndTime).HasMaxLength(50);
            entity.Property(e => e.StartTime).HasMaxLength(50);

            entity.HasOne(d => d.Orchard).WithMany(p => p.Timesheets)
                .HasForeignKey(d => d.OrchardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Timesheet_Orchard");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.JwtToken)
                .HasMaxLength(255)
                .HasColumnName("jwt_token");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(50)
                .HasColumnName("user_email");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasColumnName("user_name");
            entity.Property(e => e.UserPassword)
                .HasMaxLength(255)
                .HasColumnName("user_password");
            entity.Property(e => e.UserType)
                .HasMaxLength(10)
                .HasColumnName("user_type");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
