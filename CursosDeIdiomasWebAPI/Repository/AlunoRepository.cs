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

            aluno.listTurmas.Add(encontrarTurma);

            await _dbContext.Alunos.AddAsync(aluno);
            await _dbContext.SaveChangesAsync();

            return aluno;
        }

        public async Task<Aluno> AdicionarAlunoTurma(Aluno aluno)
        {
            Turma turmaEncontrada = await _dbContext.Turmas.FirstOrDefaultAsync(t => t.Codigo == aluno.CodigoTurma);
            if (turmaEncontrada == null)
            {
                throw new Exception($"A turma com Código {aluno.CodigoTurma} não foi encontrada.");
            }

            // Busca o aluno existente pelo CPF
            Aluno alunoExistente = await BuscarPorCPF(aluno.CPF);

            if (alunoExistente == null)
            {
                // Se o aluno não existe, cria um novo
                aluno.listTurmas.Add(turmaEncontrada); // Adiciona a turma à lista
                await _dbContext.Alunos.AddAsync(aluno); // Adiciona o novo aluno
            }
            else
            {
                // Se o aluno já existe, verifica se ele já está na turma
                if (alunoExistente.CodigoTurma == aluno.CodigoTurma || alunoExistente.listTurmas.Any(t => t.Codigo == aluno.CodigoTurma))
                {
                    throw new Exception($"O Aluno do CPF: {aluno.CPF} já está cadastrado na Turma: {aluno.CodigoTurma}.");
                }

                // Adiciona a nova turma à lista de turmas do aluno existente
                alunoExistente.listTurmas.Add(turmaEncontrada);

                // Se necessário, atualiza a turma principal do aluno
                alunoExistente.CodigoTurma = aluno.CodigoTurma;

                // Atualiza o aluno no banco de dados
                _dbContext.Alunos.Update(alunoExistente);
            }

            // Salva as alterações no banco de dados
            await _dbContext.SaveChangesAsync();

            return alunoExistente ?? aluno;
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
