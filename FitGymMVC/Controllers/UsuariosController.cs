using FitGymMVC.Models;
using FitGymMVC.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using FitGymMVC.Servicios;

namespace FitGymMVC.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUsuariosServicio _servicio;

        public UsuariosController(IUsuariosServicio service)
        {
            _servicio = service; //ineyccion de dependencias
        }
        [Authorize]
        public IActionResult Listar() //debe haber una vista Listar en /views/usuarios así con el resto
        {
            try
            {
                var objLista = _servicio.Listar();

                return View(objLista);
            }
            catch (Exception)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            
        }
        public IActionResult Guardar() //mostrar formulario para guardar.
        {
            return View();
        }

        public IActionResult CuentaCreada()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Guardar(UsuariosModel objUsuario) 
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var respuesta = _servicio.Guardar(objUsuario);

            if (respuesta)
            {
                return RedirectToAction("CuentaCreada");
            }
            else {
                return View("~/Views/Shared/Error.cshtml");
            }
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string correo, string contraseña)
        {
            var usuario = _servicio.Login(correo, contraseña);

            if (usuario != null)// si el correo y la contraseña son correctas
            {
                var claims = new List<Claim> // Se crean los claims (información del usuario que se guardará en la cookie)
                {
                    new Claim(ClaimTypes.Name, usuario.Correo),
                    new Claim(ClaimTypes.Role, usuario.TipoUsuario)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);// Se construye una identidad basada en los claims, usando autenticación por cookies
                var principal = new ClaimsPrincipal(identity); // Se crea el "principal" que representa al usuario autenticado

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);// Se registra la sesión del usuario (se emite la cookie de autenticación)
                return RedirectToAction("InicioUsuario");
            }

            ViewBag.Error = "Correo o contraseña incorrectos";
            return View();
        }

        [Authorize] // Solo un usuario autenticado puede acceder a esta acción
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); //elimina la cookie
            return RedirectToAction("Login");//redirige al login
        }
        [Authorize]
        public IActionResult InicioUsuario()
        {
            return View();
        }

        public IActionResult Ayuda()
        {
            return View();
        }
    }

}
