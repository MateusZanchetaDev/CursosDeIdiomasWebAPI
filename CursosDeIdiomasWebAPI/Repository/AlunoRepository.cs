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
            // Verifica se o aluno já existe pelo CPF
            Aluno encontrarAluno = await BuscarPorCPF(aluno.CPF);

            // Se o aluno já existe, lança uma exceção
            if (encontrarAluno != null)
            {
                throw new Exception($"O Aluno do CPF: {aluno.CPF} já existe no banco de dados.");
            }

            // Obtém as turmas válidas (aquelas que existem e têm menos de 5 alunos)
            var turmasValidas = new List<Turma>();
            foreach (var codigoTurma in aluno.listTurmas.Select(t => t.Codigo))
            {
                var encontrarTurma = await _dbContext.Turmas.Include(t => t.listAlunos)
                    .FirstOrDefaultAsync(t => t.Codigo == codigoTurma);

                if (encontrarTurma == null)
                {
                    throw new Exception($"A turma com Código {codigoTurma} não foi encontrada.");
                }

                if (encontrarTurma.listAlunos.Count >= 5)
                {
                    throw new Exception($"A turma {codigoTurma} só pode ter no máximo 5 alunos. Quantidade atual: {encontrarTurma.listAlunos.Count}.");
                }

                turmasValidas.Add(encontrarTurma);
            }

            // Adiciona as turmas válidas ao aluno
            aluno.listTurmas.AddRange(turmasValidas);

            // Adiciona o aluno no banco de dados
            await _dbContext.Alunos.AddAsync(aluno);
            await _dbContext.SaveChangesAsync();

            return aluno;
        }

        public async Task<Aluno> AdicionarAlunoTurma(string CPF, string CodigoTurmaInformado)
        {
            // Busca o aluno existente pelo CPF
            Aluno alunoExistente = await BuscarPorCPF(CPF);

            if (alunoExistente == null)
            {
                throw new Exception($"Aluno com o CPF {CPF} não encontrado.");
            }

            foreach (var codigoTurma in CodigoTurmaInformado)
            {
                // Busca a turma pelo código
                Turma turmaEncontrada = await _dbContext.Turmas.FirstOrDefaultAsync(t => t.Codigo == CodigoTurmaInformado);

                if (turmaEncontrada == null)
                {
                    throw new Exception($"A turma com Código {codigoTurma} não foi encontrada.");
                }

                // Verifica se o aluno já está na turma
                if (!alunoExistente.listTurmas.Any(t => t.Codigo == CodigoTurmaInformado))
                {
                    // Adiciona a turma à lista de turmas do aluno
                    alunoExistente.listTurmas.Add(turmaEncontrada);
                }
            }

            // Atualiza o aluno no banco de dados
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
            //alunoEncontrado.CodigoTurma = aluno.CodigoTurma;

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
