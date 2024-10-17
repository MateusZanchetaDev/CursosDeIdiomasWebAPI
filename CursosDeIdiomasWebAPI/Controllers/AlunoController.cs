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
            List<Aluno> alunos = await _alunoRepository.BuscarTodosAlunos();
            return Ok(alunos);
        }

        [HttpGet("{CPF}")]
        public async Task<ActionResult<Aluno>> BuscarPorCPF(string CPF)
        {
            Aluno aluno = await _alunoRepository.BuscarPorCPF(CPF);
            return Ok(aluno);
        }

        [HttpPost]
        public async Task<ActionResult<Aluno>> Cadastrar([FromBody]Aluno aluno)
        {
            Aluno alunoAdicionado = await _alunoRepository.Adicionar(aluno);
            return Ok(alunoAdicionado);
        }

        [HttpPut("{CPF}")]
        public async Task<ActionResult<Aluno>> Atualizar([FromBody] Aluno aluno, string CPF)
        {
            aluno.CPF = CPF;
            Aluno alunoAtualizado = await _alunoRepository.Atualizar(aluno, CPF);
            return Ok(alunoAtualizado);
        }

        [HttpDelete("{CPF}")]
        public async Task<ActionResult<Aluno>> Apagar(string CPF)
        {
            Aluno alunoAtualizado = await _alunoRepository.Apagar(CPF);
            return Ok(alunoAtualizado);
        }
    }
}
