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
        private readonly IEmailServicio _emailServicio;
        public UsuariosController(IUsuariosServicio service, IEmailServicio emailServicio)
        {
            _servicio = service; //ineyccion de dependencias
            _emailServicio = emailServicio;
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
        public async Task<IActionResult> Guardar(UsuariosModel objUsuario) 
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var respuesta = _servicio.Guardar(objUsuario);

            if (respuesta)
            {
                var correoEnviado = await _emailServicio.EnviarEmail(
                    emailReceptor: objUsuario.Correo,
                    tema: "Bienvenido a FitGym",
                    cuerpo: "<h3>¡Gracias por registrarte en FitGym!</h3><p>Tu cuenta ha sido creada correctamente.</p>");
                
                if (!correoEnviado)
                {
                    ModelState.AddModelError(string.Empty, "Ocurrió un error. Por favor, intenta nuevamente.");
                    return View(objUsuario);
                }
                return RedirectToAction("InicioCliente", "Cliente");

            }
            else {
                ModelState.AddModelError(string.Empty, "Ya existe un usuario con esa cédula.");
                return View(objUsuario);
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
                
                switch (usuario.TipoUsuario)
                {
                    case "Administrador":
                        return RedirectToAction("InicioAdmin", "Administrador");
                    case "Entrenador":
                        return RedirectToAction("InicioEntrenador", "Entrenador");
                    case "Cliente":
                        return RedirectToAction("InicioCliente", "Cliente");
                    default:
                        return RedirectToAction("Bienvenida", "Home"); // Fallback
                }
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

        [HttpGet]
        public IActionResult RedireccionarPorRol()
        {
            var rol = User.FindFirst(ClaimTypes.Role)?.Value;

            switch (rol)
            {
                case "Administrador":
                    return RedirectToAction("InicioAdmin", "Administrador");
                case "Entrenador":
                    return RedirectToAction("InicioEntrenador", "Entrenador");
                case "Cliente":
                    return RedirectToAction("InicioCliente", "Cliente");
                default:
                    return RedirectToAction("Bienvenida", "Home"); // Redirección segura por defecto
            }
        }

        public IActionResult Ayuda()
        {
            return View();
        }
    }

}
