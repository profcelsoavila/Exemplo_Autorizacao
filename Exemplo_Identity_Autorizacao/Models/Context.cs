using Exemplo_Identity_Autorizacao.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class Context : IdentityDbContext
{
    public Context(DbContextOptions<Context> options)
        : base(options)
    {
    }
    // DbSets para as entidades Aluno e Professor
    public DbSet<Professor> Professores { get; set; }
    public DbSet<Aluno> Alunos { get; set; }

}