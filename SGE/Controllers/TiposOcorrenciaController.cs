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
    public class TiposOcorrenciaController : Controller
    {
        private readonly SGEContext _context;

        public TiposOcorrenciaController(SGEContext context)
        {
            _context = context;
        }

        // GET: TiposOcorrencia
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

            return View(await _context.TiposOcorrencia.ToListAsync());
        }

        // GET: TiposOcorrencia/Details/5
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

            var tipoOcorrencia = await _context.TiposOcorrencia
                .FirstOrDefaultAsync(m => m.TipoOcorrenciaId == id);
            if (tipoOcorrencia == null)
            {
                return NotFound();
            }

            return View(tipoOcorrencia);
        }

        // GET: TiposOcorrencia/Create
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

        // POST: TiposOcorrencia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoOcorrenciaId,TipoOcorrenciaNome,CadAtivo,CadInativo")] TipoOcorrencia tipoOcorrencia)
        {
            if (ModelState.IsValid)
            {
                if (tipoOcorrencia.CadAtivo == false)
                {
                    tipoOcorrencia.CadInativo = DateTime.Now;
                }
                else
                {
                    tipoOcorrencia.CadInativo = null;
                }
                tipoOcorrencia.TipoOcorrenciaId = Guid.NewGuid();
                _context.Add(tipoOcorrencia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoOcorrencia);
        }

        // GET: TiposOcorrencia/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoOcorrencia = await _context.TiposOcorrencia.FindAsync(id);
            if (tipoOcorrencia == null)
            {
                return NotFound();
            }
            return View(tipoOcorrencia);
        }

        // POST: TiposOcorrencia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TipoOcorrenciaId,TipoOcorrenciaNome,CadAtivo,CadInativo")] TipoOcorrencia tipoOcorrencia)
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

            if (id != tipoOcorrencia.TipoOcorrenciaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (tipoOcorrencia.CadAtivo == false)
                    {
                        tipoOcorrencia.CadInativo = DateTime.Now;
                    }
                    else
                    {
                        tipoOcorrencia.CadInativo = null;
                    }
                    _context.Update(tipoOcorrencia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoOcorrenciaExists(tipoOcorrencia.TipoOcorrenciaId))
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
            return View(tipoOcorrencia);
        }

        // GET: TiposOcorrencia/Delete/5
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

            var tipoOcorrencia = await _context.TiposOcorrencia
                .FirstOrDefaultAsync(m => m.TipoOcorrenciaId == id);
            if (tipoOcorrencia == null)
            {
                return NotFound();
            }

            return View(tipoOcorrencia);
        }

        // POST: TiposOcorrencia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var tipoOcorrencia = await _context.TiposOcorrencia.FindAsync(id);
            if (tipoOcorrencia != null)
            {
                tipoOcorrencia.CadAtivo = false;
                tipoOcorrencia.CadInativo = DateTime.Now;
                _context.TiposOcorrencia.Update(tipoOcorrencia);
                _context.SaveChanges();
                //_context.TiposOcorrencia.Remove(tipoOcorrencia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoOcorrenciaExists(Guid id)
        {
            return _context.TiposOcorrencia.Any(e => e.TipoOcorrenciaId == id);
        }
    }
}
