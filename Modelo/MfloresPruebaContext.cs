using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Datos;

public partial class MfloresPruebaContext : DbContext
{
    public MfloresPruebaContext()
    {
    }

    public MfloresPruebaContext(DbContextOptions<MfloresPruebaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Charger> Chargers { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.; Database= MFlores_Prueba; Trusted_Connection=True; Trust Server Certificate=true; User ID=sa; Password=pass@word1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Charger>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Charger__3213E83F808C4E8C");

            entity.ToTable("Charger");

            entity.Property(e => e.Id)
                .HasMaxLength(24)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.CompanyId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("company_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("date")
                .HasColumnName("created_at");
            entity.Property(e => e.Status)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("date")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Company).WithMany(p => p.Chargers)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK__Charger__company__1CF15040");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PK__Company__3E267235F62F938C");

            entity.ToTable("Company");

            entity.Property(e => e.CompanyId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("company_id");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(130)
                .IsUnicode(false)
                .HasColumnName("company_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
