using CursosDeIdiomasWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CursosDeIdiomasWebAPI.DataAccess.Map
{
    public class AlunoMap : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.HasKey(x => x.CPF);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(150);
            builder.Property(x => x.CodigoTurma).IsRequired();
            builder.HasOne(a => a.Turma).WithMany(t => t.listAlunos).HasForeignKey(a => a.CodigoTurma).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
