using CursosDeIdiomasWebAPI.Models;

namespace CursosDeIdiomasWebAPI.Repository.Interfaces
{
    public interface ITurmaRepository
    {
        Task<List<Turma>> BuscarTodasAsTurmas();
        Task<Turma> BuscarPorCodigo(string codigo);
        Task<Turma> BuscarPorNivel(string nivel);
        Task<Turma> Adicionar(Turma turma);
        Task<Turma> Apagar(string codigo);
    }
}
