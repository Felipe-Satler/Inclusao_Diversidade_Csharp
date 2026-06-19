<<<<<<< HEAD
<<<<<<< HEAD
=======
﻿using System;
using System.Collections.Generic;
>>>>>>> 9afd1eb92a612d68c42265b614376f950340cdde
=======
﻿using System;
using System.Collections.Generic;
>>>>>>> 9afd1eb92a612d68c42265b614376f950340cdde
using Microsoft.EntityFrameworkCore;
using InclusaoDiversidade.Domain;

namespace InclusaoDiversidade.Infrastructure.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbCandidato> TbCandidatos { get; set; }

    public virtual DbSet<TbColaboradore> TbColaboradores { get; set; }

    public virtual DbSet<TbDepartamento> TbDepartamentos { get; set; }

    public virtual DbSet<TbTreinamentosLog> TbTreinamentosLogs { get; set; }

    public virtual DbSet<TbVaga> TbVagas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("RM561859")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<TbCandidato>(entity =>
        {
            entity.HasKey(e => e.IdCandidato).HasName("PK_CANDIDATO");

            entity.ToTable("TB_CANDIDATOS");

            entity.Property(e => e.IdCandidato)
                .HasPrecision(10)
                .ValueGeneratedNever()
                .HasColumnName("ID_CANDIDATO");
            entity.Property(e => e.FkVaga)
                .HasPrecision(10)
                .ValueGeneratedOnAdd()
                .HasColumnName("FK_VAGA");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NOME");
            entity.Property(e => e.ScoreDiversidade)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(3,1)")
                .HasColumnName("SCORE_DIVERSIDADE");

            entity.HasOne(d => d.FkVagaNavigation).WithMany(p => p.TbCandidatos)
                .HasForeignKey(d => d.FkVaga)
                .HasConstraintName("FK_CAND_VAGA");
        });

        modelBuilder.Entity<TbColaboradore>(entity =>
        {
            entity.HasKey(e => e.IdColab).HasName("PK_COLABORADOR");

            entity.ToTable("TB_COLABORADORES");

            entity.Property(e => e.IdColab)
                .HasPrecision(10)
                .HasColumnName("ID_COLAB");
            entity.Property(e => e.Etnia)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ETNIA");
            entity.Property(e => e.FkDep)
                .HasPrecision(5)
                .HasColumnName("FK_DEP");
            entity.Property(e => e.Genero)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("GENERO");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false)
                .ValueGeneratedOnAdd()
                .HasColumnName("NOME");
            entity.Property(e => e.Pcd)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("'N'")
                .IsFixedLength()
                .HasColumnName("PCD");

            entity.HasOne(d => d.FkDepNavigation).WithMany(p => p.TbColaboradores)
                .HasForeignKey(d => d.FkDep)
                .HasConstraintName("FK_COLAB_DEP");
        });

        modelBuilder.Entity<TbDepartamento>(entity =>
        {
            entity.HasKey(e => e.IdDep).HasName("PK_DEPARTAMENTO");

            entity.ToTable("TB_DEPARTAMENTOS");

            entity.Property(e => e.IdDep)
                .HasPrecision(5)
                .ValueGeneratedNever()
                .HasColumnName("ID_DEP");
            entity.Property(e => e.MetaDiversidade)
                .HasColumnType("NUMBER(3,2)")
                .HasColumnName("META_DIVERSIDADE");
            entity.Property(e => e.NomeDep)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOME_DEP");
        });

        modelBuilder.Entity<TbTreinamentosLog>(entity =>
        {
            entity.HasKey(e => e.IdLog).HasName("PK_LOG");

            entity.ToTable("TB_TREINAMENTOS_LOG");

            entity.Property(e => e.IdLog)
                .HasPrecision(15)
                .ValueGeneratedNever()
                .HasColumnName("ID_LOG");
            entity.Property(e => e.DataConclusao)
                .HasDefaultValueSql("SYSDATE")
                .HasColumnType("DATE")
                .HasColumnName("DATA_CONCLUSAO");
            entity.Property(e => e.FkColab)
                .HasPrecision(10)
                .HasColumnName("FK_COLAB");
            entity.Property(e => e.TipoTreinamento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TIPO_TREINAMENTO");

            entity.HasOne(d => d.FkColabNavigation).WithMany(p => p.TbTreinamentosLogs)
                .HasForeignKey(d => d.FkColab)
                .HasConstraintName("FK_LOG_COLAB");
        });

        modelBuilder.Entity<TbVaga>(entity =>
        {
            entity.HasKey(e => e.IdVaga).HasName("PK_VAGA");

            entity.ToTable("TB_VAGAS");

            entity.Property(e => e.IdVaga)
                .HasPrecision(10)
                .HasColumnName("ID_VAGA");
            entity.Property(e => e.Cargo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CARGO");
            entity.Property(e => e.FkDep)
                .HasPrecision(5)
                .ValueGeneratedOnAdd()
                .HasColumnName("FK_DEP");
            entity.Property(e => e.FlagAfirmativa)
                .HasMaxLength(1)
                .IsUnicode(false)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("'N'")
                .IsFixedLength()
                .HasColumnName("FLAG_AFIRMATIVA");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("'ABERTA'")
                .HasColumnName("STATUS");

            entity.HasOne(d => d.FkDepNavigation).WithMany(p => p.TbVagas)
                .HasForeignKey(d => d.FkDep)
                .HasConstraintName("FK_VAGA_DEP");
        });
        modelBuilder.HasSequence("SEQ_COLAB");
        modelBuilder.HasSequence("SEQ_TREINAMENTO_LOG");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
<<<<<<< HEAD
<<<<<<< HEAD
}
=======
}
>>>>>>> 9afd1eb92a612d68c42265b614376f950340cdde
=======
}
>>>>>>> 9afd1eb92a612d68c42265b614376f950340cdde
