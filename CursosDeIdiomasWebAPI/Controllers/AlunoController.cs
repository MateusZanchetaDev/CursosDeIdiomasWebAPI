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

        public AlunoController(IAlunoRepository alunoRepository)
        {
            _alunoRepository = alunoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Aluno>>> BuscarTodosAlunos()
        {
            return Ok(await _alunoRepository.BuscarTodosAlunos());
        }

        [HttpPost("{CodigoTurma}")]
        public async Task<ActionResult<Aluno>> Cadastrar(string CodigoTurma, [FromBody] Aluno aluno)
        {
            return Ok(await _alunoRepository.Adicionar(CodigoTurma, aluno));
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
        public async Task<ActionResult<Aluno>> Atualizar(string Nome, string CPF, string Email)
        {
            return Ok(await _alunoRepository.Atualizar(Nome, CPF, Email));
        }

        [HttpDelete("{CPF}")]
        public async Task<ActionResult<Aluno>> Apagar(string CPF)
        {
            return Ok(await _alunoRepository.Apagar(CPF));
        }
    }
}
