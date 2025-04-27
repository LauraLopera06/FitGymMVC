using FitGymMVC.Models;
using FitGymMVC.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace FitGymMVC.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly UsuariosServicio _servicio;

        public UsuariosController(UsuariosServicio service)
        {
            _servicio = service;
        }
        public IActionResult Listar()
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
        public IActionResult Guardar() //mostrar formulario solo devuelve la vista
        {
            return View();
        }

        public IActionResult CuentaCreada() //mostrar formulario solo devuelve la vista
        {
            return View();
        }

        [HttpPost]
        public IActionResult Guardar(UsuariosModel objUsuario) //sacar datos del formulario para mandar a la BD
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
    }

}
