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

        [HttpGet]
        public async Task<ActionResult<List<Turma>>> BuscarTodasTurmas()
        {
            return Ok(await _turmaRepository.BuscarTodasTurmas());
        }

        [HttpGet("{Codigo}")]
        public async Task<ActionResult<Turma>> BuscarPorCodigo(string Codigo)
        {
            return Ok(await _turmaRepository.BuscarPorCodigo(Codigo));
        }

        [HttpPost]
        public async Task<ActionResult<Turma>> Cadastrar([FromBody] Turma turma)
        {
            return Ok(await _turmaRepository.Adicionar(turma));
        }

        [HttpPut("{Codigo}")]
        public async Task<ActionResult<Turma>> Atualizar([FromBody] Turma turma, string Codigo)
        {
            return Ok(await _turmaRepository.Atualizar(turma, turma.Codigo = Codigo));
        }

        [HttpDelete("{Codigo}")]
        public async Task<ActionResult<Turma>> Apagar(string Codigo)
        {
            return Ok(await _turmaRepository.Apagar(Codigo));
        }
    }
}
