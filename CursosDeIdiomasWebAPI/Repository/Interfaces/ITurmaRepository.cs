using CursosDeIdiomasWebAPI.Models;

namespace CursosDeIdiomasWebAPI.Repository.Interfaces
{
    public interface ITurmaRepository
    {
        Task<List<Turma>> BuscarTodasTurmas();
        Task<Turma> BuscarPorCodigo(string Codigo);
        Task<Turma> Adicionar(Turma turma);
        Task<Turma> Atualizar(Turma turma, string Codigo);
        Task<Turma> Apagar(string Codigo);
    }
}
