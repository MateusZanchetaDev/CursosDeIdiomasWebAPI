using CursosDeIdiomasWebAPI.DataAccess;
using CursosDeIdiomasWebAPI.Models;
using CursosDeIdiomasWebAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CursosDeIdiomasWebAPI.Repository
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly CursoDeIdiomasDbContext _dbContext;
        public AlunoRepository(CursoDeIdiomasDbContext cursoDeIdiomasDbContext)
        {
            _dbContext = cursoDeIdiomasDbContext;
        }

        public async Task<Aluno> BuscarPorCPF(string CPF)
        {
            return await _dbContext.Alunos.FirstOrDefaultAsync(x => x.CPF == CPF);
        }

        public async Task<List<Aluno>> BuscarTodosAlunos()
        {
            return await _dbContext.Alunos.ToListAsync();
        }

        public async Task<Aluno> Adicionar(Aluno aluno)
        {
            await _dbContext.Alunos.AddAsync(aluno);
            await _dbContext.SaveChangesAsync();

            return aluno;
        }

        public async Task<Aluno> Atualizar(Aluno aluno, string CPF)
        {
            Aluno alunoEncontrado = await BuscarPorCPF(CPF);

            if (alunoEncontrado == null)
            {
                throw new Exception($"Aluno para o CPF: {CPF} não foi encontrado no banco de dados.");
            }

            alunoEncontrado.Nome = aluno.Nome;
            alunoEncontrado.CPF = aluno.CPF;
            alunoEncontrado.Email = aluno.Email;

            _dbContext.Alunos.Update(alunoEncontrado);
            await _dbContext.SaveChangesAsync();

            return alunoEncontrado;
        }

        public async Task<Aluno> Apagar(string CPF)
        {
            Aluno alunoEncontrado = await BuscarPorCPF(CPF);

            if (alunoEncontrado == null)
            {
                throw new Exception($"Aluno para o CPF: {CPF} não foi encontrado no banco de dados.");
            }

            _dbContext.Alunos.Remove(alunoEncontrado);
            await _dbContext.SaveChangesAsync();

            return alunoEncontrado;
        }
    }
}
