﻿// <auto-generated />
using CursosDeIdiomasWebAPI.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CursosDeIdiomasWebAPI.Migrations
{
    [DbContext(typeof(CursoDeIdiomasDbContext))]
    partial class CursoDeIdiomasDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CursosDeIdiomasWebAPI.Models.Aluno", b =>
                {
                    b.Property<string>("CPF")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CodigoTurma")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("CPF");

                    b.HasIndex("CodigoTurma");

                    b.ToTable("Alunos");
                });

            modelBuilder.Entity("CursosDeIdiomasWebAPI.Models.Turma", b =>
                {
                    b.Property<string>("Codigo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Nivel")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Codigo");

                    b.ToTable("Turmas");
                });

            modelBuilder.Entity("CursosDeIdiomasWebAPI.Models.Aluno", b =>
                {
                    b.HasOne("CursosDeIdiomasWebAPI.Models.Turma", "Turma")
                        .WithMany("listAlunos")
                        .HasForeignKey("CodigoTurma")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Turma");
                });

            modelBuilder.Entity("CursosDeIdiomasWebAPI.Models.Turma", b =>
                {
                    b.Navigation("listAlunos");
                });
#pragma warning restore 612, 618
        }
    }
}
