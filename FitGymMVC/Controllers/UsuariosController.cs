using FitGymMVC.Models;
using FitGymMVC.Servicios;
using FitGymMVC.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitGymMVC.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUsuariosServicio _servicio;

        public UsuariosController(IUsuariosServicio service)
        {
            _servicio = service; //ineyccion de dependencias
        }
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
        public IActionResult Login()
        {
            return View();
        }

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
