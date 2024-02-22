using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using SGE.Data;
using SGE.Models;

namespace SGE.Controllers
{
    public class ReservasSalaController : Controller
    {
        private readonly SGEContext _context;

        public ReservasSalaController(SGEContext context)
        {
            _context = context;
        }

        // GET: ReservasSala
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

            var sGEContext = _context.ReservasSala.Include(r => r.Sala).Include(r => r.Usuario);
            return View(await sGEContext.ToListAsync());
        }

        // GET: ReservasSala/Details/5
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

            var reservaSala = await _context.ReservasSala
                .Include(r => r.Sala)
                .Include(r => r.Usuario)
                .FirstOrDefaultAsync(m => m.ReservaSalaId == id);
            if (reservaSala == null)
            {
                return NotFound();
            }

            return View(reservaSala);
        }

        // GET: ReservasSala/Create
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

            ViewData["SalaId"] = new SelectList(_context.Salas, "SalaId", "SalaId");
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId");
            return View();
        }

        // POST: ReservasSala/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReservaSalaId,SalaId,UsuarioId,DataReserva,HoraInicio,HoraFim,CadAtivo,CadInativo")] ReservaSala reservaSala)
        {
            if (ModelState.IsValid)
            {
                reservaSala.ReservaSalaId = Guid.NewGuid();
                _context.Add(reservaSala);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SalaId"] = new SelectList(_context.Salas, "SalaId", "SalaId", reservaSala.SalaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId", reservaSala.UsuarioId);
            return View(reservaSala);
        }

        // GET: ReservasSala/Edit/5
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

            var reservaSala = await _context.ReservasSala.FindAsync(id);
            if (reservaSala == null)
            {
                return NotFound();
            }
            ViewData["SalaId"] = new SelectList(_context.Salas, "SalaId", "SalaId", reservaSala.SalaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId", reservaSala.UsuarioId);
            return View(reservaSala);
        }

        // POST: ReservasSala/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ReservaSalaId,SalaId,UsuarioId,DataReserva,HoraInicio,HoraFim,CadAtivo,CadInativo")] ReservaSala reservaSala)
        {
            if (id != reservaSala.ReservaSalaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservaSala);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaSalaExists(reservaSala.ReservaSalaId))
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
            ViewData["SalaId"] = new SelectList(_context.Salas, "SalaId", "SalaId", reservaSala.SalaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId", reservaSala.UsuarioId);
            return View(reservaSala);
        }

        // GET: ReservasSala/Delete/5
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

            var reservaSala = await _context.ReservasSala
                .Include(r => r.Sala)
                .Include(r => r.Usuario)
                .FirstOrDefaultAsync(m => m.ReservaSalaId == id);
            if (reservaSala == null)
            {
                return NotFound();
            }

            return View(reservaSala);
        }

        // POST: ReservasSala/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var reservaSala = await _context.ReservasSala.FindAsync(id);
            if (reservaSala != null)
            {
                _context.ReservasSala.Remove(reservaSala);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservaSalaExists(Guid id)
        {
            return _context.ReservasSala.Any(e => e.ReservaSalaId == id);
        }

        public async Task<IActionResult> Reservas()
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
            var user = _context.Usuarios.Where(a => a.Email == HttpContext.Session.GetString("email")).FirstOrDefault();
            var reservasUsuario = _context.ReservasSala.Where(a => a.UsuarioId == user.UsuarioId).Include(r => r.Sala).Include(r => r.Usuario);
            var reservasSala = _context.ReservasSala.Where(a => a.CadAtivo == true).ToList();
            ViewData["ReservasUsuario"] = reservasUsuario.ToList();
            var salas = await _context.Salas.Where(s => s.CadAtivo == true).ToListAsync();
            ViewData["Salas"] = salas;
            return View(reservasSala);
        }

        public async Task<IActionResult> SelecionaSala(Guid id)
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
                List<Sala> salas = _context.Salas.Where(a => a.CadAtivo == true).ToList();
                ViewData["SalaSelecionada"] = salas.FirstOrDefault(a => a.SalaId == id).SalaNome;
                ViewData["Salas"] = salas;
                ViewData["IdSalaSelecionada"] = id;
                ViewData["IdSala"] = salas;
                ViewData["ReservasSala"] = _context.ReservasSala.Where(a => a.CadAtivo == true).Where(r => r.SalaId == id).Include(u => u.Usuario).Include(s => s.Sala).ToList();
                List<ReservaSala> reservasSalas = _context.ReservasSala.Where(a => a.CadAtivo == true).Where(r => r.SalaId == id).ToList();
                return View("Reservas", reservasSalas);
            }

        }

        public async Task<IActionResult> FiltarSalas(string filtro)
        {
            if (filtro == null)
            {
                return RedirectToAction("Reservas");
            }
            List<Sala> salas = _context.Salas.Where(a => a.CadAtivo == true).ToList();
            salas = salas.Where(a => a.SalaNome.ToUpper().Contains(filtro.ToUpper())).ToList();
            ViewData["Salas"] = salas;
            List<ReservaSala> reservasSalas = _context.ReservasSala.Where(a => a.CadAtivo == true).ToList();
            ViewData["ReservasSala"] = _context.ReservasSala.Where(a => a.CadAtivo == true).Include(u => u.Usuario).Include(s => s.Sala).ToList();
            return View("Reservas", reservasSalas);
        }

        public async Task<IActionResult> NovaReserva(Guid id)
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
            var user = _context.Usuarios.Where(a => a.Email == HttpContext.Session.GetString("email")).FirstOrDefault();
            var sala = _context.Salas.Where(a => a.SalaId == id).FirstOrDefault();
            var reservasUsuario = _context.ReservasSala.Where(a => a.UsuarioId == user.UsuarioId).Include(r => r.Sala).Include(r => r.Usuario);
            var reservasSala = _context.ReservasSala.Where(a => a.CadAtivo == true).Where(r => r.SalaId == id).ToList();
            ViewData["ReservasUsuario"] = reservasUsuario.ToList();
            ViewData["Salas"] = _context.Salas.Where(s => s.CadAtivo == true).ToList();
            ViewData["SalaSelecionada"] = sala.SalaNome;
            ViewData["IdSalaSelecionada"] = id;
            ViewData["Usuario"] = user;
            ViewData["ReservasSala"] = _context.ReservasSala.Where(a => a.CadAtivo == true).Include(u => u.Usuario).Include(s => s.Sala).ToList();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NovaReserva([Bind("ReservaSalaId,SalaId,UsuarioId,DataReserva,HoraInicio,DataFimReserva,HoraFim,CadAtivo,CadInativo,CorReserva")] ReservaSala reservaSala, string SalaNome, string UsuarioNome)
        {

            if (ModelState.IsValid)
            {
                reservaSala.ReservaSalaId = Guid.NewGuid();
                reservaSala.CadAtivo = true;
                reservaSala.CadInativo = null;
                reservaSala.Sala = _context.Salas.Where(a => a.SalaId == reservaSala.SalaId).FirstOrDefault();
                reservaSala.Usuario = _context.Usuarios.Where(a => a.UsuarioId == reservaSala.UsuarioId).FirstOrDefault();
                _context.Add(reservaSala);
                await _context.SaveChangesAsync();
                return RedirectToAction("SelecionaSala", new { id = reservaSala.SalaId });
            }
            ViewData["SalaId"] = new SelectList(_context.Salas, "SalaId", "SalaId", reservaSala.SalaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId", reservaSala.UsuarioId);
            var user = _context.Usuarios.Where(a => a.Email == HttpContext.Session.GetString("email")).Include(t => t.TipoUsuario).FirstOrDefault();
            var sala = _context.Salas.Where(a => a.SalaId == reservaSala.SalaId).FirstOrDefault();
            var reservasUsuario = _context.ReservasSala.Where(a => a.UsuarioId == user.UsuarioId).Include(r => r.Sala).Include(r => r.Usuario);
            var reservasSala = _context.ReservasSala.Where(a => a.CadAtivo == true).Where(r => r.SalaId == reservaSala.SalaId).Include(u => u.Usuario).ToList();
            ViewData["ReservasUsuario"] = reservasUsuario.ToList();
            ViewData["Salas"] = _context.Salas.Where(s => s.CadAtivo == true).ToList();
            ViewData["SalaSelecionada"] = sala.SalaNome;
            ViewData["IdSalaSelecionada"] = reservaSala.SalaId;
            ViewData["Usuario"] = user;
            ViewData["ReservasSala"] = _context.ReservasSala.Where(a => a.CadAtivo == true).Include(u => u.Usuario).Include(s => s.Sala).ToList();
            return View();
        }
    }
}

