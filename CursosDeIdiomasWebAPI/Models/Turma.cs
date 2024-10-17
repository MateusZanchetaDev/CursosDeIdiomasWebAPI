namespace CursosDeIdiomasWebAPI.Models
{
    public class Turma
    {
        public string Codigo { get; set; }
        public string Nivel { get; set; }
        public string? AlunoCPF { get; set; }
        public Aluno? Aluno { get; set; }
    }
}
