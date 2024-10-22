using CursosDeIdiomasWebAPI.Models;

namespace CursosDeIdiomasWebAPI.Repository.Interfaces
{
    public interface IAlunoRepository
    {
        Task<List<Aluno>> BuscarTodosAlunos();
        Task<Aluno> BuscarPorCPF(string CPF);
        Task<Aluno> Adicionar(Aluno aluno);
        Task<Aluno> AdicionarAlunoTurma(string CPF, string CodigoTurmaInformado);
        Task<Aluno> Atualizar(Aluno aluno, string CPF);
        Task<Aluno> Apagar(string CPF);
    }
}
