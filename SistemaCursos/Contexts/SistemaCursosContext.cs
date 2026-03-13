using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SistemaCursos.Domains;

namespace SistemaCursos.Contexts;

public partial class SistemaCursosContext : DbContext
{
    public SistemaCursosContext()
    {
    }

    public SistemaCursosContext(DbContextOptions<SistemaCursosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aluno> Aluno { get; set; }

    public virtual DbSet<Curso> Curso { get; set; }

    public virtual DbSet<Instrutor> Instrutor { get; set; }

    public virtual DbSet<Matricula> Matricula { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=D05S31-1313868\\SQLEXPRESS;Database=SistemaCursos;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aluno>(entity =>
        {
            entity.HasKey(e => e.AlunoID).HasName("PK__Aluno__C1967C6F68A23D4A");

            entity.Property(e => e.EmailAluno)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.NomeAluno)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Senha).HasMaxLength(32);
        });

        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.CursoID).HasName("PK__Curso__7E023A373C5746AB");

            entity.Property(e => e.CargaHoraria)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.NomeCurso)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.StatusCurso).HasDefaultValue(true);

            entity.HasOne(d => d.Instrutor).WithMany(p => p.Curso)
                .HasForeignKey(d => d.InstrutorID)
                .HasConstraintName("FK__Curso__Instrutor__5FB337D6");
        });

        modelBuilder.Entity<Instrutor>(entity =>
        {
            entity.HasKey(e => e.InstrutorID).HasName("PK__Instruto__096B84F4B942CD34");

            entity.Property(e => e.EmailInstrutor)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.Especializacao)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.NomeInstrutor)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Senha).HasMaxLength(32);
            entity.Property(e => e.StatusInstrutor).HasDefaultValue(true);
        });

        modelBuilder.Entity<Matricula>(entity =>
        {
            entity.HasKey(e => e.MatriculaID).HasName("PK__Matricul__908CEE22C65374A6");

            entity.HasOne(d => d.Aluno).WithMany(p => p.Matricula)
                .HasForeignKey(d => d.AlunoID)
                .HasConstraintName("FK__Matricula__Aluno__656C112C");

            entity.HasOne(d => d.Curso).WithMany(p => p.Matricula)
                .HasForeignKey(d => d.CursoID)
                .HasConstraintName("FK__Matricula__Curso__66603565");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
