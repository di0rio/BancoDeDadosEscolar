using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using SGE.Data;
using SGE.Models;

namespace SGE.Controllers
{
    public class OcorrenciasController : Controller
    {
        private readonly SGEContext _context;

        public OcorrenciasController(SGEContext context)
        {
            _context = context;
        }

        // GET: Ocorrencias
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
            var sGEContext = _context.Ocorrencias.Include(o => o.TipoOcorrencia).Include(o => o.Usuario);
            return View(await sGEContext.ToListAsync());
        }

        // GET: Ocorrencias/Details/5
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

            var Ocorrencia = await _context.Ocorrencias
                .Include(o => o.TipoOcorrencia)
                .Include(o => o.Usuario)
                .FirstOrDefaultAsync(m => m.OcorrenciaId == id);
            if (Ocorrencia == null)
            {
                return NotFound();
            }

            return View(Ocorrencia);
        }

        // GET: Ocorrencias/Create
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

            ViewData["TipoOcorrenciaId"] = new SelectList(_context.TiposOcorrencia, "TipoOcorrenciaId", "TipoOcorrenciaId");
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId");
            return View();
        }

        // POST: Ocorrencias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OcorrenciaId,TipoOcorrenciaId,UsuarioId,DataOcorrencia,Descricao,CadAtivo,CadInativo,Finalizado,DataFinalizado")] Ocorrencia Ocorrencia)
        {
            if (ModelState.IsValid)
            {
                Ocorrencia.OcorrenciaId = Guid.NewGuid();
                _context.Add(Ocorrencia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipoOcorrenciaId"] = new SelectList(_context.TiposOcorrencia, "TipoOcorrenciaId", "TipoOcorrenciaId", Ocorrencia.TipoOcorrenciaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId", Ocorrencia.UsuarioId);
            return View(Ocorrencia);
        }

        // GET: Ocorrencias/Edit/5
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

            var Ocorrencia = await _context.Ocorrencias.FindAsync(id);
            if (Ocorrencia == null)
            {
                return NotFound();
            }
            ViewData["TipoOcorrenciaId"] = new SelectList(_context.TiposOcorrencia, "TipoOcorrenciaId", "TipoOcorrenciaId", Ocorrencia.TipoOcorrenciaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId", Ocorrencia.UsuarioId);
            return View(Ocorrencia);
        }

        // POST: Ocorrencias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("OcorrenciaId,TipoOcorrenciaId,UsuarioId,DataOcorrencia,Descricao,CadAtivo,CadInativo,Finalizado,DataFinalizado")] Ocorrencia Ocorrencia)
        {
            if (id != Ocorrencia.OcorrenciaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Ocorrencia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OcorrenciaExists(Ocorrencia.OcorrenciaId))
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
            ViewData["TipoOcorrenciaId"] = new SelectList(_context.TiposOcorrencia, "TipoOcorrenciaId", "TipoOcorrenciaId", Ocorrencia.TipoOcorrenciaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId", Ocorrencia.UsuarioId);
            return View(Ocorrencia);
        }

        // GET: Ocorrencias/Delete/5
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

            var Ocorrencia = await _context.Ocorrencias
                .Include(o => o.TipoOcorrencia)
                .Include(o => o.Usuario)
                .FirstOrDefaultAsync(m => m.OcorrenciaId == id);
            if (Ocorrencia == null)
            {
                return NotFound();
            }

            return View(Ocorrencia);
        }

        // POST: Ocorrencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var Ocorrencia = await _context.Ocorrencias.FindAsync(id);

            Ocorrencia.CadInativo = DateTime.Now;
            Ocorrencia.CadAtivo = false;
            _context.Update(Ocorrencia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OcorrenciaExists(Guid id)
        {
            return _context.Ocorrencias.Any(e => e.OcorrenciaId == id);
        }

        public async Task<IActionResult> ListaOcorrencias()
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                List<Ocorrencia> ListaOcorrencias = _context.Ocorrencias
                .Include(u => u.Usuario)
                .Include(to => to.TipoOcorrencia)
                .Include(a => a.Aluno).ToList();
                List<Aluno> listaAlunos = _context.Alunos.ToList();
                ViewData["listaAlunos"] = listaAlunos;
                return View(ListaOcorrencias);
            }
        }

        public async Task<IActionResult> BuscarAluno(int tipoBusca, string? filtro)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                List<Ocorrencia> ListaOcorrencias = _context.Ocorrencias.Include(u => u.Usuario).Include(to => to.TipoOcorrencia).Include(a => a.Aluno).ToList();
                if (filtro == null)
                {
                    List<Aluno> listaAlunos = _context.Alunos.ToList();
                    ViewData["listaAlunos"] = listaAlunos;
                    return RedirectToAction("ListaOcorrencias", ListaOcorrencias);
                }
                else
                {
                    if (tipoBusca == 0)
                    {
                        List<Aluno> listaAlunos = _context.Alunos.Where(a => a.AlunoNome.ToUpper().Contains(filtro.ToUpper())).ToList();
                        ViewData["listaAlunos"] = listaAlunos;
                        ListaOcorrencias = ListaOcorrencias = _context.Ocorrencias.Include(u => u.Usuario).Include(to => to.TipoOcorrencia).Include(a => a.Aluno).ToList();
                        return View("ListaOcorrencias", ListaOcorrencias);
                    }
                    else if (tipoBusca == 1)
                    {
                        List<Aluno> listaAlunos = _context.Alunos.Where(a => a.Email.ToUpper().Contains(filtro.ToUpper())).ToList();
                        ViewData["listaAlunos"] = listaAlunos;
                        ListaOcorrencias = ListaOcorrencias = _context.Ocorrencias.Include(u => u.Usuario).Include(to => to.TipoOcorrencia).Include(a => a.Aluno).ToList();
                        return View("ListaOcorrencias", ListaOcorrencias);
                    }
                    else if (tipoBusca == 2)
                    {
                        List<Aluno> listaAlunos = _context.Alunos.Where(a => a.Matricula.ToUpper().Contains(filtro.ToUpper())).ToList();
                        ViewData["listaAlunos"] = listaAlunos;
                        ListaOcorrencias = ListaOcorrencias = _context.Ocorrencias.Include(u => u.Usuario).Include(to => to.TipoOcorrencia).Include(a => a.Aluno).ToList();
                        return View("ListaOcorrencias", ListaOcorrencias);
                    }
                    else if (tipoBusca == 3)
                    {
                        List<Aluno> listaAlunos = _context.Alunos.Where(a => a.Celular.Contains(filtro)).ToList();
                        ViewData["listaAlunos"] = listaAlunos;
                        ListaOcorrencias = ListaOcorrencias = _context.Ocorrencias.Include(u => u.Usuario).Include(to => to.TipoOcorrencia).Include(a => a.Aluno).ToList();
                        return View("ListaOcorrencias", ListaOcorrencias);
                    }
                }
                return View("ListaOcorrencias", ListaOcorrencias);
            }
        }

        public async Task<IActionResult> OcorrenciasAluno(Guid id)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                List<Aluno> listaAlunos = _context.Alunos.ToList();
                ViewData["listaAlunos"] = listaAlunos;
                ViewData["idAluno"] = id;
                ViewData["nomeAluno"] = _context.Alunos.Where(a => a.AlunoId == id).FirstOrDefault().AlunoNome;
                List<Ocorrencia> ListaOcorrencias = _context.Ocorrencias.Include(u => u.Usuario).Include(to => to.TipoOcorrencia).Include(a => a.Aluno).Where(a => a.AlunoId == id).ToList();
                return View("ListaOcorrencias", ListaOcorrencias);
            }
        }

        public async Task<IActionResult> AdicionarOcorrencia(Guid id)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                List<Aluno> listaAlunos = _context.Alunos.ToList();
                ViewData["listaAlunos"] = listaAlunos;
                ViewData["alunoId"] = id;
                ViewData["alunoNome"] = _context.Alunos.Where(a => a.AlunoId == id).FirstOrDefault().AlunoNome;
                ViewData["TipoOcorrenciaId"] = new SelectList(_context.TiposOcorrencia, "TipoOcorrenciaId", "TipoOcorrenciaNome");
                ViewData["usuarioId"] = _context.Usuarios.Where(a => a.Email == HttpContext.Session.GetString("email")).FirstOrDefault().UsuarioId;
                ViewData["usuarioNome"] = _context.Usuarios.Where(a => a.Email == HttpContext.Session.GetString("email")).FirstOrDefault().UsuarioNome;
                List<Ocorrencia> ListaOcorrencias = _context.Ocorrencias.Include(u => u.Usuario).Include(to => to.TipoOcorrencia).Include(a => a.Aluno).Where(a => a.AlunoId == id).ToList();
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdicionarOcorrencia([Bind("OcorrenciaId,TipoOcorrenciaId,UsuarioId,DataOcorrencia,Descricao,CadAtivo,CadInativo,Finalizado,DataFinalizado,AlunoId,Tratativa")] Ocorrencia Ocorrencia, string AlunoNome, string UsuarioNome)
        {
            if (ModelState.IsValid)
            {
                if (Ocorrencia.DataOcorrencia == null)
                {
                    Ocorrencia.DataOcorrencia = DateTime.Now;
                }

                if (Ocorrencia.CadAtivo == false)
                {
                    Ocorrencia.CadInativo = DateTime.Now;
                }
                else
                {
                    Ocorrencia.CadInativo = null;
                }
                if (Ocorrencia.Finalizado == true)
                {
                    Ocorrencia.DataFinalizado = DateTime.Now;
                }
                else
                {
                    Ocorrencia.DataFinalizado = null;
                }
                Ocorrencia.OcorrenciaId = Guid.NewGuid();
                _context.Add(Ocorrencia);
                await _context.SaveChangesAsync();
                return RedirectToAction("OcorrenciasAluno", new { id = Ocorrencia.AlunoId });
            }
            ViewData["TipoOcorrenciaId"] = new SelectList(_context.TiposOcorrencia, "TipoOcorrenciaId", "TipoOcorrenciaId", Ocorrencia.TipoOcorrenciaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId", Ocorrencia.UsuarioId);
            return View(Ocorrencia);
        }
    }
}
