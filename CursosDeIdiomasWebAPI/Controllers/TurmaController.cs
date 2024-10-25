using CursosDeIdiomasWebAPI.Models;
using CursosDeIdiomasWebAPI.Repository;
using CursosDeIdiomasWebAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CursosDeIdiomasWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurmaController : ControllerBase
    {
        private readonly ITurmaRepository _turmaRepository;

        public TurmaController(ITurmaRepository turmaRepository)
        {
            _turmaRepository = turmaRepository;
        }

        [HttpGet("Listar todas")]
        public async Task<ActionResult<List<Turma>>> BuscarTodasTurmas()
        {
            return Ok(await _turmaRepository.BuscarTodasAsTurmas());
        }

        [HttpPost("Cadastrar")]
        public async Task<ActionResult<Turma>> Cadastrar([FromBody] Turma turma)
        {
            return Ok(await _turmaRepository.Adicionar(turma));
        }

        [HttpGet("Buscar por Codigo")]
        public async Task<ActionResult<Turma>> BuscarPorCodigo(string codigo)
        {
            return Ok(await _turmaRepository.BuscarPorCodigo(codigo));
        }

        [HttpGet("Buscar por Nível")]
        public async Task<ActionResult<Turma>> BuscarPorNivel(string nivel)
        {
            return Ok(await _turmaRepository.BuscarPorNivel(nivel));
        }

        [HttpDelete("Apagar")]
        public async Task<ActionResult<Turma>> Apagar(string codigo)
        {
            return Ok(await _turmaRepository.Apagar(codigo));
        }
    }
}
