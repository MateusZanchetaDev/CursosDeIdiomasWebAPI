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
            return await _dbContext.Alunos.Include(a => a.listTurmas).FirstOrDefaultAsync(a => a.CPF == CPF);
        }

        public async Task<Turma> BuscarTurma(string CodigoTurma)
        {
            return await _dbContext.Turmas.Include(t => t.listAlunos).FirstOrDefaultAsync(t => t.Codigo == CodigoTurma);
        }

        public async Task<List<Aluno>> BuscarTodosAlunos()
        {
            return await _dbContext.Alunos.ToListAsync();
        }

        public async Task<Aluno> Adicionar(string CodigoTurma, Aluno aluno)
        {
            Aluno encontrarAluno = await BuscarPorCPF(aluno.CPF);
            Turma turmaEncontrada = await BuscarTurma(CodigoTurma);

            if (encontrarAluno != null)
            {
                throw new Exception($"O Aluno do CPF: {aluno.CPF} já existe no banco de dados.");
            }

            ValidaTurmaExistente_QuantidadeAlunosEmTurma(turmaEncontrada, CodigoTurma);

            aluno.listTurmas = new List<Turma> { turmaEncontrada };
            aluno.listTurmas.AddRange(aluno.listTurmas);

            await _dbContext.Alunos.AddAsync(aluno);
            await _dbContext.SaveChangesAsync();

            return aluno;
        }

        public async Task<Aluno> AdicionarAlunoTurma(string CPF, string CodigoTurma)
        {
            Aluno alunoExistente = await BuscarPorCPF(CPF);
            Turma turmaEncontrada = await BuscarTurma(CodigoTurma);

            ValidaAlunoCPF(alunoExistente, CPF);

            ValidaTurmaExistente_QuantidadeAlunosEmTurma(turmaEncontrada, CodigoTurma);

            if (alunoExistente.listTurmas.Any(t => t.Codigo == CodigoTurma))
            {
                throw new Exception($"O aluno: {alunoExistente.CPF}, já está na turma: {CodigoTurma}.");
            }

            alunoExistente.listTurmas.Add(turmaEncontrada);

            _dbContext.Alunos.Update(alunoExistente);
            await _dbContext.SaveChangesAsync();

            return alunoExistente;
        }

        public async Task<Aluno> Atualizar(string Nome, string CPF, string Email)
        {
            Aluno alunoEncontrado = await BuscarPorCPF(CPF);

            ValidaAlunoCPF(alunoEncontrado, CPF);

            alunoEncontrado.Nome = Nome;
            alunoEncontrado.CPF = CPF;
            alunoEncontrado.Email = Email;

            _dbContext.Alunos.Update(alunoEncontrado);
            await _dbContext.SaveChangesAsync();

            return alunoEncontrado;
        }

        public async Task<Aluno> Apagar(string CPF)
        {
            Aluno alunoEncontrado = await BuscarPorCPF(CPF);

            ValidaAlunoCPF(alunoEncontrado, CPF);

            _dbContext.Alunos.Remove(alunoEncontrado);
            await _dbContext.SaveChangesAsync();

            return alunoEncontrado;
        }

        public void ValidaAlunoCPF(Aluno alunoInformado, string CPF)
        {
            if (alunoInformado == null)
            {
                throw new Exception($"Aluno para o CPF: {CPF} não foi encontrado no banco de dados.");
            }
        }

        public void ValidaTurmaExistente_QuantidadeAlunosEmTurma(Turma turmaInformada, string codigoTurma)
        {
            if (turmaInformada == null)
            {
                throw new Exception($"A turma com Código: {codigoTurma} não foi encontrada.");
            }

            if (turmaInformada.listAlunos.Count >= 5)
            {
                throw new Exception($"A turma {codigoTurma} só pode ter no máximo 5 alunos. Quantidade atual: {turmaInformada.listAlunos.Count}.");
            }
        }
    }
}
