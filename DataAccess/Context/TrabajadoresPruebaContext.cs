using System;
using System.Collections.Generic;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context;

public partial class TrabajadoresPruebaContext : DbContext
{
    public TrabajadoresPruebaContext()
    {
    }

    public TrabajadoresPruebaContext(DbContextOptions<TrabajadoresPruebaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Distrito> Distritos { get; set; }

    public virtual DbSet<Provincia> Provincia { get; set; }

    public virtual DbSet<Trabajador> Trabajadores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Departam__3214EC07127C028F");

            entity.ToTable("Departamento");

            entity.Property(e => e.NombreDepartamento)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Distrito>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Distrito__3214EC074DF35B66");

            entity.ToTable("Distrito");

            entity.Property(e => e.NombreDistrito)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.IdProvinciaNavigation).WithMany(p => p.Distritos)
                .HasForeignKey(d => d.IdProvincia)
                .HasConstraintName("FK__Distrito__IdProv__2A4B4B5E");
        });

        modelBuilder.Entity<Provincia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Provinci__3214EC07DD6FCD1B");

            entity.Property(e => e.NombreProvincia)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Provincia)
                .HasForeignKey(d => d.IdDepartamento)
                .HasConstraintName("FK__Provincia__IdDep__2B3F6F97");
        });

        modelBuilder.Entity<Trabajador>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Trabajad__3214EC0778B3C769");

            entity.Property(e => e.Nombres)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.NumeroDocumento)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Sexo)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.TipoDocumento)
                .HasMaxLength(3)
                .IsUnicode(false);

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Trabajadores)
                .HasForeignKey(d => d.IdDepartamento)
                .HasConstraintName("FK__Trabajado__IdDep__2C3393D0");

            entity.HasOne(d => d.IdDistritoNavigation).WithMany(p => p.Trabajadores)
                .HasForeignKey(d => d.IdDistrito)
                .HasConstraintName("FK__Trabajado__IdDis__2D27B809");

            entity.HasOne(d => d.IdProvinciaNavigation).WithMany(p => p.Trabajadores)
                .HasForeignKey(d => d.IdProvincia)
                .HasConstraintName("FK__Trabajado__IdPro__2E1BDC42");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
