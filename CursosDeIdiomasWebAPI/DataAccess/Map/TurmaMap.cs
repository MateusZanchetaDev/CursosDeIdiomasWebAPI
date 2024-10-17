using CursosDeIdiomasWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CursosDeIdiomasWebAPI.DataAccess.Map
{
    public class TurmaMap : IEntityTypeConfiguration<Turma>
    {
        public void Configure(EntityTypeBuilder<Turma> builder)
        {
            builder.HasKey(x => x.Codigo);
            builder.Property(x => x.Nivel).IsRequired().HasMaxLength(100);
            builder.Property(x => x.AlunoCPF).IsRequired(false);

            builder.HasOne(x => x.Aluno);
        }
    }
}
