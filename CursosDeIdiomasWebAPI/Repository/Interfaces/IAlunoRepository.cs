using CursosDeIdiomasWebAPI.Models;

namespace CursosDeIdiomasWebAPI.Repository.Interfaces
{
    public interface IAlunoRepository
    {
        Task<List<Aluno>> BuscarTodosAlunos();
        Task<Aluno> BuscarPorCPF(string CPF);
        Task<Aluno> Adicionar(string CodigoTurma, Aluno aluno);
        Task<Aluno> AdicionarAlunoTurma(string CPF, string CodigoTurmaInformado);
        Task<Aluno> Atualizar(string Nome, string CPF, string Email);
        Task<Aluno> Apagar(string CPF);
    }
}
