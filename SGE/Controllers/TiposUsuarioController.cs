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
    public class TiposUsuarioController : Controller
    {
        private readonly SGEContext _context;

        public TiposUsuarioController(SGEContext context)
        {
            _context = context;
        }

        // GET: TiposUsuario
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

            return View(await _context.TiposUsuario.ToListAsync());
        }

        // GET: TiposUsuario/Details/5
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

            var tipoUsuario = await _context.TiposUsuario
                .FirstOrDefaultAsync(m => m.TipoUsuarioId == id);
            if (tipoUsuario == null)
            {
                return NotFound();
            }

            return View(tipoUsuario);
        }

        // GET: TiposUsuario/Create
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

            return View();
        }

        // POST: TiposUsuario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoUsuarioId,Tipo")] TipoUsuario tipoUsuario)
        {
            if (ModelState.IsValid)
            {
                tipoUsuario.TipoUsuarioId = Guid.NewGuid();
                _context.Add(tipoUsuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoUsuario);
        }

        // GET: TiposUsuario/Edit/5
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

            var tipoUsuario = await _context.TiposUsuario.FindAsync(id);
            if (tipoUsuario == null)
            {
                return NotFound();
            }
            return View(tipoUsuario);
        }

        // POST: TiposUsuario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TipoUsuarioId,Tipo")] TipoUsuario tipoUsuario)
        {
            if (id != tipoUsuario.TipoUsuarioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                TipoUsuario tipoUsuarioAntigo = _context.TiposUsuario.Where(a => a.TipoUsuarioId == tipoUsuario.TipoUsuarioId).FirstOrDefault();
                if (tipoUsuarioAntigo.Tipo == "Administrador" || tipoUsuarioAntigo.Tipo == "Aluno")
                {
                    ViewData["Erro"] = "Os tipos de usuários ADMINISTRADOR e ALUNO não podem ser alterados!";
                    return View(tipoUsuario);
                }

                try
                {
                    _context.Update(tipoUsuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoUsuarioExists(tipoUsuario.TipoUsuarioId))
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
            return View(tipoUsuario);
        }

        // GET: TiposUsuario/Delete/5
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

            var tipoUsuario = await _context.TiposUsuario
                .FirstOrDefaultAsync(m => m.TipoUsuarioId == id);
            if (tipoUsuario == null)
            {
                return NotFound();
            }

            return View(tipoUsuario);
        }

        // POST: TiposUsuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            TipoUsuario tipoUsuario = _context.TiposUsuario.Where(a => a.TipoUsuarioId == id).FirstOrDefault();
            if (tipoUsuario.Tipo == "Administrador" || tipoUsuario.Tipo == "Aluno")
            {
                ViewData["Erro"] = "Os tipos de usuários ADMINISTRADOR e ALUNO não podem ser Excluídos!";
                return View(tipoUsuario);
            }
            if (tipoUsuario != null)
            {
                _context.TiposUsuario.Remove(tipoUsuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoUsuarioExists(Guid id)
        {
            return _context.TiposUsuario.Any(e => e.TipoUsuarioId == id);
        }
    }
}
