using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Istat2.Models;

public partial class IstatContext : DbContext
{
    public IstatContext()
    {
    }

    public IstatContext(DbContextOptions<IstatContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comune> Comuni { get; set; }

    public virtual DbSet<Provincium> Province { get; set; }

    public virtual DbSet<Regione> Regioni { get; set; }

    public virtual DbSet<RipartizioneGeografica> RipartizioneGeografica { get; set; }

    public virtual DbSet<VwComuniInPianura> VwComuniInPianura { get; set; }

    public virtual DbSet<ZonaAltimetrica> ZonaAltimetrica { get; set; }

    public virtual DbSet<ZonaMontana> ZonaMontana { get; set; }

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
        modelBuilder.Entity<Comune>(entity =>
        {
            modelBuilder.Entity<Provincium>().ToTable("Provincia");
            entity.HasKey(e => e.Id).HasName("PK__Comune__3214EC07918D1B4C");

            entity.ToTable("Comune");

            entity.Property(e => e.CodiceCatastale)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Denominazione)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IdZonaMontana)
                .HasMaxLength(2)
                .IsUnicode(false);

            entity.HasOne(d => d.IdProvinciaNavigation).WithMany(p => p.Comuni)
                .HasForeignKey(d => d.IdProvincia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comune_Provincia");

            entity.HasOne(d => d.IdZonaAltimetricaNavigation).WithMany(p => p.Comuni)
                .HasForeignKey(d => d.IdZonaAltimetrica)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comune_ZonaAltimetrica");

            entity.HasOne(d => d.IdZonaMontanaNavigation).WithMany(p => p.Comuni)
                .HasForeignKey(d => d.IdZonaMontana)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comune_ZonaMontana");
        });

        modelBuilder.Entity<Provincium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Provinci__3214EC0762EBB50F");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Denominazione)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Sigla)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.IdRegioneNavigation).WithMany(p => p.Province)
                .HasForeignKey(d => d.IdRegione)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Provincia_Regione");
        });

        modelBuilder.Entity<Regione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Regione__3214EC0790E9E202");

            entity.ToTable("Regione");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Denominazione)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdRipartizioneNavigation).WithMany(p => p.Regioni)
                .HasForeignKey(d => d.IdRipartizione)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Regione_RipartizioneGeografica");
        });

        modelBuilder.Entity<RipartizioneGeografica>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ripartiz__3214EC07FEC22120");

            entity.ToTable("RipartizioneGeografica");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Denominazione)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwComuniInPianura>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_ComuniInPianura");

            entity.Property(e => e.Comune)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Provincia)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Regione)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SuperficieDelComune).HasColumnName("Superficie del comune");
        });

        modelBuilder.Entity<ZonaAltimetrica>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ZonaAlti__3214EC07358F1DA3");

            entity.ToTable("ZonaAltimetrica");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Denominazione)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ZonaMontana>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ZonaMont__3214EC07A6028124");

            entity.ToTable("ZonaMontana");

            entity.Property(e => e.Id)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.Denominazione)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
