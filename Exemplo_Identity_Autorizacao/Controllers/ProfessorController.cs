using Exemplo_Identity_Autorizacao.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

[Authorize(Roles = "Professor")] // Restringe o acesso a Professores
public class ProfessorController : Controller
{
    private readonly Context _context;

    public ProfessorController(Context context)
    {
        _context = context;
    }

    // Action para cadastrar um professor (GET)
    [HttpGet]
    public IActionResult CadastrarProfessor()
    {
        return View();
    }

    // Action para cadastrar um professor (POST)
    [HttpPost]
    public IActionResult CadastrarProfessor(Professor professor)
    {
        if (ModelState.IsValid)
        {
            _context.Professores.Add(professor);
            _context.SaveChanges();
            return RedirectToAction("ListaAlunos", "Professor"); // Redireciona para a página inicial
        }
        return View(professor);
    }

    // Action para cadastrar um aluno (GET)
    // Action para exibir o formulário de cadastro de aluno (GET)
    public IActionResult CadastrarAluno()
    {
        // Busca os professores cadastrados no banco de dados
        var professores = _context.Professores.Select(p => new { p.ProfessorID, p.Nome }).ToList();

        // Cria uma lista SelectList para enviar à view
        ViewBag.Professores = new SelectList(professores, "ProfessorID", "Nome");

        return View();
    }

    // Action para processar o cadastro do aluno (POST)
    [HttpPost]
    public IActionResult CadastrarAluno(Aluno aluno)
    {
        if (ModelState.IsValid)
        {
            // Adiciona o aluno ao banco de dados
            _context.Alunos.Add(aluno);
            _context.SaveChanges();
            return RedirectToAction("ListaAlunos");
        }

        // Caso a validação falhe, enviar novamente a lista de professores
        var professores = _context.Professores.Select(p => new { p.ProfessorID, p.Nome }).ToList();
        ViewBag.Professores = new SelectList(professores, "ProfessorID", "Nome");

        return View(aluno);
    }

    // Action para listar alunos do professor
    public IActionResult ListaAlunos()
    {
        var professorId = User.Identity.Name; // Identifica o professor logado
        var alunos = _context.Alunos.Where(a => a.ProfessorID == int.Parse(professorId)).ToList();
        return View(alunos);
    }

    // Action para editar notas de um aluno
    [HttpGet]
    public IActionResult EditarNotas(int alunoId)
    {
        var aluno = _context.Alunos.FirstOrDefault(a => a.AlunoID == alunoId);
        if (aluno == null)
        {
            return NotFound();
        }
        return View(aluno);
    }

    [HttpPost]
    public IActionResult EditarNotas(Aluno aluno)
    {
        if (ModelState.IsValid)
        {
            _context.Alunos.Update(aluno);
            _context.SaveChanges();
            return RedirectToAction("ListaAlunos");
        }
        return View(aluno);
    }
}