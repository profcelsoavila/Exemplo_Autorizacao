using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


[Authorize(Roles = "Aluno")] // Restringe o acesso a Alunos
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
        var alunoId = User.Identity.Name; // Identifica o aluno logado
        var aluno = _context.Alunos.FirstOrDefault(a => a.AlunoID == int.Parse(alunoId));
        if (aluno == null)
        {
            return NotFound();
        }
        return View(aluno);
    }
}