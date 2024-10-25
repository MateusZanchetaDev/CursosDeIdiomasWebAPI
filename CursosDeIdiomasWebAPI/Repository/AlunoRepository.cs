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

        public async Task<Aluno> BuscarPorCPF(string cpf)
        {
            return await _dbContext.Alunos.Include(a => a.listTurmas).FirstOrDefaultAsync(a => a.CPF == cpf) ?? 
                throw new Exception($"Aluno para o CPF: {cpf} não foi encontrado no banco de dados.");
        }

        public async Task<List<Aluno>> BuscarPorNome(string nome)
        {
            return await _dbContext.Alunos.Include(a => a.listTurmas).Where(a => a.Nome == nome).ToListAsync();
        }

        public async Task<Turma> BuscarPorCodigoTurma(string codigo)
        {
            return await _dbContext.Turmas.Include(t => t.listAlunos).FirstOrDefaultAsync(t => t.Codigo == codigo);
        }

        public async Task<List<Aluno>> BuscarTodosOsAlunos()
        {
            return await _dbContext.Alunos.ToListAsync();
        }

        public async Task<Aluno> Adicionar(string codigoTurma, Aluno aluno)
        {
            Aluno alunoEncontrado = await BuscarPorCPF(aluno.CPF);
            Turma turmaEncontrada = await BuscarPorCodigoTurma(codigoTurma);

            if (alunoEncontrado != null)
            {
                throw new Exception($"O Aluno do CPF: {aluno.CPF} já existe no banco de dados.");
            }

            ValidaTurmaExistente_QuantidadeAlunosEmTurma(turmaEncontrada, codigoTurma);

            aluno.listTurmas = new List<Turma> { turmaEncontrada };
            aluno.listTurmas.AddRange(aluno.listTurmas);

            await _dbContext.Alunos.AddAsync(aluno);
            await _dbContext.SaveChangesAsync();

            return aluno;
        }

        public async Task<Aluno> AdicionarAlunoTurma(string cpf, string codigoTurma)
        {
            Aluno alunoEncontrado = await BuscarPorCPF(cpf);
            Turma turmaEncontrada = await BuscarPorCodigoTurma(codigoTurma);

            ValidaAlunoCPF(alunoEncontrado, cpf);

            ValidaTurmaExistente_QuantidadeAlunosEmTurma(turmaEncontrada, codigoTurma);

            if (alunoEncontrado.listTurmas.Any(t => t.Codigo == codigoTurma))
            {
                throw new Exception($"O aluno: {alunoEncontrado.CPF}, já está na turma: {codigoTurma}.");
            }

            alunoEncontrado.listTurmas.Add(turmaEncontrada);

            _dbContext.Alunos.Update(alunoEncontrado);
            await _dbContext.SaveChangesAsync();

            return alunoEncontrado;
        }

        public async Task<Aluno> Atualizar(string nome, string cpf, string email)
        {
            Aluno alunoEncontrado = await BuscarPorCPF(cpf);

            ValidaAlunoCPF(alunoEncontrado, cpf);

            alunoEncontrado.Nome = nome;
            alunoEncontrado.CPF = cpf;
            alunoEncontrado.Email = email;

            _dbContext.Alunos.Update(alunoEncontrado);
            await _dbContext.SaveChangesAsync();

            return alunoEncontrado;
        }

        public async Task<Aluno> Apagar(string cpf)
        {
            Aluno alunoEncontrado = await BuscarPorCPF(cpf);

            ValidaAlunoCPF(alunoEncontrado, cpf);

            _dbContext.Alunos.Remove(alunoEncontrado);
            await _dbContext.SaveChangesAsync();

            return alunoEncontrado;
        }

        public void ValidaAlunoCPF(Aluno aluno, string cpf)
        {
            if (aluno == null)
            {
                throw new Exception($"Aluno para o CPF: {cpf} não foi encontrado no banco de dados.");
            }
        }

        public void ValidaTurmaExistente_QuantidadeAlunosEmTurma(Turma turma, string codigo)
        {
            if (turma == null)
            {
                throw new Exception($"A turma com Código: {codigo} não foi encontrada.");
            }

            if (turma.listAlunos.Count >= 5)
            {
                throw new Exception($"A turma {codigo} só pode ter no máximo 5 alunos. Quantidade atual: {turma.listAlunos.Count}.");
            }
        }
    }
}
