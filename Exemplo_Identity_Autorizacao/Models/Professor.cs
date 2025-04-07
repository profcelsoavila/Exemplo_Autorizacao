namespace Exemplo_Identity_Autorizacao.Models
{
    public class Professor
    {
        public int ProfessorID { get; set; }
        public string Nome { get; set; }
        public string Disciplina { get; set; }
        public ICollection<Aluno> Alunos { get; set; }
    }

}
