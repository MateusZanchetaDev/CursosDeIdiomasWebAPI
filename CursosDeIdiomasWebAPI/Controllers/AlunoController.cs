using CursosDeIdiomasWebAPI.DataAccess;
using CursosDeIdiomasWebAPI.Models;
using CursosDeIdiomasWebAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CursosDeIdiomasWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly CursoDeIdiomasDbContext _dbContext;

        public AlunoController(CursoDeIdiomasDbContext cursoDeIdiomasDbContext, IAlunoRepository alunoRepository)
        {
            _alunoRepository = alunoRepository;
            _dbContext = cursoDeIdiomasDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Aluno>>> BuscarTodosAlunos()
        {
            return Ok(await _alunoRepository.BuscarTodosAlunos());
        }

        [HttpPost("{CodigoTurma}")]
        public async Task<ActionResult<Aluno>> Cadastrar(string CodigoTurma, [FromBody] Aluno aluno)
        {
            Turma turmaEncontrada = await _dbContext.Turmas.FirstOrDefaultAsync(t => t.Codigo == CodigoTurma);

            if (turmaEncontrada == null)
            {
                return BadRequest($"A turma com Código: {CodigoTurma} não foi encontrada.");
            }

            aluno.listTurmas = new List<Turma> { turmaEncontrada };

            return Ok(await _alunoRepository.Adicionar(aluno));
        }

        [HttpPost("{CPF}/{CodigoTurma}")]
        public async Task<ActionResult<Aluno>> CadastrarAlunoTurma(string CPF, string CodigoTurma)
        {
            return Ok(await _alunoRepository.AdicionarAlunoTurma(CPF, CodigoTurma));
        }

        [HttpGet("{CPF}")]
        public async Task<ActionResult<Aluno>> BuscarPorCPF(string CPF)
        {
            return Ok(await _alunoRepository.BuscarPorCPF(CPF));
        }

        [HttpPut("{CPF}")]
        public async Task<ActionResult<Aluno>> Atualizar([FromBody] Aluno aluno, string CPF)
        {
            return Ok(await _alunoRepository.Atualizar(aluno, aluno.CPF = CPF));
        }

        [HttpDelete("{CPF}")]
        public async Task<ActionResult<Aluno>> Apagar(string CPF)
        {
            return Ok(await _alunoRepository.Apagar(CPF));
        }
    }
}
