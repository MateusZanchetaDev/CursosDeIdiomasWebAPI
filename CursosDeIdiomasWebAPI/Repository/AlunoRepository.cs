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

        public async Task<List<Aluno>> BuscarTodosAlunos()
        {
            return await _dbContext.Alunos.ToListAsync();
        }

        public async Task<Aluno> Adicionar(Aluno aluno)
        {
            Aluno encontrarAluno = await BuscarPorCPF(aluno.CPF);

            if (encontrarAluno != null)
            {
                throw new Exception($"O Aluno do CPF: {aluno.CPF} já existe no banco de dados.");
            }

            var turmasValidas = new List<Turma>();

            foreach (var codigoTurma in aluno.listTurmas.Select(t => t.Codigo))
            {
                var encontrarTurma = await _dbContext.Turmas.Include(t => t.listAlunos).FirstOrDefaultAsync(t => t.Codigo == codigoTurma);

                ValidaQuantidadeAlunosEmTurma(encontrarTurma, codigoTurma);

                turmasValidas.Add(encontrarTurma);
            }

            aluno.listTurmas.AddRange(turmasValidas);

            await _dbContext.Alunos.AddAsync(aluno);
            await _dbContext.SaveChangesAsync();

            return aluno;
        }

        public async Task<Aluno> AdicionarAlunoTurma(string CPF, string CodigoTurmaInformado)
        {
            Aluno alunoExistente = await BuscarPorCPF(CPF);

            if (alunoExistente == null)
            {
                throw new Exception($"Aluno com o CPF {CPF} não encontrado.");
            }

            foreach (var codigoTurma in CodigoTurmaInformado)
            {
                Turma turmaEncontrada = await _dbContext.Turmas.Include(t => t.listAlunos).FirstOrDefaultAsync(t => t.Codigo == CodigoTurmaInformado);

                if (turmaEncontrada == null)
                {
                    throw new Exception($"A turma com Código: {CodigoTurmaInformado} não foi encontrada.");
                }

                ValidaQuantidadeAlunosEmTurma(turmaEncontrada, CodigoTurmaInformado);

                if (alunoExistente.listTurmas.Any(t => t.Codigo == CodigoTurmaInformado))
                {
                    throw new Exception($"O aluno: {alunoExistente.CPF}, já está na turma: {CodigoTurmaInformado}.");
                }

                alunoExistente.listTurmas.Add(turmaEncontrada);
                break;
            }

            _dbContext.Alunos.Update(alunoExistente);
            await _dbContext.SaveChangesAsync();

            return alunoExistente;
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

        public void ValidaQuantidadeAlunosEmTurma(Turma turmaInformada, string codigoTurma)
        {
            if (turmaInformada.listAlunos.Count >= 5)
            {
                throw new Exception($"A turma {codigoTurma} só pode ter no máximo 5 alunos. Quantidade atual: {turmaInformada.listAlunos.Count}.");
            }
        }
    }
}
