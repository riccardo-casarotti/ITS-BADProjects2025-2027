using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GAMMVC.Models;

public partial class GamContext : DbContext
{
    public GamContext()
    {
    }

    public GamContext(DbContextOptions<GamContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Autore> Autori { get; set; }

    public virtual DbSet<Materiale> Materiali{ get; set; }

    public virtual DbSet<Opera> Opere { get; set; }

    public virtual DbSet<VwElencoMateriali> VwElencoMateriali { get; set; }

   
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autore>(entity =>
        {
            entity.ToTable("Autore");
        });

        modelBuilder.Entity<Materiale>(entity =>
        {
            entity.ToTable("Materiale");
        });

        modelBuilder.Entity<Opera>(entity =>
        {
            entity.ToTable("Opera");

            entity.Property(e => e.AmbitoCulturale).HasColumnName("Ambito_culturale");
            entity.Property(e => e.Lsreferenceby).HasColumnName("lsreferenceby");
            entity.Property(e => e.TitoloSoggetto).HasColumnName("Titolo_soggetto");

            entity.HasOne(d => d.IdAutoreNavigation).WithMany(p => p.Opere)
                .HasForeignKey(d => d.IdAutore)
                .HasConstraintName("FK_Opera_Autore");

            entity.HasMany(d => d.Materiali).WithMany(p => p.Opere)
                .UsingEntity<Dictionary<string, object>>(
                    "OperaMateriale",
                    r => r.HasOne<Materiale>().WithMany()
                        .HasForeignKey("IdMateriale")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_OperaMateriale_Materiale"),
                    l => l.HasOne<Opera>().WithMany()
                        .HasForeignKey("IdOpera")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_OperaMateriale_Opera"),
                    j =>
                    {
                        j.HasKey("IdOpera", "IdMateriale");
                        j.ToTable("OperaMateriale");
                    });
        });

        modelBuilder.Entity<VwElencoMateriali>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_ElencoMateriali");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
