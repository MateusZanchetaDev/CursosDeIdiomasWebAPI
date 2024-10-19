namespace CursosDeIdiomasWebAPI.Models
{
    public class Turma
    {
        public string Codigo { get; set; }
        public string Nivel { get; set; }
        public List<Aluno>? listAlunos { get; set; }
    }
}
