using System.Text.Json.Serialization;

namespace CursosDeIdiomasWebAPI.Models
{
    public class Aluno
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string CodigoTurma { get; set; }
        [JsonIgnore] public List<Turma>? listTurmas { get; set; } = new();
    }
}