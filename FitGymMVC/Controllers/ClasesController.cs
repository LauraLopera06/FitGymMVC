using FitGymMVC.Models;
using FitGymMVC.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace FitGymMVC.Controllers
{
    public class ClasesController : Controller
    {
        private readonly ClasesServicio _servicio;

        public ClasesController(ClasesServicio service)
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
        [HttpPost]
        public IActionResult Guardar(ClasesModel objClase)
        {
            if (!ModelState.IsValid)
            {
                return View(objClase); // <-- devuelves el modelo para que no se borren los campos
            }

            var respuesta = _servicio.Guardar(objClase); // <-- ahora 'respuesta' tiene Exito y Mensaje

            if (respuesta.Exito)
            {
                return RedirectToAction("ClaseCreada");
            }
            else
            {
                ModelState.AddModelError(string.Empty, respuesta.Mensaje); // <-- mostramos el mensaje que venga
                return View(objClase); // <-- volvemos a mostrar el formulario
            }
        }

        public IActionResult ClaseCreada() //mostrar formulario solo devuelve la vista
        {
            return View();
        }

    }

}
