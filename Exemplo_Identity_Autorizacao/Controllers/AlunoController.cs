using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[Authorize(Roles = "Aluno, Professor")] // Restringe o acesso a Alunos e Professores
public class AlunoController : Controller
{
    private readonly Context _context;

    public AlunoController(Context context)
    {
        _context = context;
    }

    // Action para visualizar as notas de um aluno
    public IActionResult VisualizarNotas()
    {
        return View(_context.Alunos
            .Include(p => p.Professor));
    }
}