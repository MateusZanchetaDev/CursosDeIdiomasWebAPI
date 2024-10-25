using CursosDeIdiomasWebAPI.Models;

namespace CursosDeIdiomasWebAPI.Repository.Interfaces
{
    public interface IAlunoRepository
    {
        Task<List<Aluno>> BuscarTodosOsAlunos();
        Task<Aluno> BuscarPorCPF(string cpf);
        Task<List<Aluno>> BuscarPorNome(string nome);
        Task<Aluno> Adicionar(string codigoTurma, Aluno aluno);
        Task<Aluno> AdicionarAlunoTurma(string cpf, string codigoTurma);
        Task<Aluno> Atualizar(string nome, string cpf, string email);
        Task<Aluno> Apagar(string cpf);
    }
}
