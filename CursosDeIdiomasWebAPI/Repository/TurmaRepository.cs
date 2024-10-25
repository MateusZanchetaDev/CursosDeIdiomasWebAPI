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

        public async Task<Turma> BuscarPorCodigo(string codigo)
        {
            return await _dbContext.Turmas.Include(x => x.listAlunos).FirstOrDefaultAsync(x => x.Codigo == codigo);
        }

        public async Task<Turma> BuscarPorNivel(string nivel)
        {
            return await _dbContext.Turmas.Include(x => x.listAlunos).FirstOrDefaultAsync(x => x.Nivel == nivel);
        }

        public async Task<List<Turma>> BuscarTodasAsTurmas()
        {
            return await _dbContext.Turmas.Include(x => x.listAlunos).ToListAsync();
        }

        public async Task<Turma> Adicionar(Turma turma)
        {
            turma.listAlunos = null;

            await _dbContext.Turmas.AddAsync(turma);
            await _dbContext.SaveChangesAsync();

            return turma;
        }

        public async Task<Turma> Apagar(string codigo)
        {
            Turma turmaEncontrada = await BuscarPorCodigo(codigo);

            if (turmaEncontrada == null)
            {
                throw new Exception($"Turma para o Código: {codigo} não foi encontrado no banco de dados.");
            }
            if (turmaEncontrada.listAlunos.Count >= 1)
            {
                throw new Exception($"Existe alunos na Turma: {turmaEncontrada.Codigo} e não pode ser apagada do banco de dados.");
            }

            _dbContext.Turmas.Remove(turmaEncontrada);
            await _dbContext.SaveChangesAsync();

            return turmaEncontrada;
        }
    }
}
