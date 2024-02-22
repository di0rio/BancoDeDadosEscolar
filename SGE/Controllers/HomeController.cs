using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SGE.Data;
using SGE.Models;
using System.Diagnostics;

namespace SGE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SGEContext _context;

        public HomeController(ILogger<HomeController> logger, SGEContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login()
        {
            List<TipoUsuario> tiposUsuario = _context.TiposUsuario.ToList();
            List<Usuario> usuarios = _context.Usuarios.ToList();
            if (tiposUsuario.Count == 0)
            {
                TipoUsuario tipoUsuario1 = new TipoUsuario();
                tipoUsuario1.Tipo = "Administrador";
                _context.TiposUsuario.Add(tipoUsuario1);
                _context.SaveChanges();
                TipoUsuario tipoUsuario2 = new TipoUsuario();
                tipoUsuario2.Tipo = "Aluno";
                _context.TiposUsuario.Add(tipoUsuario2);
                _context.SaveChanges();
                TipoUsuario tipoUsuario3 = new TipoUsuario();
                tipoUsuario3.Tipo = "Professor";
                _context.TiposUsuario.Add(tipoUsuario3);
                _context.SaveChanges();
            }
            if (!usuarios.Any(u => u.UsuarioNome == "Administrador"))
            {
                Usuario usuario = new Usuario();
                usuario.UsuarioNome = "Administrador";
                usuario.Email = "admin@sge.com.br";
                usuario.Senha = "admin";
                usuario.TipoUsuarioId = _context.TiposUsuario.FirstOrDefault(t => t.Tipo == "Administrador").TipoUsuarioId;
                usuario.CadAtivo = true;
                usuario.DataCadastro = DateTime.Now;
                usuario.Celular = "99999999999";
                usuario.TipoUsuario = _context.TiposUsuario.FirstOrDefault(t => t.Tipo == "Administrador");
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
            }

            return View(usuarios);
        }
        [HttpPost]
        public IActionResult Login(string inEmail, string inSenha)
        {
            if (inEmail == null)
            {
                ViewData["Erro"] = "Digite seu e-mail!";
                return View("Login");
            }

            if (inSenha == null)
            {
                ViewData["Erro"] = "Digite sua senha!";
                return View("Login");
            }

            Usuario usuario = _context.Usuarios.FirstOrDefault(u => u.Email == inEmail && u.Senha == inSenha);
            if (usuario != null)
            {
                if (usuario.CadAtivo == false)
                {
                    ViewData["Erro"] = "Seu cadastro está desativado!";
                    return View("Login");
                }
                HttpContext.Session.SetString("usuario", usuario.UsuarioNome);
                HttpContext.Session.SetString("email", usuario.Email);
                HttpContext.Session.SetString("tipo", usuario.TipoUsuarioId.ToString());
                HttpContext.Session.SetString("usuarioId", usuario.UsuarioId.ToString());
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["Erro"] = "O E-mail e/ou a Senha Informados não conferem!\nVerifique as informações e tente novamente.";
                return View("Login");
            }
        }

        public IActionResult AcessoNegado()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
