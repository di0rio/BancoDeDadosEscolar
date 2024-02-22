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
    public class SalasController : Controller
    {
        private readonly SGEContext _context;

        public SalasController(SGEContext context)
        {
            _context = context;
        }

        // GET: Salas
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

            return View(await _context.Salas.ToListAsync());
        }

        // GET: Salas/Details/5
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

            var sala = await _context.Salas
                .FirstOrDefaultAsync(m => m.SalaId == id);
            if (sala == null)
            {
                return NotFound();
            }

            return View(sala);
        }

        // GET: Salas/Create
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

        // POST: Salas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalaId,SalaNome,CadAtivo,CadInativo")] Sala sala)
        {
            if (ModelState.IsValid)
            {
                if (sala.CadAtivo == false)
                {
                    sala.CadInativo = DateTime.Now;
                }
                else
                {
                    sala.CadInativo = null;
                }
                sala.SalaId = Guid.NewGuid();
                _context.Add(sala);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sala);
        }

        // GET: Salas/Edit/5
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

            var sala = await _context.Salas.FindAsync(id);
            if (sala == null)
            {
                return NotFound();
            }
            return View(sala);
        }

        // POST: Salas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("SalaId,SalaNome,CadAtivo,CadInativo")] Sala sala)
        {
            if (id != sala.SalaId)
            {
                return NotFound();
            }
            if (sala.CadAtivo == false)
            {
                sala.CadInativo = DateTime.Now;
            }
            else
            {
                sala.CadInativo = null;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sala);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalaExists(sala.SalaId))
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
            return View(sala);
        }

        // GET: Salas/Delete/5
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

            var sala = await _context.Salas
                .FirstOrDefaultAsync(m => m.SalaId == id);
            if (sala == null)
            {
                return NotFound();
            }

            return View(sala);
        }

        // POST: Salas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var sala = await _context.Salas.FindAsync(id);
            if (sala != null)
            {

                sala.CadInativo = DateTime.Now;
                sala.CadAtivo = false;
                _context.Update(sala);
                await _context.SaveChangesAsync();
                //_context.Salas.Remove(sala);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalaExists(Guid id)
        {
            return _context.Salas.Any(e => e.SalaId == id);
        }
    }
}
