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
            builder.HasMany(t => t.listAlunos).WithOne(a => a.Turma).HasForeignKey(a => a.CodigoTurma).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
