using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SGE.Data;
using SGE.Models;

namespace SGE.Controllers
{
    public class AlunosTurmaController : Controller
    {
        private readonly SGEContext _context;

        public AlunosTurmaController(SGEContext context)
        {
            _context = context;
        }

        // GET: AlunosTurma
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                string Email = HttpContext.Session.GetString("email");
                var usuario = _context.Usuarios.Where(a => a.Email == Email).FirstOrDefault();
                Guid idTipoAluno = _context.TiposUsuario.Where(a => a.Tipo == "Aluno").FirstOrDefault().TipoUsuarioId;
                if (usuario.TipoUsuarioId == idTipoAluno)
                {
                    return RedirectToAction("AcessoNegado", "Home");
                }
            }
            var sGEContext = _context.AlunosTurma.Include(a => a.Aluno).Include(a => a.Turma);
            return View(await sGEContext.ToListAsync());
        }

        // GET: AlunosTurma/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                string Email = HttpContext.Session.GetString("email");
                var usuario = _context.Usuarios.Where(a => a.Email == Email).FirstOrDefault();
                Guid idTipoAluno = _context.TiposUsuario.Where(a => a.Tipo == "Aluno").FirstOrDefault().TipoUsuarioId;
                if (usuario.TipoUsuarioId == idTipoAluno)
                {
                    return RedirectToAction("AcessoNegado", "Home");
                }
            }
            if (id == null)
            {
                return NotFound();
            }

            var alunoTurma = await _context.AlunosTurma
                .Include(a => a.Aluno)
                .Include(a => a.Turma)
                .FirstOrDefaultAsync(m => m.AlunoTurmaId == id);
            if (alunoTurma == null)
            {
                return NotFound();
            }

            return View(alunoTurma);
        }

        // GET: AlunosTurma/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                string Email = HttpContext.Session.GetString("email");
                var usuario = _context.Usuarios.Where(a => a.Email == Email).FirstOrDefault();
                Guid idTipoAluno = _context.TiposUsuario.Where(a => a.Tipo == "Aluno").FirstOrDefault().TipoUsuarioId;
                if (usuario.TipoUsuarioId == idTipoAluno)
                {
                    return RedirectToAction("AcessoNegado", "Home");
                }
            }
            ViewData["AlunoId"] = new SelectList(_context.Alunos, "AlunoId", "AlunoId");
            ViewData["TurmaId"] = new SelectList(_context.Turmas, "TurmaId", "TurmaId");
            return View();
        }

        // POST: AlunosTurma/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AlunoTurmaId,AlunoId,TurmaId")] AlunoTurma alunoTurma)
        {
            if (ModelState.IsValid)
            {
                alunoTurma.AlunoTurmaId = Guid.NewGuid();
                _context.Add(alunoTurma);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlunoId"] = new SelectList(_context.Alunos, "AlunoId", "AlunoId", alunoTurma.AlunoId);
            ViewData["TurmaId"] = new SelectList(_context.Turmas, "TurmaId", "TurmaId", alunoTurma.TurmaId);
            return View(alunoTurma);
        }

        // GET: AlunosTurma/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                string Email = HttpContext.Session.GetString("email");
                var usuario = _context.Usuarios.Where(a => a.Email == Email).FirstOrDefault();
                Guid idTipoAluno = _context.TiposUsuario.Where(a => a.Tipo == "Aluno").FirstOrDefault().TipoUsuarioId;
                if (usuario.TipoUsuarioId == idTipoAluno)
                {
                    return RedirectToAction("AcessoNegado", "Home");
                }
            }
            if (id == null)
            {
                return NotFound();
            }

            var alunoTurma = await _context.AlunosTurma.FindAsync(id);
            if (alunoTurma == null)
            {
                return NotFound();
            }
            ViewData["AlunoId"] = new SelectList(_context.Alunos, "AlunoId", "AlunoId", alunoTurma.AlunoId);
            ViewData["TurmaId"] = new SelectList(_context.Turmas, "TurmaId", "TurmaId", alunoTurma.TurmaId);
            return View(alunoTurma);
        }

        // POST: AlunosTurma/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AlunoTurmaId,AlunoId,TurmaId")] AlunoTurma alunoTurma)
        {
            if (id != alunoTurma.AlunoTurmaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alunoTurma);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlunoTurmaExists(alunoTurma.AlunoTurmaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlunoId"] = new SelectList(_context.Alunos, "AlunoId", "AlunoId", alunoTurma.AlunoId);
            ViewData["TurmaId"] = new SelectList(_context.Turmas, "TurmaId", "TurmaId", alunoTurma.TurmaId);
            return View(alunoTurma);
        }

        // GET: AlunosTurma/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                string Email = HttpContext.Session.GetString("email");
                var usuario = _context.Usuarios.Where(a => a.Email == Email).FirstOrDefault();
                Guid idTipoAluno = _context.TiposUsuario.Where(a => a.Tipo == "Aluno").FirstOrDefault().TipoUsuarioId;
                if (usuario.TipoUsuarioId == idTipoAluno)
                {
                    return RedirectToAction("AcessoNegado", "Home");
                }
            }

            if (id == null)
            {
                return NotFound();
            }

            var alunoTurma = await _context.AlunosTurma
                .Include(a => a.Aluno)
                .Include(a => a.Turma)
                .FirstOrDefaultAsync(m => m.AlunoTurmaId == id);
            if (alunoTurma == null)
            {
                return NotFound();
            }

            return View(alunoTurma);
        }

        // POST: AlunosTurma/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var alunoTurma = await _context.AlunosTurma.FindAsync(id);
            if (alunoTurma != null)
            {
                _context.AlunosTurma.Remove(alunoTurma);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlunoTurmaExists(Guid id)
        {
            return _context.AlunosTurma.Any(e => e.AlunoTurmaId == id);
        }

        public async Task<IActionResult> ListaAlunos()
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                string Email = HttpContext.Session.GetString("email");
                var usuario = _context.Usuarios.Where(a => a.Email == Email).FirstOrDefault();
                Guid idTipoAluno = _context.TiposUsuario.Where(a => a.Tipo == "Aluno").FirstOrDefault().TipoUsuarioId;
                if (usuario.TipoUsuarioId == idTipoAluno)
                {
                    return RedirectToAction("AcessoNegado", "Home");
                }
            }
            List<Aluno> alunos = _context.Alunos.Where(a => a.CadAtivo == true).ToList();
            List<Turma> turmas = _context.Turmas.Where(a => a.CadAtivo == true).ToList();
            List<AlunoTurma> alunoTurmas = _context.AlunosTurma.ToList();
            ViewData["Alunos"] = alunos.ToList();
            ViewData["Turmas"] = turmas.ToList();
            return View(alunoTurmas);
        }

        public async Task<IActionResult> SelecionaTurma(Guid id)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                string Email = HttpContext.Session.GetString("email");
                var usuario = _context.Usuarios.Where(a => a.Email == Email).FirstOrDefault();
                Guid idTipoAluno = _context.TiposUsuario.Where(a => a.Tipo == "Aluno").FirstOrDefault().TipoUsuarioId;
                if (usuario.TipoUsuarioId == idTipoAluno)
                {
                    return RedirectToAction("AcessoNegado", "Home");
                }
            }
            List<Aluno> alunos = _context.Alunos.Where(a => a.CadAtivo == true).ToList();
            List<Turma> turmas = _context.Turmas.Where(a => a.CadAtivo == true).ToList();
            List<AlunoTurma> alunoTurmas = _context.AlunosTurma.Where(a => a.TurmaId != id).ToList();
            alunos = alunos.Where(a => !alunoTurmas.Any(at => at.AlunoId == a.AlunoId)).ToList();

            ViewData["Turmas"] = turmas;
            Turma turmaSelecionada = _context.Turmas.Where(a => a.TurmaId == id).FirstOrDefault();
            ViewData["TurmaSelecionada"] = turmaSelecionada.TurmaNome;
            ViewData["TurmaId"] = id;
            List<AlunoTurma> alunosDaTurma = _context.AlunosTurma.Where(at => at.TurmaId == id).ToList();
            List<Aluno> alunosMatriculados = new List<Aluno>();
            foreach (var aluno in alunosDaTurma)
            {
                alunosMatriculados.Add(_context.Alunos.Where(a => a.AlunoId == aluno.AlunoId).FirstOrDefault());
            }
            ViewData["AlunosDaTurma"] = alunosMatriculados;
            List<Aluno> alunosDisponiveis = new List<Aluno>();
            foreach (var aluno in alunos)
            {
                if (!alunosMatriculados.Any(a => a.AlunoId == aluno.AlunoId))
                {
                    alunosDisponiveis.Add(aluno);
                }
            }
            ViewData["Alunos"] = alunosDisponiveis;
            return View("ListaAlunos", alunoTurmas);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionaAlunoTurma(string idAluno, string idTurma)
        {
            if (idTurma == null)
            {
                ViewData["Erro"] = "Selecione uma turma para adicionar Alunos";
                List<Aluno> alunos = _context.Alunos.Where(a => a.CadAtivo == true).ToList();
                List<Turma> turmas = _context.Turmas.Where(a => a.CadAtivo == true).ToList();
                List<AlunoTurma> alunoTurmas = _context.AlunosTurma.ToList();
                ViewData["Alunos"] = alunos.ToList();
                ViewData["Turmas"] = turmas.ToList();
                return View("ListaAlunos", alunoTurmas);
            }
            AlunoTurma alunoTurma = new AlunoTurma();
            alunoTurma.AlunoTurmaId = Guid.NewGuid();
            alunoTurma.AlunoId = Guid.Parse(idAluno);
            alunoTurma.TurmaId = Guid.Parse(idTurma);
            _context.Add(alunoTurma);
            await _context.SaveChangesAsync();
            return RedirectToAction("SelecionaTurma", new { id = idTurma });
        }

        public async Task<IActionResult> RemoveAlunoTurma(string idAluno, string idTurma)
        {
            if (idTurma == null)
            {
                ViewData["Erro"] = "Selecione uma turma para Remover Alunos";
                List<Aluno> alunos = _context.Alunos.Where(a => a.CadAtivo == true).ToList();
                List<Turma> turmas = _context.Turmas.Where(a => a.CadAtivo == true).ToList();
                List<AlunoTurma> alunoTurmas = _context.AlunosTurma.ToList();
                ViewData["Alunos"] = alunos.ToList();
                ViewData["Turmas"] = turmas.ToList();
                return View("ListaAlunos", alunoTurmas);
            }
            AlunoTurma alunoTurma = _context.AlunosTurma.Where(a => a.AlunoId == Guid.Parse(idAluno)).Where(a => a.TurmaId == Guid.Parse(idTurma)).FirstOrDefault();
            _context.Remove(alunoTurma);
            await _context.SaveChangesAsync();
            return RedirectToAction("SelecionaTurma", new { id = idTurma });
        }

        public async Task<IActionResult> FiltarTurmas(string filtro)
        {
            if (filtro == null)
            {
                return RedirectToAction("ListaAlunos");
            }
            List<Turma> turmas = _context.Turmas.Where(a => a.CadAtivo == true).ToList();
            turmas = turmas.Where(a => a.TurmaNome.ToUpper().Contains(filtro.ToUpper())).ToList();
            ViewData["Turmas"] = turmas;
            List<Aluno> alunos = _context.Alunos.Where(a => a.CadAtivo == true).ToList();
            List<AlunoTurma> alunoTurmas = _context.AlunosTurma.ToList();
            ViewData["Alunos"] = alunos.ToList();
            ViewData["Turmas"] = turmas.ToList();
            return View("ListaAlunos", _context.AlunosTurma.ToList());
        }
    }

}
