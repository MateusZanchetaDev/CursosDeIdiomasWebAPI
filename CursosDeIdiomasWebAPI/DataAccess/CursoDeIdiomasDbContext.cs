using CursosDeIdiomasWebAPI.DataAccess.Map;
using CursosDeIdiomasWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CursosDeIdiomasWebAPI.DataAccess
{
    public class CursoDeIdiomasDbContext : DbContext
    {
        public CursoDeIdiomasDbContext(DbContextOptions<CursoDeIdiomasDbContext> options) : base(options)
        {

        }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Turma> Turmas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AlunoMap());
            modelBuilder.ApplyConfiguration(new TurmaMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
