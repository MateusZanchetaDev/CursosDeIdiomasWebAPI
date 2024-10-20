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
            builder.HasMany(a => a.listTurmas).WithMany(t => t.listAlunos).UsingEntity(j => j.ToTable("AlunoTurma"));
        }
    }
}
