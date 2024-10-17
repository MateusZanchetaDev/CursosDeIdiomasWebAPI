using CursosDeIdiomasWebAPI.Models;
using CursosDeIdiomasWebAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("{CPF}")]
        public async Task<ActionResult<Aluno>> BuscarPorCPF(string CPF)
        {
            return Ok(await _alunoRepository.BuscarPorCPF(CPF));
        }

        [HttpPost]
        public async Task<ActionResult<Aluno>> Cadastrar([FromBody]Aluno aluno)
        {
            return Ok(await _alunoRepository.Adicionar(aluno));
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
