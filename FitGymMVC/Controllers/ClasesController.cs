using FitGymMVC.Models;
using FitGymMVC.Servicios;
using FitGymMVC.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitGymMVC.Controllers
{
    public class ClasesController : Controller
    {
        private readonly IClasesServicio _servicio;

        public ClasesController(IClasesServicio service) 
        {
            _servicio = service; //inyeccion de dependencias.
        }
        public IActionResult Listar() //es una vista.
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
        public IActionResult Guardar() //mostrar formulario para crear usuario.
        {
            return View();
        }
        [HttpPost]
        public IActionResult Guardar(ClasesModel objClase)
        {
            if (!ModelState.IsValid)
            {
                return View(objClase);
            }

            var respuesta = _servicio.Guardar(objClase);

            if (respuesta.Exito)
            {
                return RedirectToAction("ClaseCreada");
            }
            else
            {
                ModelState.AddModelError(string.Empty, respuesta.Mensaje); // mostramos el mensaje que venga
                return View(objClase); // <-- volvemos a mostrar el formulario
            }
        }

        public IActionResult ClaseCreada() //mostrar formulario solo devuelve la vista
        {
            return View();
        }

    }

}
