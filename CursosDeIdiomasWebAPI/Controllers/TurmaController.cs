using CursosDeIdiomasWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CursosDeIdiomasWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurmaController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Turma>> BuscarTodasTurmas()
        {
            return Ok();
        }
    }
}
