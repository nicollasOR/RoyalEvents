using System;
using System.Collections.Generic;
using EventsRoyalOneSirR.Domains;
using Microsoft.EntityFrameworkCore;

namespace EventsRoyalOneSirR.Contexts;

public partial class EventsRoyalOneSirRContext : DbContext
{
    public EventsRoyalOneSirRContext()
    {
    }

    public EventsRoyalOneSirRContext(DbContextOptions<EventsRoyalOneSirRContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Evento> Evento { get; set; }

    public virtual DbSet<Inscrição> Inscrição { get; set; }

    public virtual DbSet<TipoUsuario> TipoUsuario { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-HULK0D89\\SQLEXPRESS;Database=EventsRoyalOneSir;Trusted_Connection=True;TrustServerCertificate=True");
    //"Server=(localdb)\MSSQLLocalDB;Database=EventsRoyalOneSirRR;Trusted_Connection=True;TrustServerCertificate=True"

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Evento>(entity =>
        {
            entity.HasKey(e => e.EventoId).HasName("PK__Evento__1EEB5921581160ED");

            entity.ToTable(tb => tb.HasTrigger("trg_desabilitarEvento"));

            entity.Property(e => e.DataEvento).HasPrecision(0);
            entity.Property(e => e.Localizacao).HasMaxLength(50);
            entity.Property(e => e.Nome).HasMaxLength(40);
            entity.Property(e => e.StatusEvento).HasDefaultValue(true);
        });

        modelBuilder.Entity<Inscrição>(entity =>
        {
            entity.HasKey(e => e.InscriçãoId).HasName("PK__Inscriçã__66D7E8BF72835A69");

            entity.Property(e => e.DataInscrição)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Evento).WithMany(p => p.Inscrição)
                .HasForeignKey(d => d.EventoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Inscricao_Evento");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Inscrição)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Inscricao_Usuario");
        });

        modelBuilder.Entity<TipoUsuario>(entity =>
        {
            entity.HasKey(e => e.TipoUsuarioId).HasName("PK__TipoUsua__7F22C722AC306104");

            entity.Property(e => e.Tipo_de_Usuario).HasMaxLength(40);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuario__2B3DE7B878522959");

            entity.ToTable(tb => tb.HasTrigger("trg_exclusaoUsuario"));

            entity.HasIndex(e => e.Email, "UQ__Usuario__A9D1053439FE0483").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Especialidade).HasMaxLength(70);
            entity.Property(e => e.Nome).HasMaxLength(70);
            entity.Property(e => e.Senha).HasMaxLength(32);
            entity.Property(e => e.StatusUsuario).HasDefaultValue(true);

            entity.HasOne(d => d.TipoUsuarioNavigation).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.TipoUsuario)
                .HasConstraintName("TipoUsuario_FK");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
