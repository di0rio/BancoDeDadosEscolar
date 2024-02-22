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
    public class UsuariosController : Controller
    {
        private readonly SGEContext _context;

        public UsuariosController(SGEContext context)
        {
            _context = context;
        }

        // GET: Usuarios
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

            var sGEContext = _context.Usuarios.Include(u => u.TipoUsuario);
            return View(await sGEContext.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                string Email = HttpContext.Session.GetString("email");
                var user = _context.Usuarios.Where(a => a.Email == Email).FirstOrDefault();
                Guid idTipoAluno = _context.TiposUsuario.Where(a => a.Tipo == "Aluno").FirstOrDefault().TipoUsuarioId;
                if (user.TipoUsuarioId == idTipoAluno)
                {
                    return RedirectToAction("AcessoNegado", "Home");
                }
            }

            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.TipoUsuario)
                .FirstOrDefaultAsync(m => m.UsuarioId == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                string Email = HttpContext.Session.GetString("email");
                var user = _context.Usuarios.Where(a => a.Email == Email).FirstOrDefault();
                Guid idTipoAluno = _context.TiposUsuario.Where(a => a.Tipo == "Aluno").FirstOrDefault().TipoUsuarioId;
                if (user.TipoUsuarioId == idTipoAluno)
                {
                    return RedirectToAction("AcessoNegado", "Home");
                }
            }

            ViewData["TipoUsuarioId"] = new SelectList(_context.TiposUsuario, "TipoUsuarioId", "TipoUsuarioId");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UsuarioId,UsuarioNome,Email,Senha,Celular,CadAtivo,DataCadastro,CadInativo,TipoUsuarioId")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.UsuarioId = Guid.NewGuid();
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipoUsuarioId"] = new SelectList(_context.TiposUsuario, "TipoUsuarioId", "TipoUsuarioId", usuario.TipoUsuarioId);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                string Email = HttpContext.Session.GetString("email");
                var user = _context.Usuarios.Where(a => a.Email == Email).FirstOrDefault();
                Guid idTipoAluno = _context.TiposUsuario.Where(a => a.Tipo == "Aluno").FirstOrDefault().TipoUsuarioId;
                if (user.TipoUsuarioId == idTipoAluno)
                {
                    return RedirectToAction("AcessoNegado", "Home");
                }
            }

            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            ViewData["TipoUsuarioId"] = new SelectList(_context.TiposUsuario, "TipoUsuarioId", "TipoUsuarioId", usuario.TipoUsuarioId);
            return View(usuario);
        }

        public async Task<IActionResult> SelecionaTipo(Guid? id)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Login", "Home");
            }

            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            if (usuario.TipoUsuarioId == _context.TiposUsuario.FirstOrDefault(a => a.Tipo == "Aluno").TipoUsuarioId)
            {
                Aluno aluno = _context.Alunos.Where(a => a.Email == usuario.Email).FirstOrDefault();
                ViewData["TipoUsuarioId"] = new SelectList(_context.TiposUsuario, "TipoUsuarioId", "TipoUsuarioId", usuario.TipoUsuarioId);
                return RedirectToAction("Edit", "Alunos", new { id = aluno.AlunoId });
            }
            else
            {
                ViewData["TipoUsuarioId"] = new SelectList(_context.TiposUsuario, "TipoUsuarioId", "TipoUsuarioId", usuario.TipoUsuarioId);
                return RedirectToAction("Edit", "Usuarios", new { id = usuario.UsuarioId });
            }
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("UsuarioId,UsuarioNome,Email,Senha,Celular,CadAtivo,DataCadastro,CadInativo,TipoUsuarioId")] Usuario usuario)
        {
            if (id != usuario.UsuarioId)
            {
                return NotFound();
            }

            if (_context.Usuarios.FirstOrDefault(u => u.UsuarioNome == "Administrador").UsuarioId == id)
            {
                ViewData["Erro"] = "Não é possível Editar o usuário Administrador";
                ViewData["TipoUsuarioId"] = new SelectList(_context.TiposUsuario, "TipoUsuarioId", "TipoUsuarioId", usuario.TipoUsuarioId);
                return View();
            }


            if (ModelState.IsValid)
            {
                Aluno aluno = _context.Alunos.Where(a => a.Email == usuario.Email).FirstOrDefault();
                if (usuario.CadAtivo == false)
                {
                    usuario.CadInativo = DateTime.Now;
                    aluno.CadAtivo = false;
                    aluno.CadInativo = DateTime.Now;
                }
                else
                {
                    aluno.CadAtivo = true;
                    usuario.CadInativo = null;
                    aluno.CadInativo = null;
                }
                try
                {
                    usuario.TipoUsuario = _context.TiposUsuario.Where(a => a.TipoUsuarioId == usuario.TipoUsuarioId).FirstOrDefault();
                    aluno.Senha = usuario.Senha;
                    aluno.Celular = usuario.Celular;
                    aluno.AlunoNome = usuario.UsuarioNome;
                    aluno.Email = usuario.Email;
                    aluno.TipoUsuarioId = usuario.TipoUsuarioId;
                    aluno.TipoUsuario = usuario.TipoUsuario;

                    _context.Update(usuario);
                    _context.Alunos.Update(aluno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.UsuarioId))
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
            ViewData["TipoUsuarioId"] = new SelectList(_context.TiposUsuario, "TipoUsuarioId", "TipoUsuarioId", usuario.TipoUsuarioId);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                string Email = HttpContext.Session.GetString("email");
                var user = _context.Usuarios.Where(a => a.Email == Email).FirstOrDefault();
                Guid idTipoAluno = _context.TiposUsuario.Where(a => a.Tipo == "Aluno").FirstOrDefault().TipoUsuarioId;
                if (user.TipoUsuarioId == idTipoAluno)
                {
                    return RedirectToAction("AcessoNegado", "Home");
                }
            }

            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.TipoUsuario)
                .FirstOrDefaultAsync(m => m.UsuarioId == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario.UsuarioNome == "Administrador")
            {
                ViewData["Erro"] = "Não é possível Excluir o usuário Administrador";
                return View(usuario);
            }

            var aluno = _context.Alunos.Where(a => a.Email == usuario.Email).FirstOrDefault();
            if (usuario != null)
            {
                aluno.CadAtivo = false;
                aluno.CadInativo = DateTime.Now;
                usuario.CadAtivo = false;
                usuario.CadInativo = DateTime.Now;
                _context.Alunos.Update(aluno);
                _context.Update(aluno);
                await _context.SaveChangesAsync();
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(Guid id)
        {
            return _context.Usuarios.Any(e => e.UsuarioId == id);
        }
    }
}
