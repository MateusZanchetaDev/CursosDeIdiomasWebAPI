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
            Aluno encontrarAluno = await BuscarPorCPF(aluno.CPF);
            Turma encontrarTurma = await _dbContext.Turmas.Include(t => t.listAlunos).FirstOrDefaultAsync(t => t.Codigo == aluno.CodigoTurma);

            if (encontrarTurma == null)
            {
                throw new Exception($"O aluno precisa informar uma Turma, para finalizar o cadastro. Turma informada: {aluno.CodigoTurma}");
            }
            if (encontrarTurma.listAlunos.Count >= 5)
            {
                throw new Exception($"A turma só pode ter no máximo 5 alunos. Quantidade de alunos atual: {encontrarTurma.listAlunos.Count}.");
            }
            else if (encontrarAluno != null && encontrarAluno.CPF == aluno.CPF)
            {
                throw new Exception($"O Aluno do CPF: {aluno.CPF} já existe no banco de dados.");
            }

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
            alunoEncontrado.CodigoTurma = aluno.CodigoTurma;

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
