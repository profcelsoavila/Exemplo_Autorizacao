namespace Exemplo_Identity_Autorizacao.Models
{
    public class Aluno
    {
        public int AlunoID { get; set; }
        public double Nota1 { get; set; }
        public double Nota2 { get; set; }
        public double Nota3 { get; set; }
        public int ProfessorID { get; set; }
        public Professor Professor { get; set; }
    }

}
