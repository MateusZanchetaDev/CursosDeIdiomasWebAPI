using CursosDeIdiomasWebAPI.DataAccess;
using CursosDeIdiomasWebAPI.Models;
using CursosDeIdiomasWebAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CursosDeIdiomasWebAPI.Repository
{
    public class TurmaRepository : ITurmaRepository
    {
        private readonly CursoDeIdiomasDbContext _dbContext;

        public TurmaRepository(CursoDeIdiomasDbContext cursoDeIdiomasDbContext)
        {
            _dbContext = cursoDeIdiomasDbContext;
        }

        public async Task<Turma> BuscarPorCodigo(string Codigo)
        {
            return await _dbContext.Turmas.Include(x => x.Aluno).FirstOrDefaultAsync(x => x.Codigo == Codigo);
        }

        public async Task<List<Turma>> BuscarTodasTurmas()
        {
          return await _dbContext.Turmas.Include(x => x.Aluno).ToListAsync();
        }

        public async Task<Turma> Adicionar(Turma turma)
        {
            await _dbContext.Turmas.AddAsync(turma);
            await _dbContext.SaveChangesAsync();

            return turma;
        }

        public async Task<Turma> Apagar(string Codigo)
        {
            Turma turmaPorCodigo = await BuscarPorCodigo(Codigo);

            if (turmaPorCodigo == null)
            {
                throw new Exception($"Turma para o Código: {Codigo} não foi encontrado no banco de dados.");
            }

            _dbContext.Turmas.Remove(turmaPorCodigo);
            await _dbContext.SaveChangesAsync();

            return turmaPorCodigo;
        }
    }
}
