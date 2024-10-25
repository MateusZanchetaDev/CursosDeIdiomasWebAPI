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

        [HttpGet("Listar todos")]
        public async Task<ActionResult<List<Aluno>>> BuscarTodosAlunos()
        {
            return Ok(await _alunoRepository.BuscarTodosOsAlunos());
        }

        [HttpPost("Cadastro")]
        public async Task<ActionResult<Aluno>> Cadastrar(string codigoTurma, [FromBody] Aluno aluno)
        {
            return Ok(await _alunoRepository.Adicionar(codigoTurma, aluno));
        }

        [HttpPost("Cadastrar aluno em turma")]
        public async Task<ActionResult<Aluno>> CadastrarAlunoTurma(string cpf, string codigoTurma)
        {
            return Ok(await _alunoRepository.AdicionarAlunoTurma(cpf, codigoTurma));
        }

        [HttpGet("Buscar por CPF")]
        public async Task<ActionResult<Aluno>> BuscarPorCPF(string cpf)
        {
            return Ok(await _alunoRepository.BuscarPorCPF(cpf));
        }

        [HttpGet("Buscar por Nome")]
        public async Task<ActionResult<Aluno>> BuscarPorNome(string nome)
        {
            return Ok(await _alunoRepository.BuscarPorNome(nome));
        }

        [HttpPut("Editar")]
        public async Task<ActionResult<Aluno>> Atualizar(string cpf, string nome, string email)
        {
            return Ok(await _alunoRepository.Atualizar(nome, cpf, email));
        }

        [HttpDelete("Apagar")]
        public async Task<ActionResult<Aluno>> Apagar(string cpf)
        {
            return Ok(await _alunoRepository.Apagar(cpf));
        }
    }
}
